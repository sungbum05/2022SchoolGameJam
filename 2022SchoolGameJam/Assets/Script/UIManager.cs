using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image SoundButton;
    public Sprite[] SoundStack = new Sprite[4];

    public bool isStart = false;

    [SerializeField]
    SoundManager soundManager;

    public GameObject explain;

    public GameObject gameTitle;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnStart()
    {
        isStart = true;
        gameTitle.SetActive(false);
        soundManager.PlayBGM(soundManager.masterVolume);
        StartCoroutine(soundManager.bellBgm());
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
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
