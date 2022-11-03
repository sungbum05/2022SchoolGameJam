using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.PostProcessing;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    [Header("PostProcessing")]
    [SerializeField]
    PostProcessVolume Volume;
    [SerializeField]
    Vignette Vignette;
    [SerializeField]
    float MinValue, MaxValue;
    [SerializeField]
    float Speed;

    [Header("CameraMove")]
    [SerializeField]
    Vector3 OriginalCameraPos;
    [SerializeField]
    Vector3 YutCameraPos;
    [SerializeField]
    Vector3 FreeCameraPos;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Volume.profile.TryGetSettings(out Vignette);

        StartCoroutine(CameraFade("Up"));
    }

    IEnumerator CameraFade(string Type)
    {
        yield return null;

        switch(Type)
        {
            case "Up":
                while(MaxValue > Vignette.intensity)
                {
                    yield return null;
                    Vignette.intensity.value += Time.deltaTime * Speed;
                }

                StartCoroutine(CameraFade("Down"));
                break;
            case "Down":
                while (MinValue < Vignette.intensity)
                {
                    yield return null;
                    Vignette.intensity.value -= Time.deltaTime * Speed;
                }
                StartCoroutine(CameraFade("Up"));
                break;
        }

        yield break;
    }
}
