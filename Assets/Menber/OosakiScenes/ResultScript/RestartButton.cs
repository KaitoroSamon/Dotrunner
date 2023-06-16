using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public void Reset()
    {


        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);

    }



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("b") || Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
        }
    }
}
