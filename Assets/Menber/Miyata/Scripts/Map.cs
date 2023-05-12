using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField]
    private TextAsset textFile;
 
    private string[] textData;
    private string[,] dungeonMap;
 
    private int LineNumber; // 行数に相当
    private int ColumnNumber; // 列数に相当
 
    [SerializeField]
    private GameObject NothingSquare;
    [SerializeField]
    private GameObject Player1Square;
    [SerializeField]
    private GameObject Player2Square;
    [SerializeField]
    private GameObject Item1Square;
    [SerializeField]
    private GameObject Item2Square;
    [SerializeField]
    private GameObject Item3Square;
    [SerializeField]
    private GameObject GoalSquare;
 
    private void Start()
    {
        string textLines = textFile.text; // テキストの全体データの代入
        print(textLines);
 
        // 改行でデータを分割して配列に代入
        textData = textLines.Split('\n');
 
        // 行数と列数の取得
        ColumnNumber = textData[0].Split(',').Length;
        LineNumber = textData.Length;
 
        print("tate" + LineNumber);
        print("yoko" + ColumnNumber);
 
        // ２次元配列の定義
        dungeonMap = new string[LineNumber, ColumnNumber];
 
        for(int i = 0; i < LineNumber; i++)
        {
            string[] tempWords = textData[i].Split(',');
 
            for(int j = 0; j < ColumnNumber; j++)
            {
                dungeonMap[i, j] = tempWords[j];
 
                if(dungeonMap[i,j] != null)
                {
                    switch (dungeonMap[i,j])
                    {
                        case "0":
                            Instantiate(NothingSquare, new Vector3(-7.5f + j, 3.5f - i, 0), Quaternion.identity);
                            break;
 
                        case "1":
                            Instantiate(Player1Square, new Vector3(-7.5f + j, 3.5f - i, 0), Quaternion.identity);
                            break;
 
                        case "2":
                            Instantiate(Player2Square, new Vector3(-7.5f + j, 3.5f - i, 0), Quaternion.identity);
                            break;
                        
                        case "3":
                            Instantiate(Item1Square, new Vector3(-7.5f + j, 3.5f - i, 0), Quaternion.identity);
                            break;

                        case "4":
                            Instantiate(Item2Square, new Vector3(-7.5f + j, 3.5f - i, 0), Quaternion.identity);
                            break;

                        case "5":
                            Instantiate(Item3Square, new Vector3(-7.5f + j, 3.5f - i, 0), Quaternion.identity);
                            break;

                        case "6":
                            Instantiate(GoalSquare, new Vector3(-7.5f + j, 3.5f - i, 0), Quaternion.identity);
                            break;
                    }
                }
            }
        }
    }
}
