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

    [SerializeField]
    List<Point> CurLine;
    [SerializeField]
    Point CurPoint;
    [SerializeField]
    bool IsCanHideLine;

    public List<YutType> YutStack;

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

        switch(Type)
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

        for (int i = 0; i < MoveValue; i++)
        {
            StartCoroutine(Move(CurLine[++LineIdx]));
            yield return new WaitForSeconds(0.5f);
        }

        YutStack.Remove(Type);
        GameMgr.Instance.IsPlayerMove = true;

        yield break;
    }

    IEnumerator Move(Point point)
    {
        yield return new WaitForSeconds(0.5f);

        CurPoint = point;
        this.transform.position = CurPoint.transform.position + new Vector3(0,0.1f,0);

        yield break;
    }
}
