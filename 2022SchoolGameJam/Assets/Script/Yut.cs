using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yut : MonoBehaviour
{
    public bool IsCanDraw = false;

    public bool IsDraw = false;
    public bool IsRotation = false;

    [SerializeField]
    Vector3 OriginalPos;
    [SerializeField]
    Quaternion OriginalRot;

    [SerializeField]
    RaycastHit hit;
    [SerializeField]
    LayerMask layerMask;
    [SerializeField]
    float RotationValue;
    [SerializeField]
    Vector3 RotationVec;

    private void Awake()
    {
        OriginalPos = this.transform.position;
        OriginalRot = this.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsCanDraw == true)
        {
            DrawYut();
        }

        if(IsRotation == true)
        {
            //this.gameObject.transform.Rotate(RotationVec);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Floor") && IsDraw == true)
        {
            IsRotation = false;
            IsDraw = false;

            StartCoroutine(FloorCheck());
        }
    }

    public void DrawYut()
    {
        IsCanDraw = false;

        this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-0.2f, 0.2f), 1.0f, Random.Range(-0.2f, 0.2f)) * 5.0f, ForceMode.Impulse);

        RotationVec = new Vector3(Random.Range(-RotationValue, RotationValue), Random.Range(-RotationValue, RotationValue), Random.Range(-RotationValue, RotationValue)) * Time.deltaTime;
        IsDraw = true;
        IsRotation = true;
    }

    public void RetryDrawYut()
    {
        IsCanDraw = true;

        IsDraw = false;
        IsRotation = false;
    }

    void ResetPos()
    {
        this.transform.rotation = OriginalRot;
        this.transform.position = OriginalPos;
    }

    IEnumerator FloorCheck()
    {
        yield return new WaitForSeconds(4.0f);

        Debug.DrawRay(transform.position, -transform.forward * 15.0f, Color.blue, 2.5f);
        if (Physics.Raycast(transform.position, -transform.forward, out hit, 15.0f, layerMask))
        {

        }

        else
        {
            YutMgr.FleepCnt++;
        }

        yield return new WaitForSeconds(0.5f);

        YutMgr.ClearYutCnt++;
        ResetPos();

        yield break;
    }
}
