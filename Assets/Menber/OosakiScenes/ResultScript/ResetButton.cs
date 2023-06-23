using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetButton : MonoBehaviour
{
    public void Retry()
    {


        SceneManager.LoadScene("TitleScene", LoadSceneMode.Single);

    }



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        string name = "DS4-1"; //“o˜^‚µ‚½–¼‘O
        if (Input.GetButtonDown(name))
        {
            SceneManager.LoadScene("TitleScene", LoadSceneMode.Single);
        }
    }
}
