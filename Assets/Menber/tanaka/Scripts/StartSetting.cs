using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class StartSetting : MonoBehaviour
{

    public static int ColumnNumber;
    public static int LineNumber;
    public static string[,] fieldMap;

    public static StartSetting startSetting;

    private List<string[]> csvData = new List<string[]>();

    public static string test = "/StreamingAssets/csv/tanaka_map01.csv";

    /*
    "/StreamingAssets/csv/TutorialMapOnePlayer.csv"
    "/StreamingAssets/csv/BaseMap.csv"
    "/StreamingAssets/csv/tanaka_map01.csv"
    "/StreamingAssets/csv/Tomita_Map_Sample5.csv"
    "/StreamingAssets/csv/Tomita_Map_Sample4.csv"
    */

    void Awake()
    {
        ColumnNumber = 0;
        LineNumber = 0;
        
        if (tutorialManager.tutorialNow)
        {
            test = "/StreamingAssets/csv/TutorialMapOnePlayer.csv";
        }

#if UNITY_EDITOR
        StreamReader fs = 
            new StreamReader(Application.dataPath + test);
        /*
#elif UNITY_STANDALONE_OSX
            StreamReader fs = 
            new StreamReader(Application.dataPath + "/Resources/Data/StreamingAssets/csv/EnemySpown.csv");  
        */
#elif UNITY_STANDALONE_WIN
            StreamReader fs = 
            new StreamReader(Application.dataPath + test);    
#endif
        {
            while (fs.Peek() != -1)
            {
                var str = fs.ReadLine();
                csvData.Add(str.Split(','));
            }
            ColumnNumber = csvData[0].Length;
            LineNumber = csvData.Count;
            fieldMap = new string[LineNumber, ColumnNumber];

            Debug.Log("Col" + ColumnNumber + "\nLine" + LineNumber);
            for(int i = 0; i < LineNumber; i++)
            {
                string[] tempWord = csvData[i];
                for(int j = 0; j < ColumnNumber; j++)
                {
                    fieldMap[i,j] = tempWord[j];
                }
            }
            //Debug.Log(string.Join(",", StartSetting.fieldMap.Cast<string>()));
        }

        fs.Close();
        csvData.Clear();
    }

    /*
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    */
}
