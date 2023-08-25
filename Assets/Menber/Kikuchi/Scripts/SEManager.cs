using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    public static SEManager Instance { get => _instance; }

    static SEManager _instance;

    
    public AudioClip[] Audio_Clip_SE;

    public AudioSource Audio_Source_SE;

    public float Audio_Source_SE_Vol;


    public enum SE_TYPE
    {
        Select = 0,
        CURSORMOVE = 1,
        BUCKET = 2,
    }
    private void Start()
    {
        _instance = this;
     
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

    public void PlaySE(SE_TYPE clip)
    {
        if(Audio_Clip_SE[(int)clip] != null)
        {
            Audio_Source_SE.clip = Audio_Clip_SE[(int)clip];
            Audio_Source_SE.PlayOneShot(Audio_Clip_SE[(int)clip]);
        }
        else
        {
            Debug.Log("éwíËÇ≥ÇÍÇΩSEÇ™Ç†ÇËÇ‹ÇπÇÒÅB");
        }
    }

    public void SEVolumeChange(float volume)
    {
        Audio_Source_SE.volume = volume;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Z))
        {
            SEManager.Instance.PlaySE(SEManager.SE_TYPE.Select);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            SEManager.Instance.PlaySE(SEManager.SE_TYPE.CURSORMOVE);
        }
    }


}
