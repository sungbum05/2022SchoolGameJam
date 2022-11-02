using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
class HideLineClass
{
    [SerializeField]
    string LineName;
    public List<Point> HidePoint;

    public HideLineClass(string lineName)
    {
        LineName = lineName;
    }
}

public class Board : MonoBehaviour
{
    [SerializeField]
    Transform BasicLines;
    [SerializeField]
    List<Point> BasicLine;
    [SerializeField]
    Transform HideLines;
    [SerializeField]
    List<HideLineClass> HideLine;
    [SerializeField]
    Point MiddlePoint;

    private void Awake()
    {
        foreach (Transform Parent in BasicLines)
        {
            foreach(Transform Line in Parent)
            {
                BasicLine.Add(Line.GetComponent<Point>());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
