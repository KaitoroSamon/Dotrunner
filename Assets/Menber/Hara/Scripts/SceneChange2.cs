using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("DS4circle"))
        {
            TitleChange();
        }

        if (Input.GetButtonDown("DS4circle2"))
        {
            TitleChange();
        }
    }
    public void TitleChange()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }
}
