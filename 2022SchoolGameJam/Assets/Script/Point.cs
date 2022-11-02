using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerNum
{
    Null, Player1, Player2, Player3
}

public class Point : MonoBehaviour
{
    [SerializeField]
    int PointNumber;
    [SerializeField]
    PlayerNum PlayerNum;


    public LineClass HideLine_1;
    public LineClass HideLine_2;

    public bool IsOnPlayer;
    public bool IsGoToHide;
    public bool IsEnd;
}
