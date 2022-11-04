using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    public static GameMgr Instance;

    public List<Player> PlayerList;
    public Player CurTurnPlayer;

    public YutMgr YutMgr;
    public List<Yut> Yuts;

    [Header("다음 넘어가기 위한 조건들")]
    public bool IsYutDraw = false;
    public bool IsPlayerMove = false;

    private void Awake()
    {
        Instance = this;
    }

    public void StartGame()
    {
        StartCoroutine(GameRutineSystem());
    }

    IEnumerator GameRutineSystem()
    {
        yield return null;

        for (int i = 0; i < PlayerList.Count; i++)
        {
            PlayerList[i].IsMyTurn = true;
            CurTurnPlayer = PlayerList[i];

            if (CurTurnPlayer.IsEnd == false)
            {
                CurTurnPlayer.MyTurnText.SetActive(true);
                CurTurnPlayer.LastMalImgs.SetActive(false);

                CurTurnPlayer.UpdateSetting();

                #region 윷 던짐
                YutMgr.IsYutDraw = false;

                if (CurTurnPlayer.PlayerType == PlayerType.Robot)
                {
                    while (true)
                    {
                        yield return null;

                        if (GameMgr.Instance.IsYutDraw == false && YutMgr.IsYutDraw == false)
                        {
                            yield return new WaitForSeconds(1.0f);

                            YutMgr.IsYutDraw = true;
                            SoundManager.Instance.PlaySFX("Yut_Drop1", 3f);

                            foreach (var Obj in Yuts)
                            {
                                Obj.DrawYut();
                            }
                        }

                        else if (GameMgr.Instance.IsYutDraw == true)
                            break;
                    }
                }

                YutMgr.IsYutDraw = false;
                foreach (var Obj in Yuts)
                {
                    Obj.IsCanDraw = true;
                }
                //윷을 던졌는가?
                while (true)
                {
                    yield return null;

                    if (IsYutDraw == true)
                        break;
                }
                #endregion

                CurTurnPlayer.StartCalculator();

                #region 플레이어 움직임
                while (true)
                {
                    yield return null;

                    if (IsPlayerMove == true)
                        break;
                }
                #endregion

                IsYutDraw = false;
                IsPlayerMove = false;

                CurTurnPlayer.IsMyTurn = false;
            }
        }

        StartCoroutine(GameRutineSystem());
        yield break;
    }
}
