using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static SoundManager Instance;

    public UIManager uiManager;

    public int masterVolume = 3;

    AudioSource bgmPlayer;
    AudioSource sfxPlayer;
    AudioSource BellPlayer;

    [SerializeField]
    AudioClip InGameBgmAudioClip;

    [SerializeField]
    AudioClip TitlebgmAudioClip;

    [SerializeField]
    AudioClip BellbgmAudioClip;

    [SerializeField]
    AudioClip[] sfxAudioClips;

    Dictionary<string, AudioClip> audioClipDictionary = new Dictionary<string, AudioClip>();  
    void Awake()
    {
        Instance = this;

        bgmPlayer = GameObject.Find("BGMPlayer").GetComponent<AudioSource>();
        sfxPlayer = GameObject.Find("SFXPlayer").GetComponent<AudioSource>();
        BellPlayer = GameObject.Find("BellPlayer").GetComponent<AudioSource>();

        foreach (AudioClip clip in sfxAudioClips)
        {
            audioClipDictionary.Add(clip.name, clip);
        }
        TitleBGM(masterVolume);

    }

    public void PlaySFX(string name, float volume)
    {
        if(audioClipDictionary.ContainsKey(name) == false)
        {
            Debug.Log(name + " Sound Not Found");
            return;
        }
        sfxPlayer.PlayOneShot(audioClipDictionary[name], 4 * (volume/3));

    }

    public void PlayBGM(float volume)
    {
        bgmPlayer.Stop();
        bgmPlayer.loop = true;
        bgmPlayer.volume = 1 * (volume / 3);
        bgmPlayer.clip = InGameBgmAudioClip;
        bgmPlayer.Play();
    }

    public void TitleBGM(float volume)
    {
        bgmPlayer.loop = true;
        bgmPlayer.volume = 1 * (volume / 3);
        bgmPlayer.clip = TitlebgmAudioClip;
        bgmPlayer.Play();
    }

    public void BellBGM(float volume)
    {
        BellPlayer.loop = false;
        BellPlayer.volume = 1 * (volume / 3);
        BellPlayer.clip = BellbgmAudioClip;
        BellPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        bgmPlayer.volume = 1 * ((float)masterVolume / 3);
        BellPlayer.volume = 1 * ((float)masterVolume / 3);
    }



    public void OpenExplainWindow()
    {
        PlaySFX("Open", masterVolume);
    }

    public void CloseExplainWindow()
    {
       

        PlaySFX("Close", masterVolume);


    }

    public void ClickButton()
    {
        PlaySFX("Button_Click", masterVolume);
    }

    public IEnumerator bellBgm()
    {
        if(uiManager.isStart)
        {
            float randomCool = Random.Range(180, 300);
            BellBGM(masterVolume);
            yield return new WaitForSeconds(randomCool);
        }
        StartCoroutine(bellBgm());
    }

    
}
