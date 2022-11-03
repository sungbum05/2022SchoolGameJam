using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioSource howToPlay;
    public AudioSource close;

    public UIManager uiManager;

    int soundStep = 3;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenExplainWindow()
    {
        howToPlay.volume = 1 * (soundStep/3);
        
        howToPlay.Play();
    }

    public void CloseExplainWindow()
    {
        close.volume = 1 * (soundStep / 3);

        close.Play();
       
        
    }

    
}
