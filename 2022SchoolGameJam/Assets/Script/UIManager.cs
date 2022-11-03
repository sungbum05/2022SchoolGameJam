using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button SoundButton;

    public GameObject explain;
    
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
