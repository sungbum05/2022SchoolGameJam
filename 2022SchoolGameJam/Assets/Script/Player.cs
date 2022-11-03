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
    public bool IsSelectPin = false;

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

    private void Start()
    {
        BasicSetting();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 1000, Color.blue);

            RaycastHit hit;

            if(Physics.Raycast(ray, out hit,15.0f,layerMask))
            {
                SelectPin = hit.transform.gameObject;
                IsSelectPin = true;
            }
        }
    }

    public void BasicSetting()
    {
        CurLine = Board.Instance.BasicLine[0].Points;
    }

    public void UpdateSetting()
    {
        StartCoroutine(Move(CurLine[LineIdx]));
    }

    public void StartCalculator(YutType Type)
    {
        StartCoroutine(YutStackCalculator(Type));
    }

    public void OnPin()
    {
        Pin1.SetActive(true);
        Pin2.SetActive(true);
    }

    public void OffPin()
    {
        Pin1.SetActive(false);
        Pin2.SetActive(false);
    }

    public void ResetPlayer()
    {
        IsSelectPin = false;
        SelectPin = null;

        GameMgr.Instance.IsPlayerMove = true;
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

        else
            IsCanHideLine = false;

        YutStack.Remove(Type);

        ResetPlayer();
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

        OnPin();

        while (true)
        {
            yield return null;

            if (IsSelectPin == true)
                break;
        }

        OffPin();

        if(SelectPin.name.Equals(Pin1.name))
        {
            yield return null;
        }

        else if(SelectPin.name.Equals(Pin2.name))
        {
            CurLine = HideLine_1.Points;
            LineIdx = 0;
        }

        yield break;
    }

    IEnumerator Move(Point point)
    {
        yield return new WaitForSeconds(0.5f);

        CurPoint = point;
        this.transform.position = CurPoint.transform.position + new Vector3(0, 0.1f, 0);

        yield break;
    }
}
