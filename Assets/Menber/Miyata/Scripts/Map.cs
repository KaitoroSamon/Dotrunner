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
                            break;
                    }
                }
            }
        }

        //横山加筆
        //アイテム生成の処理
        //ポーション
        for(int i = 1; i <= 10; i++)
        {
            CreateItem(0, 8, 0, 16, "3");
        }
        //バケツ
        for (int i = 1; i <= 5; i++)
        {
            CreateItem(0, 8, 0, 16, "4");
        }
        //爆弾
        for (int i = 1; i <= 5; i++)
        {
            CreateItem(3, 6, 0, 16, "5");
        }
    }

    //田中加筆
    public bool paintRedMap(Vector2 pos)
    {
        bool paint = false;
        Debug.Log("塗り");
        for(int i = 0; i < LineNumber; i++)
        {
            for (int j = 0; j < ColumnNumber; j++)
            {
                //横山一部変更　jとiが逆だったので変更
                if (i == pos.y && j == pos.x)
                {
                    //隣接したマスに何もなければ何もしない
                    if (dungeonMap[i,j+1] == "1" || dungeonMap[i+1, j] == "1" || dungeonMap[i, j - 1] == "1" || dungeonMap[i - 1, j] == "1" ||
                        dungeonMap[i, j + 1] == "6" || dungeonMap[i + 1, j] == "6" || dungeonMap[i, j - 1] == "6" || dungeonMap[i - 1, j] == "6")
                    {
                        formerData = int.Parse(dungeonMap[i, j]);
                        //対応した色に塗る
                        dungeonMap[i, j] = "1";
                        //横山加筆
                        if (GameManager.rePaint_p1 >= 1)
                        {
                            GameManager.rePaint_p1--;
                        }
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
                //横山加筆
                for (int i = 0; i < LineNumber; i++)
                {
                    for (int j = 0; j < ColumnNumber; j++)
                    {
                        if (i == pos.y && j == pos.x)
                        {
                            //上
                            if (dungeonMap[i - 1, j] != "6")
                            {
                                dungeonMap[i - 1, j] = "1";
                                Instantiate(Player1Square, new Vector3(-7.5f + j, 3.5f - (i - 1), 0), Quaternion.identity);
                            }
                            //下
                            if (dungeonMap[i + 1, j] != "6")
                            {
                                dungeonMap[i + 1, j] = "1";
                                Instantiate(Player1Square, new Vector3(-7.5f + j, 3.5f - (i + 1), 0), Quaternion.identity);
                            }
                            //左
                            if (dungeonMap[i, j - 1] != "6" && j != 0)
                            {
                                dungeonMap[i, j - 1] = "1";
                                Instantiate(Player1Square, new Vector3(-7.5f + (j - 1), 3.5f - i, 0), Quaternion.identity);
                            }
                            //左斜め上
                            if (dungeonMap[i - 1, j - 1] != "6" && j != 0)
                            {
                                dungeonMap[i - 1, j - 1] = "1";
                                Instantiate(Player1Square, new Vector3(-7.5f + (j - 1), 3.5f - (i - 1), 0), Quaternion.identity);
                            }
                            //左斜め下
                            if (dungeonMap[i + 1, j - 1] != "6" && j != 0)
                            {
                                dungeonMap[i + 1, j - 1] = "1";
                                Instantiate(Player1Square, new Vector3(-7.5f + (j - 1), 3.5f - (i + 1), 0), Quaternion.identity);
                            }
                            //右
                            if (dungeonMap[i, j + 1] != "6" && j != 16)
                            {
                                dungeonMap[i, j + 1] = "1";
                                Instantiate(Player1Square, new Vector3(-7.5f + (j + 1), 3.5f - i, 0), Quaternion.identity);
                            }
                            //右斜め上
                            if (dungeonMap[i - 1, j + 1] != "6" && j != 16)
                            {
                                dungeonMap[i - 1, j + 1] = "1";
                                Instantiate(Player1Square, new Vector3(-7.5f + (j + 1), 3.5f - (i - 1), 0), Quaternion.identity);
                            }
                            //右斜め下
                            if (dungeonMap[i + 1, j + 1] != "6" && j != 16)
                            {
                                dungeonMap[i + 1, j + 1] = "1";
                                Instantiate(Player1Square, new Vector3(-7.5f + (j + 1), 3.5f - (i + 1), 0), Quaternion.identity);
                            }
                        }
                    }
                }
                break;
            case 6:

            default : break;
        }

        return paint;
    }

    //横山加筆
    public bool paintBlueMap(Vector2 pos)
    {
        bool paint = false;
        Debug.Log("塗り");
        for (int i = 0; i < LineNumber; i++)
        {
            for (int j = 0; j < ColumnNumber; j++)
            {
                if (i == pos.y && j == pos.x)
                {
                    //隣接したマスに何もなければ何もしない
                    if (dungeonMap[i, j + 1] == "2" || dungeonMap[i + 1, j] == "2" || dungeonMap[i, j - 1] == "2" || dungeonMap[i - 1, j] == "2" ||
                        dungeonMap[i, j + 1] == "6" || dungeonMap[i + 1, j] == "6" || dungeonMap[i, j - 1] == "6" || dungeonMap[i - 1, j] == "6")
                    {
                        formerData = int.Parse(dungeonMap[i, j]);
                        //対応した色に塗る
                        dungeonMap[i, j] = "2";
                        //横山加筆
                        if (GameManager.rePaint_p2 >= 1)
                        {
                            GameManager.rePaint_p2--;
                        }
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
                for (int i = 0; i < LineNumber; i++)
                {
                    for (int j = 0; j < ColumnNumber; j++)
                    {
                        if (i == pos.y && j == pos.x)
                        {
                            //上
                            if (dungeonMap[i - 1, j] != "6")
                            {
                                dungeonMap[i - 1, j] = "2";
                                Instantiate(Player2Square, new Vector3(-7.5f + j, 3.5f - (i - 1), 0), Quaternion.identity);
                            }
                            //下
                            if (dungeonMap[i + 1, j] != "6")
                            {
                                dungeonMap[i + 1, j] = "2";
                                Instantiate(Player2Square, new Vector3(-7.5f + j, 3.5f - (i + 1), 0), Quaternion.identity);
                            }
                            //左
                            if (dungeonMap[i, j - 1] != "6" && j != 0)
                            {
                                dungeonMap[i, j - 1] = "2";
                                Instantiate(Player2Square, new Vector3(-7.5f + (j - 1), 3.5f - i, 0), Quaternion.identity);
                            }
                            //左斜め上
                            if (dungeonMap[i - 1, j - 1] != "6" && j != 0)
                            {
                                dungeonMap[i - 1, j - 1] = "2";
                                Instantiate(Player2Square, new Vector3(-7.5f + (j - 1), 3.5f - (i - 1), 0), Quaternion.identity);
                            }
                            //左斜め下
                            if (dungeonMap[i + 1, j - 1] != "6" && j != 0)
                            {
                                dungeonMap[i + 1, j - 1] = "2";
                                Instantiate(Player2Square, new Vector3(-7.5f + (j - 1), 3.5f - (i + 1), 0), Quaternion.identity);
                            }
                            //右
                            if (dungeonMap[i, j + 1] != "6" && j != 16)
                            {
                                dungeonMap[i, j + 1] = "2";
                                Instantiate(Player2Square, new Vector3(-7.5f + (j + 1), 3.5f - i, 0), Quaternion.identity);
                            }
                            //右斜め上
                            if (dungeonMap[i - 1, j + 1] != "6" && j != 16)
                            {
                                dungeonMap[i - 1, j + 1] = "2";
                                Instantiate(Player2Square, new Vector3(-7.5f + (j + 1), 3.5f - (i - 1), 0), Quaternion.identity);
                            }
                            //右斜め下
                            if (dungeonMap[i + 1, j + 1] != "6" && j != 16)
                            {
                                dungeonMap[i + 1, j + 1] = "2";
                                Instantiate(Player2Square, new Vector3(-7.5f + (j + 1), 3.5f - (i + 1), 0), Quaternion.identity);
                            }
                        }
                    }
                }
                break;
            case 6:

            default: break;
        }

        return paint;
    }

    //横山加筆
    //アイテムをランダムで生成するためのメソッド
    void CreateItem(int tate_min, int tate_max, int yoko_min, int yoko_max, string Item)
    {
        while(true)
        {
            int Tate = Random.Range(tate_min, tate_max);
            int Yoko = Random.Range(yoko_min, yoko_max);

            //何もマップに置いて無かったら
            if(dungeonMap[Tate, Yoko] == "0")
            {
                dungeonMap[Tate, Yoko] = Item;

                switch (dungeonMap[Tate, Yoko])
                {
                    case "3"://ポーション
                        Instantiate(Item1Square, new Vector3(-7.5f + Yoko, 3.5f - Tate, 0), Quaternion.identity);
                        break;

                    case "4"://バケツ
                        Instantiate(Item2Square, new Vector3(-7.5f + Yoko, 3.5f - Tate, 0), Quaternion.identity);
                        break;

                    case "5"://爆弾
                        Instantiate(Item3Square, new Vector3(-7.5f + Yoko, 3.5f - Tate, 0), Quaternion.identity);
                        break;
                }

                //処理を抜ける
                break;
            }
        }
    }
}
