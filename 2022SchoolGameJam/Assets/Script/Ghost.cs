using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField]
    Vector3 OriginalPos;
    [SerializeField]
    float MaxMoveValue;
    [SerializeField]
    float MoveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        OriginalPos = this.transform.position;
        StartCoroutine(GhostMove());
    }

    IEnumerator GhostMove()
    {
        yield return null;

        while(true)
        {
            yield return null;

            this.gameObject.transform.position = Vector3.Lerp(this.transform.position, new Vector3(this.transform.position.x, OriginalPos.y + MaxMoveValue, this.transform.position.z), MoveSpeed * Time.deltaTime);

            if (OriginalPos.y + MaxMoveValue <= this.transform.position.y + 0.1f)
                break;
        }

        yield return new WaitForSeconds(0.5f);

        while (true)
        {
            yield return null;

            this.gameObject.transform.position = Vector3.Lerp(this.transform.position, new Vector3(this.transform.position.x, OriginalPos.y - MaxMoveValue, this.transform.position.z), MoveSpeed * Time.deltaTime);

            if (OriginalPos.y - MaxMoveValue >= this.transform.position.y - 0.1f)
                break;
        }

        yield return new WaitForSeconds(0.5f);

        StartCoroutine(GhostMove());
        yield break;
    }
}
