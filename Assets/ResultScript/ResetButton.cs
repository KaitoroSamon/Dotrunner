using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetButton : MonoBehaviour
{
    public void Retry()
    {
        
        
            SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        
    }

   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a")|| Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        }
    }
}
