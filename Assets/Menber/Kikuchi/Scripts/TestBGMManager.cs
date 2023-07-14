/*using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    private bool isDisplay;
    [SerializeField]
    GameObject Volume;
    VolumeController volumeController;
    public static BGMManager Instance { get => _instance; }

    static BGMManager _instance;


    public AudioClip[] Audio_Clip_BGM;

    [HideInInspector]
    public float Audio_Clip_BGM_Vol = 1;


    public AudioSource Audio_Source_BGM;
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
            volumeController = Volume.GetComponent<VolumeController>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Play(BGMManager.BGM_TYPE.TITLE);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isDisplay == false)
            {
                Volume.gameObject.SetActive(true);
                isDisplay = true;
            }
            else
            {
                Volume.gameObject.SetActive(false);
                isDisplay = false;
            }
            
        }
    }


    public void Play(BGM_TYPE clip)
    {
        BGMVolumeChange(Audio_Clip_BGM_Vol);
        Audio_Source_BGM.clip = Audio_Clip_BGM[(int)clip];
        Audio_Source_BGM.Play();
    }

    public void BGMVolumeChange(float volume)
    {
        Debug.Log(volume);
        Audio_Source_BGM.volume = volume;
        volumeController.TileChange(volume);
    }
}
*/