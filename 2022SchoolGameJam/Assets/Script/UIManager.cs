using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance; 

    public Image SoundButton;
    public Sprite[] SoundStack = new Sprite[4];

    public bool isStart = false;

    [SerializeField]
    SoundManager soundManager;

    public GameObject explain;

    public GameObject gameTitle;
    public Text EndingText;
    public GameObject GameEndingPan;

    public GameObject PlayerIcons;
    public Text GuideText;

    public Ghost Ghost1;
    public Ghost Ghost2;

    private void Awake()
    {
        Instance = this;
    }

    public void OnStart()
    {
        isStart = true;
        gameTitle.SetActive(false);
        soundManager.PlayBGM(soundManager.masterVolume);
        StartCoroutine(soundManager.bellBgm());
        GameMgr.Instance.StartGame();

        Ghost1.ResetCorutine();
        Ghost2.ResetCorutine();

        PlayerIcons.SetActive(true);
        OnGuideText("???? ????!");
    }

    public void OnWinPan()
    {
        EndingText.text = "?¸??ϼ̽??ϴ?!";
        GameEndingPan.SetActive(true);
    }

    public void OnLosePan()
    {
        EndingText.text = "?й??ϼ̽??ϴ?..";
        GameEndingPan.SetActive(true);
    }

    public void OnRestart()
    {
        SceneManager.LoadScene(0);
    }

    public void ClickSound()
    {
        switch(soundManager.masterVolume)
            {
                case 0:
                    soundManager.masterVolume = 3;
                break;
                case 1:
                    soundManager.masterVolume--;
                break;
            case 2:
                soundManager.masterVolume--;
                break;
            case 3:
                soundManager.masterVolume--;
                break;
        }
        SoundButton.sprite = SoundStack[soundManager.masterVolume];
        
    }

    public void HowToPlay()
    {
        explain.SetActive(true);

    }

    public void Close()
    {
        explain?.SetActive(false);
    }

    public void OnExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // ???ø????̼? ????
#endif
    }

    public void OnGuideText(string Content)
    {
        GuideText.text = Content;
        GuideText.DOFade(1, 0);

        GuideText.gameObject.SetActive(true);
        StartCoroutine(FadeGuideText());
    }

    IEnumerator FadeGuideText()
    {
        yield return null;

        GuideText.DOFade(0, 3);

        yield return new WaitForSeconds(3.0f);
        GuideText.gameObject.SetActive(false);

        yield break;
    }
}
