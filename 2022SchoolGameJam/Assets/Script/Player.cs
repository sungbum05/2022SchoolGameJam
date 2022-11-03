using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType
{
    Human, Robot
}

public enum YutType
{
    ¸ð, µµ, °³, °É, À·
}

public class Player : MonoBehaviour
{
    public PlayerType PlayerType;

    public bool IsMyTurn = false;
    public bool IsStart = false;

    public int LineIdx = 0;

    public GameObject SelectPin;
    public GameObject Pin1;
    public GameObject Pin2;

    public LineClass HideLine_1;
    public LineClass HideLine_2;

    [SerializeField]
    List<Point> CurLine;
    [SerializeField]
    Point CurPoint;
    [SerializeField]
    bool IsCanHideLine;

    [SerializeField]
    LayerMask layerMask;

    public List<YutType> YutStack;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 1000, Color.blue);

            RaycastHit hit;

            if(Physics.Raycast(ray, out hit,15.0f,layerMask))
            {
               Debug.Log(hit.transform.name);
            }
        }
    }

    public void BasicSetting()
    {
        CurLine = Board.Instance.BasicLine[0].Points;

        StartCoroutine(Move(CurLine[LineIdx]));
    }

    public void StartCalculator(YutType Type)
    {
        StartCoroutine(YutStackCalculator(Type));
    }

    IEnumerator ChangeLine()
    {
        yield return null;
    }

    IEnumerator YutStackCalculator(YutType Type)
    {
        yield return null;

        int MoveValue = 0;

        switch (Type)
        {
            case YutType.µµ:
                MoveValue = 1;
                break;

            case YutType.°³:
                MoveValue = 2;
                break;

            case YutType.°É:
                MoveValue = 3;
                break;

            case YutType.À·:
                MoveValue = 4;
                break;

            case YutType.¸ð:
                MoveValue = 5;
                break;
        }

        yield return StartCoroutine(PinMovePos(MoveValue));

        for (int i = 0; i < MoveValue; i++)
        {
            StartCoroutine(Move(CurLine[++LineIdx]));
            yield return new WaitForSeconds(0.5f);
        }

        if (CurPoint.IsGoToHide == true)
            IsCanHideLine = true;

        YutStack.Remove(Type);
        GameMgr.Instance.IsPlayerMove = true;

        yield break;
    }

    IEnumerator PinMovePos(int MoveValue)
    {
        yield return null;

        #region »û±æ °è»ê
        if (IsCanHideLine == true)
        {
            int HideLineIdx = 0;

            HideLine_1 = CurPoint.HideLine_1;
            HideLine_2 = CurPoint.HideLine_2;

            Pin2.transform.position = HideLine_1.Points[HideLineIdx + MoveValue].transform.position;
        }
        #endregion

        Pin1.transform.position = CurLine[LineIdx + MoveValue].transform.position;


        while (true)
        {
            yield return null;

            Debug.DrawRay(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition) * 15.0f, Color.blue, 2.5f);
        }
    }

    IEnumerator Move(Point point)
    {
        yield return new WaitForSeconds(0.5f);

        CurPoint = point;
        this.transform.position = CurPoint.transform.position + new Vector3(0, 0.1f, 0);

        yield break;
    }
}
