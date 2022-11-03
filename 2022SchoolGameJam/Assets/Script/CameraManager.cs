using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.PostProcessing;

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
