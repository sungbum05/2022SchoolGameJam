using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YutMgr : MonoBehaviour
{
    public static int FleepCnt = 0;
    public static int ClearYutCnt = 0;

    [SerializeField]
    int FleepCount = 0;
    [SerializeField]
    int ClearYutCount = 0;

    // Update is called once per frame
    void Update()
    {
        if (ClearYutCnt >= 4)
        {
            Debug.Log(FleepCnt);

            GameMgr.Instance.CurTurnPlayer.YutStack.Add((YutType)FleepCnt);
            GameMgr.Instance.IsYutDraw = true;

            FleepCnt = 0;
            ClearYutCnt = 0;
        }
            

        FleepCount = FleepCnt;
        ClearYutCount = ClearYutCnt;
    }
}
