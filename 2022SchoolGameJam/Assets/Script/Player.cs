using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType
{
    Human, Robot
}

public enum YutType
{
    모, 도, 개, 걸, 윷
}

public class Player : MonoBehaviour
{
    public PlayerType PlayerType;

    public bool IsMyTurn = false;
    public bool IsStart = false;
    public bool IsEnd = false;

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

            if (Physics.Raycast(ray, out hit, 15.0f, layerMask))
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

    #region On/Off Pin
    public void OnPin1()
    {
        Pin1.SetActive(true);
    }

    public void OnPin2()
    {
        Pin2.SetActive(true);
    }

    public void OffPin()
    {
        Pin1.SetActive(false);
        Pin2.SetActive(false);
    }
    #endregion

    public void ResetPlayer()
    {
        IsSelectPin = false;
        SelectPin = null;

        GameMgr.Instance.IsPlayerMove = true;
    }

    IEnumerator YutStackCalculator(YutType Type)
    {
        yield return null;

        int MoveValue = 0;

        switch (Type)
        {
            case YutType.도:
                MoveValue = 1;
                break;

            case YutType.개:
                MoveValue = 2;
                break;

            case YutType.걸:
                MoveValue = 3;
                break;

            case YutType.윷:
                MoveValue = 4;
                break;

            case YutType.모:
                MoveValue = 5;
                break;
        }

        yield return StartCoroutine(PinMovePos(MoveValue));

        if (IsEnd == true)
        {
            CurPoint = Board.Instance.EndPoint;
            this.transform.position = CurPoint.transform.position;

            LineIdx = 11;

            if (CurPoint.IsGoToHide == true)
                IsCanHideLine = true;

            else
                IsCanHideLine = false;

            YutStack.Remove(Type);

            ResetPlayer();
            yield break;
        }

        else
        {
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
    }

    IEnumerator PinMovePos(int MoveValue)
    {
        yield return null;

        //MiddlePoint가 아닐 때
        if (CurPoint.IsMiddlePoint == false)
        {
            #region 샛길 계산
            if (IsCanHideLine == true)
            {
                int HideLineIdx = 0;

                HideLine_1 = CurPoint.HideLine_1;
                HideLine_2 = CurPoint.HideLine_2;

                Pin2.transform.position = HideLine_1.Points[HideLineIdx + MoveValue].transform.position;
                OnPin2();
            }
            #endregion

            if (CurLine.Count > LineIdx + MoveValue)
            {
                Pin1.transform.position = CurLine[LineIdx + MoveValue].transform.position;
            }

            else
            {
                IsEnd = true;
                Pin1.transform.position = Board.Instance.EndPoint.transform.position;
            }

            OnPin1();

            while (true)
            {
                yield return null;

                if (IsSelectPin == true)
                    break;
            }

            OffPin();

            if (SelectPin.name.Equals(Pin1.name))
            {
                yield return null;
            }

            else if (SelectPin.name.Equals(Pin2.name))
            {
                CurLine = HideLine_1.Points;
                LineIdx = 0;
            }
        }

        //MiddlePoint가 맞을 때
        else if(CurPoint.IsMiddlePoint == true)
        {
            #region 첫번째 경로
            int HideLineIdx_1 = 0;

            HideLine_1 = CurPoint.HideLine_1;

            if (HideLine_1.Points.Count < HideLineIdx_1 + MoveValue)
            {
                Pin1.transform.position = Board.Instance.EndPoint.transform.position;
            }

            else
            {
                Pin1.transform.position = HideLine_1.Points[HideLineIdx_1 + MoveValue].transform.position;
            }

            OnPin1();
            #endregion

            #region 두번째 경로
            int HideLineIdx_2 = 0;

            HideLine_2 = CurPoint.HideLine_2;

            if (HideLine_2.Points.Count < HideLineIdx_2 + MoveValue)
            {
                Pin2.transform.position = Board.Instance.EndPoint.transform.position;
            }

            else
            {
                Pin2.transform.position = HideLine_2.Points[HideLineIdx_2 + MoveValue].transform.position;
            }

            OnPin2();
            #endregion

            while (true)
            {
                yield return null;

                if (IsSelectPin == true)
                    break;
            }

            OffPin();

            if (SelectPin.name.Equals(Pin1.name))
            {
                CurLine = HideLine_1.Points;
                LineIdx = 0;
            }

            else if (SelectPin.name.Equals(Pin2.name))
            {
                CurLine = HideLine_2.Points;
                LineIdx = 0;
            }
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
