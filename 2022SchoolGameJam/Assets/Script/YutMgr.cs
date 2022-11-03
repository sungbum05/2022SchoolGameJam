using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YutMgr : MonoBehaviour
{
    public static int FleepCnt = 0;
    public static int ClearYutCnt = 0;

    public static bool IsYutDraw = true;

    public static bool IsCatchPlayer = false;
    public static bool IsDoubleDraw = false;

    [SerializeField]
    int FleepCount = 0;
    [SerializeField]
    int ClearYutCount = 0;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && IsYutDraw == false)
        {
            SoundManager.Instance.PlaySFX("Yut_Drop1", 3f);
            IsYutDraw = true;
        }

        if (ClearYutCnt >= 4)
        {
            GameMgr.Instance.CurTurnPlayer.YutStack.Add((YutType)FleepCnt);

            if (FleepCnt > 0 && FleepCnt < 4)
            {
                GameMgr.Instance.IsYutDraw = true;
            }

            else
            {
                foreach(Transform Obj in this.gameObject.transform)
                {
                    Obj.gameObject.GetComponent<Yut>().RetryDrawYut();
                }

                IsYutDraw = false;
            }

            FleepCnt = 0;
            ClearYutCnt = 0;
        }

        #region À· ¸¾´ë·Î Á¶Á¤ Ä¡Æ®
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            FleepCnt = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            FleepCnt = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            FleepCnt = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            FleepCnt = 4;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            FleepCnt = 0;
        }
        #endregion

        FleepCount = FleepCnt;
        ClearYutCount = ClearYutCnt;
    }
}
