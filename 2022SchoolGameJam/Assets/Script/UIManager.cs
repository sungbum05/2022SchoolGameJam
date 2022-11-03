using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button SoundButton;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnStart()
    {

    }

    void HowToPlay()
    {

    }

    void OnExit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        
        Application.Quit();
    }
}
