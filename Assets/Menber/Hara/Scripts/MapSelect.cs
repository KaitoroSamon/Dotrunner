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
    public void TutorialMapOnePlayer()
    {
        tutorialManager.tutorialNow=true;
        set.test = "/StreamingAssets/csv/TutorialMapOnePlayer.csv";
        MoveScene();
    }

    public void BaseMap()
    {
        tutorialManager.tutorialNow=false;
        set.test = "/StreamingAssets/csv/BaseMap.csv";
        MoveScene();
    }

    public void tanaka_map01()
    {
        tutorialManager.tutorialNow=false;
        set.test = "/StreamingAssets/csv/tanaka_map01.csv";
        MoveScene();
    }

    public void Tomita_Map_Sample2()
    {
        tutorialManager.tutorialNow=false;
        set.test = "/StreamingAssets/csv/Tomita_Map_Sample2.csv";
        MoveScene();
    }

    public void Tomita_Map_Sample4()
    {
        tutorialManager.tutorialNow=false;
        set.test = "/StreamingAssets/csv/Tomita_Map_Sample4.csv";
        MoveScene();
    }

    public void TitleBack()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void MoveScene()
    {
        SceneManager.LoadScene("MainScene");
    }

}
