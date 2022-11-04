using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Ghost : MonoBehaviour
{
    public string Name;

    [SerializeField]
    Vector3 OriginalPos;
    [SerializeField]
    float MaxMoveValue;
    [SerializeField]
    float MoveSpeed;
    [SerializeField]
    GameObject ScreamPan;
    [SerializeField]
    List<Sprite> ScreamImages;

    public Coroutine FadeCorutine;

    // Start is called before the first frame update
    void Start()
    {
        OriginalPos = this.transform.position;
        StartCoroutine(GhostMove());

        ResetCorutine();
    }

    public void ResetCorutine()
    {
        if(FadeCorutine != null)
        {
            StopCoroutine(FadeCorutine);
            FadeCorutine = null;
        }

        FadeCorutine = StartCoroutine(FadeGhost());
    }

    IEnumerator FadeGhost()
    {
        yield return null;
        this.gameObject.GetComponent<MeshRenderer>().materials[0].DOFade(1, 0);

        float Ran = Random.Range(35.0f, 45.0f);

        this.gameObject.GetComponent<MeshRenderer>().materials[0].DOFade(0, Ran);
        yield return new WaitForSeconds(Ran);

        StartCoroutine(OnScream());
        ResetCorutine();
    }

    IEnumerator OnScream()
    {
        yield return null;

        SoundManager.Instance.PlaySFX($"Scream_{Random.Range(1, 4)}", 2.0f);

        ScreamPan.GetComponent<Image>().sprite = ScreamImages[Random.Range(0, ScreamImages.Count)];
        ScreamPan.SetActive(true);

        yield return new WaitForSeconds(2.5f);
        ScreamPan.SetActive(false);

        yield break;
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
