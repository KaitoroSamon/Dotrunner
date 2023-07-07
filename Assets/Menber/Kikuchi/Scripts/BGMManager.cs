using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance { get => _instance; }

    static BGMManager _instance;


    public AudioClip[] Audio_Clip_BGM;

    public float Audio_Clip_BGM_Vol;


    public AudioSource Audio_Source_BGM;

    //float volume;

    public enum BGM_TYPE
    {
        TITLE = 0,
        STAGESELECT = 1,
        PLAY = 2,
        END = 3,
    }
    BGM_TYPE bgmType = BGM_TYPE.TITLE;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Play(BGM_TYPE clip)
    {
        Audio_Source_BGM.volume = Audio_Clip_BGM_Vol;
        Audio_Source_BGM.clip = Audio_Clip_BGM[(int)clip];
        Audio_Source_BGM.Play();

    }

    public void BGMVolumeChange(float volume)
    {
        Audio_Source_BGM.volume = volume;
    }
}
