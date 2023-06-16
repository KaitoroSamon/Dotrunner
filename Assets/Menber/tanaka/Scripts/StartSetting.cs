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

    private List<string[]> csvData = new List<string[]>();  //CSV�t�@�C���̒��g�����郊�X�g

    //test�̏���public static �Ń}�b�v�I���̂ق��Ŏ��R�ɕύX����
    ///StreamingAssets/csv/�I�������}�b�v��.csv�@�ŕύX��
    private string test = "/StreamingAssets/csv/BaseMap.csv";

    void Awake()
    {
        // Path���ς��̂ŕ���
#if UNITY_EDITOR
        StreamReader fs = 
            new StreamReader(Application.dataPath + test);
#elif UNITY_STANDALONE_OSX
            StreamReader fs = 
            new StreamReader(Application.dataPath + "/Resources/Data/StreamingAssets/csv/EnemySpown.csv");    
#elif UNITY_STANDALONE_WIN
            StreamReader fs = 
            new StreamReader(Application.dataPath + "/StreamingAssets/csv/EnemySpown.csv");    
#endif
        {
            while (fs.Peek() != -1)
            {
                var str = fs.ReadLine();
                csvData.Add(str.Split(','));
            }
            ColumnNumber = csvData[0].Length;
            LineNumber = csvData[0].Length;
            fieldMap = new string[LineNumber, ColumnNumber];

            for(int i = 0; i < LineNumber; i++)
            {
                string[] tempWord = csvData[i];
                for(int j = 0; j < ColumnNumber; j++)
                {
                    fieldMap[i,j] = tempWord[j];
                }
            }
            
        }
        //�L���b�V���΍�
        fs.Close();
        csvData.Clear();
    }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
