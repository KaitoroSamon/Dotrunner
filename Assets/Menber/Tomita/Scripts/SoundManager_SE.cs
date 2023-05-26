using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager_SE : MonoBehaviour
{
    public static SoundManager_SE Instance { get => _instance; }

    static SoundManager_SE _instance;

    public AudioClip[] Audio_Clip_SE;

    public AudioSource Audio_Source_SE;

    //float volume;

    private void Start()
    {
        _instance = this;
    }
    public void Play(int clip)
    {
       
            Audio_Source_SE.volume = 1;
            Audio_Source_SE.clip = Audio_Clip_SE[clip];
            Audio_Source_SE.Play();
    
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
            Audio_Source_SE.volume -= 0.01f;
            if (Audio_Source_SE.volume <= 0)
                break;

        }
        Audio_Source_SE.Stop();
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Z))
        {
            Play(0);
        }

        if (Input.GetKeyUp(KeyCode.Y))
        {
            Stop();
        }
    }
}
