using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelect : MonoBehaviour
{
    [SerializeField]
    StartSetting set;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }
    public void OnClick()
    {
        set.test = "/StreamingAssets/csv/Tomita_MapSample2.csv";
        SceneManager.LoadScene("Wa", LoadSceneMode.Single);
        Debug.Log("aaa");

    }


}
