using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update



    

    public UIManager uiManager;

    public int masterVolume = 3;

    AudioSource bgmPlayer;
    AudioSource sfxPlayer;

    [SerializeField]
    AudioClip bgmAudioClip;

    

    [SerializeField]
    AudioClip[] sfxAudioClips;

    Dictionary<string, AudioClip> audioClipDictionary = new Dictionary<string, AudioClip>();  
    void Awake()
    {
        bgmPlayer = GameObject.Find("BGMPlayer").GetComponent<AudioSource>();
        sfxPlayer = GameObject.Find("SFXPlayer").GetComponent<AudioSource>();

        foreach(AudioClip clip in sfxAudioClips)
        {
            audioClipDictionary.Add(clip.name, clip);
        }
        PlayBGM(masterVolume);
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
        bgmPlayer.loop = true;
        bgmPlayer.volume = 1 * (volume / 3);

        bgmPlayer.clip = bgmAudioClip;
        bgmPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        bgmPlayer.volume = 1 * ((float)masterVolume / 3);
        if (Input.GetKeyDown(KeyCode.Space))
            PlaySFX("Yut_Drop1", 3f);
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

    
}
