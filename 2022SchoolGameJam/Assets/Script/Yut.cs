using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yut : MonoBehaviour
{
    [SerializeField]
    RaycastHit hit;
    [SerializeField]
    LayerMask layerMask;
    [SerializeField]
    bool IsRotation = false;
    [SerializeField]
    float RotationValue;
    [SerializeField]
    Vector3 RotationVec;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-0.2f, 0.2f), 1.0f, Random.Range(-0.2f, 0.2f)) * 5.0f, ForceMode.Impulse);

            RotationVec = new Vector3(Random.Range(-RotationValue, RotationValue), Random.Range(-RotationValue, RotationValue), Random.Range(-RotationValue, RotationValue)) * Time.deltaTime;
            IsRotation = true;
        }

        if(IsRotation == true)
        {
            this.gameObject.transform.Rotate(RotationVec);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Floor"))
        {
            IsRotation = false;

            StartCoroutine(FloorCheck());
        }
    }

    IEnumerator FloorCheck()
    {
        yield return new WaitForSeconds(2.0f);

        Debug.DrawRay(transform.position, -transform.forward * 15.0f, Color.blue, 2.5f);
        if (Physics.Raycast(transform.position, -transform.forward, out hit, 15.0f, layerMask))
        {
            Debug.Log("Floor");
        }
    }
}
