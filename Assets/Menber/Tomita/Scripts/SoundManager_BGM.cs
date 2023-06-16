using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager_BGM : MonoBehaviour
{
    public static SoundManager_BGM Instance { get => _instance; }
    
    static SoundManager_BGM _instance;



    public AudioClip[] Audio_Clip_BGM;

    public float [] Audio_Clip_BGM_Vol;


    public AudioSource Audio_Source_BGM;

    //float volume;

    private void Start()
    {
        _instance = this;
        //ここで流す処理
        Play(0);

    }
    public void Play(int clip)
    {
        Audio_Source_BGM.volume = Audio_Clip_BGM_Vol[clip];
        Audio_Source_BGM.clip = Audio_Clip_BGM[clip];
        Audio_Source_BGM.Play();

    }

    public void Stop()
    {
        
        StartCoroutine(fadeVolue());
       
        
      
    }
    private IEnumerator fadeVolue()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f); 
            Audio_Source_BGM.volume -= 0.01f;
            if (Audio_Source_BGM.volume <= 0)
                break;

        }
        Audio_Source_BGM.Stop();
    }
    private void Update()
    {
      
        //音を止める処理（必要なら）
        if (Input.GetKeyUp(KeyCode.X))
        {
            Stop();
        }
        
    }
}
