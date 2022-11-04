using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    [Header("PostProcessing")]
    [SerializeField]
    PostProcessVolume Volume;
    [SerializeField]
    Vignette Vignette;
    [SerializeField]
    float MinValue, MaxValue;
    [SerializeField]
    float Speed;

    [Header("CameraMove")]
    [SerializeField]
    int idx = 0;
    [SerializeField]
    Coroutine CameraAngleCoroutine;

    [SerializeField]
    Vector3 OriginalCameraPos;
    [SerializeField]
    Vector3 YutCameraPos;
    [SerializeField]
    Vector3 FreeCameraPos;

    [SerializeField]
    Vector3 FixCameraRot;
    [SerializeField]
    Vector3 FreeCameraRot;

    [SerializeField]
    Vector3 OriginalFreeRot;
    [SerializeField]
    Vector3 MoveFreeRot;

    [SerializeField]
    Image CameraLights;
    [SerializeField]
    GameObject Target;
    [SerializeField]
    LayerMask LayerMask;

    private float xRotate, yRotate, xRotateMove, yRotateMove;
    public float rotateSpeed = 500.0f;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Volume.profile.TryGetSettings(out Vignette);

        StartCoroutine(CameraFade("Up"));
    }

    private void Update()
    {
        ChangeCameraAngle();

        if (idx == 2)
        {
            Target.SetActive(true);

            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2));
            Debug.DrawRay(ray.origin, ray.direction * 1000, Color.blue);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 15.0f, LayerMask))
            {
                Debug.Log(hit.transform.gameObject.GetComponent<Ghost>().Name);

                if(Input.GetMouseButtonDown(0))
                {
                    CameraLights.DOFade(1, 0);
                    SoundManager.Instance.PlaySFX("Camera_Flash", 2.0f);
                    CameraLights.DOFade(0, 1);

                    UIManager.Instance.OnGuideText($"{hit.transform.gameObject.GetComponent<Ghost>().Name} 멈춰!");
                    hit.transform.gameObject.GetComponent<Ghost>().ResetCorutine();
                }
            }

            if (Input.GetMouseButton(1)) // 클릭한 경우
            {
                xRotateMove = -Input.GetAxis("Mouse Y") * Time.deltaTime * rotateSpeed;
                yRotateMove = Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed;

                yRotate = transform.eulerAngles.y + yRotateMove;
                xRotate = xRotate + xRotateMove;

                xRotate = Mathf.Clamp(xRotate, -90, 90); // 위, 아래 고정

                transform.eulerAngles = new Vector3(xRotate, yRotate, 0);
            }
        }

        else
        {
            Target.SetActive(false);
        }
    }

    IEnumerator CameraFade(string Type)
    {
        yield return null;

        switch(Type)
        {
            case "Up":
                while(MaxValue > Vignette.intensity)
                {
                    yield return null;
                    Vignette.intensity.value += Time.deltaTime * Speed;
                }

                StartCoroutine(CameraFade("Down"));
                break;
            case "Down":
                while (MinValue < Vignette.intensity)
                {
                    yield return null;
                    Vignette.intensity.value -= Time.deltaTime * Speed;
                }
                StartCoroutine(CameraFade("Up"));
                break;
        }

        yield break;
    }

    public void ChangeCameraAngle()
    {
        if(Input.GetKeyDown(KeyCode.A) && CameraAngleCoroutine == null)
        {
            idx++;

            if (idx > 2)
                idx = 0;

            switch(idx)
            {
                case 0:
                    CameraAngleCoroutine = StartCoroutine(FixMyTurnCamera());
                    break;

                case 1:
                    CameraAngleCoroutine = StartCoroutine(FixYutCamera());
                    break;

                case 2:
                    CameraAngleCoroutine = StartCoroutine(FreeCamera());
                    break;
            }
        }
    }

    IEnumerator FixMyTurnCamera()
    {
        yield return null;

        this.transform.DOMove(OriginalCameraPos, 1.0f);
        this.transform.DORotate(FixCameraRot, 1.0f);

        yield return new WaitForSeconds(1.0f);

        CameraAngleCoroutine = null;

        yield break;
    }

    IEnumerator FixYutCamera()
    {
        this.transform.DOMove(YutCameraPos, 1.0f);
        this.transform.DORotate(FixCameraRot, 1.0f);

        yield return new WaitForSeconds(1.0f);

        CameraAngleCoroutine = null;

        yield break;
    }

    IEnumerator FreeCamera()
    {
        this.transform.DOMove(FreeCameraPos, 1.0f);
        this.transform.DORotate(FreeCameraRot, 1.0f);

        yield return new WaitForSeconds(1.0f);

        CameraAngleCoroutine = null;

        yield break;
    }
}
