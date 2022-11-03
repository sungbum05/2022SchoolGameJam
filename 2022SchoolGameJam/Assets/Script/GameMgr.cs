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

    [Header("���� �Ѿ�� ���� ���ǵ�")]
    public bool IsYutDraw = false;
    public bool IsPlayerMove = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
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
            CurTurnPlayer.UpdateSetting();

            #region �� ����
            if(CurTurnPlayer.PlayerType == PlayerType.Robot)
            {
                yield return new WaitForSeconds(1.0f);

                foreach(var Obj in Yuts)
                {
                    Obj.DrawYut();
                }
            }

            foreach (var Obj in Yuts)
            {
                Obj.IsCanDraw = true;
            }
            //���� �����°�?
            while (true)
            {
                yield return null;

                if (IsYutDraw == true)
                    break;
            }
            #endregion

            CurTurnPlayer.StartCalculator(CurTurnPlayer.YutStack[0]);

            #region �÷��̾� ������
            while(true)
            {
                yield return null;

                if (IsPlayerMove == true)
                    break;
            }
            #endregion

            IsYutDraw = false;
            IsPlayerMove = false;
        }

        StartCoroutine(GameRutineSystem());
        yield break;
    }
}
