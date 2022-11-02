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
    [SerializeField]
    bool IsOnPlayer;
    [SerializeField]
    bool GoToHide;
    [SerializeField]
    bool IsEnd;
}
