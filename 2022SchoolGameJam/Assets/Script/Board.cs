using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LineClass
{
    [SerializeField]
    string LineName;
    public List<Point> Points;

    public LineClass(string lineName)
    {
        LineName = lineName;
    }
}

public class Board : MonoBehaviour
{
    public static Board Instance;

    [SerializeField]
    Transform BasicLines;

    public List<LineClass> BasicLine;

    [SerializeField]
    Transform HideLines;

    public List<LineClass> HideLine;
    public Point MiddlePoint;
    public Point EndPoint;
    public Point OutPoint;

    private void Awake()
    {
        Instance = this;

        foreach (Transform Parent in BasicLines)
        {
            foreach(Transform Line in Parent)
            {
                BasicLine[0].Points.Add(Line.GetComponent<Point>());
            }
        }

        BasicLine[0].Points.Add(BasicLines.GetChild(0).GetChild(0).GetComponent<Point>());
    }
}
