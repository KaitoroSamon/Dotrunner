using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance { get => _instance; }

    static BGMManager _instance;


    public AudioClip[] Audio_Clip_BGM;

    public AudioSource Audio_Source_BGM;

    //float volume;

    public enum BGM_TYPE
    {
        TITLE = 0,
        STAGESELECT = 1,
        PLAY = 2,
        END = 3,
    }

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

    private void Start()
    {
        PlayBGM(BGM_TYPE.TITLE,0.6f);
    }

    public void PlayBGM(BGM_TYPE clip ,float BGMVol)
    {
        if(Audio_Clip_BGM[(int)clip]!= null)
        {
            Audio_Source_BGM.volume = BGMVol;
            Audio_Source_BGM.clip = Audio_Clip_BGM[(int)clip];
            Audio_Source_BGM.Play();
        }
        else
        {
            Debug.Log("BGMクリップがありません");
        }
        

    }

    public void BGMVolumeChange(float volume)
    {
        Audio_Source_BGM.volume = volume;
    }

    /*
       private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayBGM(BGM_TYPE.PLAY,0.6f);
        }
    }
    */
}
