using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField]
    int PointNumber;

    public Player OnPlayer;

    public LineClass HideLine_1;
    public LineClass HideLine_2;

    public bool IsOnPlayer;
    public bool IsGoToHide;
    public bool IsMiddlePoint;
    public bool IsEnd;
}
