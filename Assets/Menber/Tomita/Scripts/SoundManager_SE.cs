using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager_SE : MonoBehaviour
{
    public static SoundManager_SE Instance { get => _instance; }

    static SoundManager_SE _instance;

    public AudioClip[] Audio_Clip_SE;

    public AudioSource Audio_Source_SE;

    private bool isWalking = false;


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
      //  StartCoroutine(FadeVolume());
    }
/*
    private IEnumerator FadeVolume()
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
*/
    private void Update()
    {

        if (Input.GetKeyUp(KeyCode.Z))
        {
            Play(3);
        }

        if (Input.GetKeyUp(KeyCode.Y))
        {
            Stop();
        }

       
  

        float moveX = Input.GetAxis("Horizontal");  

        if (moveX != 0 && !isWalking)
        {
            isWalking = true;
            Play(4);  
        }
        else if (moveX == 1 && isWalking)
        {
            isWalking = false;
            Stop();  
        }
        else if (moveX == -1 && isWalking)
        {
            isWalking = false;
            Stop();
        }
    }


}
