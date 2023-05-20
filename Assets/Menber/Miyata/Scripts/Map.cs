using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField]
    public TextAsset textFile; //マップの情報取得
 
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

    //田中さん加筆
    //塗った元のマスのデータ保持
    private int formerData = default;
    GameManager gameManager;
    PlayerManager playerManager;
    [SerializeField]
    GameObject gameManagerScripts;
    [SerializeField]
    GameObject playerManagerScripts;
    int count = 0;

    private void Awake()
    {
        gameManager = gameManagerScripts.GetComponent<GameManager>();
        playerManager = playerManagerScripts.GetComponent<PlayerManager>();


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
                        case "0"://何もなし
                            Instantiate(NothingSquare, new Vector3(-7.5f + j, 3.5f - i, 0), Quaternion.identity);
                            break;
 
                        case "1"://プレイヤー1の色
                            Instantiate(Player1Square, new Vector3(-7.5f + j, 3.5f - i, 0), Quaternion.identity);
                            break;
 
                        case "2"://プレイヤー2の色
                            Instantiate(Player2Square, new Vector3(-7.5f + j, 3.5f - i, 0), Quaternion.identity);
                            break;
                        
                        case "3"://ポーション
                            Instantiate(Item1Square, new Vector3(-7.5f + j, 3.5f - i, 0), Quaternion.identity);
                            break;

                        case "4"://バケツ
                            Instantiate(Item2Square, new Vector3(-7.5f + j, 3.5f - i, 0), Quaternion.identity);
                            break;

                        case "5"://爆弾
                            Instantiate(Item3Square, new Vector3(-7.5f + j, 3.5f - i, 0), Quaternion.identity);
                            break;

                        case "6"://ゴール
                            Instantiate(GoalSquare, new Vector3(-7.5f + j, 3.5f - i, 0), Quaternion.identity);
                            if(count == 0)
                            {
                                playerManager.PlayerUpdate(new Vector2(-7.5f + j, 3.5f - i));
                                count++;
                            }
                            else{

                            }
                            
                            break;
                    }
                }
            }
        }
    }

    //田中加筆
    public bool paintRedMap(Vector2 pos)
    {
        pos = positionConverter(pos);
        Debug.Log("["+pos.x+ "]" + "["+pos.y+"]");
        bool paint = false;
        for(int i = 0; i < ColumnNumber; i++)
        {
            for (int j = 0; j < LineNumber; j++)
            {
                if (i == pos.x && j == pos.y)
                {
                    Debug.Log("あった！");
                    //隣接したマスに何もなければ何もしない
                    if (dungeonMap[i+1,j] == "1" || dungeonMap[i, j+1] == "1" || dungeonMap[i - 1, j] == "1" || dungeonMap[i, j - 1] == "1" ||
                        dungeonMap[i + 1, j] == "6" || dungeonMap[i, j + 1] == "6" || dungeonMap[i - 1, j] == "6" || dungeonMap[i, j - 1] == "6")
                    {
                        Debug.Log("塗った!");
                        formerData = int.Parse(dungeonMap[i, j]);
                        //対応した色に塗る
                        dungeonMap[i, j] = "1";
                        paint = true;
                    }
                }
            }
        }

        //アイテムと重なった処置
        switch (formerData)
        {
            case 3:
                gameManager.addMoveCounter();
                break;
            case 4:
                gameManager.addRePaint();
                break;
            case 5:
                //爆弾の処理
                break;
            case 6:

            default : break;
        }

        return paint;
    }

    public Vector2 positionConverter(Vector2 pos)
    {
        //  x/y   x   y
        //  0/0 -7.5 3.5
        //  0/8 -7.5 -4.5
        //  15/0 7.5 3.5
        //  15/8 7.5 -4.5

        //(0,0)を起点にする
        pos = new Vector2(pos.x + 7.5f,pos.y-3.5f);
        return pos;
        
    }
}
