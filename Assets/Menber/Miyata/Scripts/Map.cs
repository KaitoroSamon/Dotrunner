//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    RedPlayerManager RedPlayerManager;
    BluePlayerManager BluePlayerManager;
    [SerializeField]
    GameObject gameManagerScripts;
    [SerializeField]
    GameObject RedPlayerManagerScripts;
    [SerializeField]
    GameObject BluePlayerManagerScripts;

    public bool paint = false;
    private bool neighbor = false;

    //選択したマスの上下左右の座標保持
    private Vector2 top = default;
    private Vector2 bottom = default;
    private Vector2 left = default;
    private Vector2 right = default;

    private bool setRedPlayer = false;
    private bool setBluePlayer = false;

    

    private void Awake()
    {
        gameManager = gameManagerScripts.GetComponent<GameManager>();
        RedPlayerManager = RedPlayerManagerScripts.GetComponent<RedPlayerManager>();
        BluePlayerManager = BluePlayerManagerScripts.GetComponent<BluePlayerManager>();

        string textLines = textFile.text; // テキストの全体データの代入
        print(textLines);

        // 改行でデータを分割して配列に代入
        textData = textLines.Split('\n');

        // 行数と列数の取得
        ColumnNumber = textData[0].Split(',').Length;
        LineNumber = textData.Length;

        //print("tate" + LineNumber);
        //print("yoko" + ColumnNumber);

        // ２次元配列の定義
        dungeonMap = new string[LineNumber, ColumnNumber];

        for (int i = 0; i < LineNumber; i++)
        {
            string[] tempWords = textData[i].Split(',');

            for (int j = 0; j < ColumnNumber; j++)
            {
                dungeonMap[i, j] = tempWords[j];
            }
        }
        mapRemake();

        //横山加筆
        //アイテム生成の処理
        for (int i = 1; i <= 10; i++)
        {
            CreateItem(0, 8, 0, 16, "3");
        }
        for (int i = 1; i <= 10; i++)
        {
            CreateItem(0, 8, 0, 16, "4");
        }
        for (int i = 1; i <= 5; i++)
        {
            CreateItem(3, 6, 2, 14, "5");
        }
    }

    /// <summary>
    /// マップの表示
    /// </summary>
    public void mapRemake()
    {
        //最初に以前のマップ画像を消す
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(this.transform.GetChild(i).gameObject);
        }
        this.transform.DetachChildren();

        for (int i = 0; i < LineNumber; i++)
        {
            for (int j = 0; j < ColumnNumber; j++)
            {
                if (dungeonMap[i, j] != null)
                {
                    switch (dungeonMap[i, j])
                    {
                        case "0"://何もなし
                            Instantiate(NothingSquare, new Vector3(-7.5f + j, 3.5f - i, 0), Quaternion.identity, this.transform);
                            break;

                        case "1"://プレイヤー1の色
                            Instantiate(Player1Square, new Vector3(-7.5f + j, 3.5f - i, 0), Quaternion.identity, this.transform);
                            break;

                        case "2"://プレイヤー2の色
                            Instantiate(Player2Square, new Vector3(-7.5f + j, 3.5f - i, 0), Quaternion.identity, this.transform);
                            break;

                        case "3"://ポーション
                            Instantiate(Item1Square, new Vector3(-7.5f + j, 3.5f - i, 0), Quaternion.identity, this.transform);
                            break;

                        case "4"://バケツ
                            Instantiate(Item2Square, new Vector3(-7.5f + j, 3.5f - i, 0), Quaternion.identity, this.transform);
                            break;

                        case "5"://爆弾
                            Instantiate(Item3Square, new Vector3(-7.5f + j, 3.5f - i, 0), Quaternion.identity, this.transform);
                            break;

                        case "6"://赤ゴール
                            Instantiate(GoalSquare, new Vector3(-7.5f + j, 3.5f - i, 0), Quaternion.identity, this.transform);
                            //一人目のプレイヤー配置
                            if (!setRedPlayer)
                            {
                                RedPlayerManager.PlayerUpdate(new Vector2(-7.5f + j, 3.5f - i));
                                setRedPlayer = true;
                            }
                            break;
                        case "7"://青ゴール
                            Instantiate(GoalSquare, new Vector3(-7.5f + j, 3.5f - i, 0), Quaternion.identity, this.transform);
                            //二人目のプレイヤー配置
                            if (!setBluePlayer)
                            {
                                BluePlayerManager.PlayerUpdate(new Vector2(-7.5f + j, 3.5f - i));
                                setBluePlayer = true;
                            }
                            break;
                    }
                }
            }
        }
    }


    //田中加筆
    /// <summary>
    /// RedPlayer処理
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public IEnumerator paintRedMap(Vector2 pos)
    {
        var converterPos = positionConverter(pos);

        //paintがtureになるまで続ける
        while (!paint)
        {
            for (int y = 0; y < LineNumber; y++)
            {

                for (int x = 0; x < ColumnNumber; x++)
                {

                    if (y == converterPos.y && x == converterPos.x)
                    {

                        Debug.Log("縦[" + converterPos.y + "]　" + "横[" + converterPos.x + "]");
                        //Debug.Log("[" + tate + "]" + "[" + yoko + "]");
                        Debug.Log("マスデータ" + dungeonMap[y, x]);

                        top = new Vector2(converterPos.x, converterPos.y - 1);
                        bottom = new Vector2(converterPos.x, converterPos.y + 1);
                        right = new Vector2(converterPos.x + 1, converterPos.y);
                        left = new Vector2(converterPos.x - 1, converterPos.y);

                        //塗ろうとしているマスが塗れないなら終了
                        if (dungeonMap[y, x] == "1" || dungeonMap[y, x] == "6")
                        {
                            break;
                        }
                        //バケツ所持していない状態で相手のマスを塗ろうとしたら飛ばす
                        if (dungeonMap[y, x] == "2" && gameManager.redRePaint <= 0)
                        {
                            break;
                        }
                        //上のマスが配列外でない dungeonMap[(int)top.y, (int)top.x] != null
                        if (!neighbor && IsArrayRange((int)top.y, (int)top.x))
                        {
                            if (dungeonMap[(int)top.y, (int)top.x] == "1" ||
                                dungeonMap[(int)top.y, (int)top.x] == "6")
                            {
                                Debug.Log("上");
                                neighbor = true;
                            }
                        }
                        //右のマスが配列外でない dungeonMap[(int)right.y, (int)right.x] != null
                        if (!neighbor && IsArrayRange((int)right.y, (int)right.x))
                        {
                            if (dungeonMap[(int)right.y, (int)right.x] == "1" ||
                                dungeonMap[(int)right.y, (int)right.x] == "6")
                            {
                                Debug.Log("右");
                                neighbor = true;
                            }
                        }
                        //下のマスが配列外でないdungeonMap[(int)bottom.y, (int)bottom.x] != null
                        if (!neighbor && IsArrayRange((int)bottom.y, (int)bottom.x))
                        {
                            if (dungeonMap[(int)bottom.y, (int)bottom.x] == "1" ||
                                dungeonMap[(int)bottom.y, (int)bottom.x] == "6")
                            {
                                Debug.Log("下");
                                neighbor = true;
                            }
                        }
                        //左のマスが配列外でないdungeonMap[(int)left.y, (int)left.x] != null
                        if (!neighbor && IsArrayRange((int)left.y, (int)left.x))
                        {
                            if (dungeonMap[(int)left.y, (int)left.x] == "1" ||
                                dungeonMap[(int)left.y, (int)left.x] == "6")
                            {
                                Debug.Log("左");
                                neighbor = true;
                            }
                        }
                        if (neighbor)
                        {

                            /*
                            //配列外ならスキップ
                            //隣接したマスに何もなければ何もしない
                            else if (dungeonMap[tate, yoko + 1] == "1" || dungeonMap[tate + 1, yoko] == "1" ||
                                    dungeonMap[tate - 1, yoko] == "1" || dungeonMap[tate, yoko - 1] == "1" ||
                                    dungeonMap[tate, yoko + 1] == "6" || dungeonMap[tate + 1, yoko] == "6" ||
                                    dungeonMap[tate - 1, yoko] == "6" || dungeonMap[tate, yoko - 1] == "6") { 
                            */
                            //相手のマスだったら
                            if (dungeonMap[y, x] == "2" && gameManager.redRePaint > 0)
                            {
                                gameManager.redRePaint--;
                                dungeonMap[y, x] = "1";
                            }
                            //塗る前のデータ保持
                            formerData = int.Parse(dungeonMap[y, x]);
                            //対応した色に塗る
                            dungeonMap[y, x] = "1";

                            neighbor = false;
                            paint = true;
                        }
                    }
                }
            }
            //回し終わってもfalseだったらbreak
            if (!paint) break;
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
                //横山加筆
                //爆弾を使った後、おかしくなる時がある。
                //爆発した際にアイテムが中に存在する場合は、一旦消滅させる用にする
                //爆弾の処理
                for (int y = 0; y < LineNumber; y++)
                {
                    for (int x = 0; x < ColumnNumber; x++)
                    {
                        if (y == converterPos.y && x == converterPos.x)
                        {
                            //上
                            dungeonMap[y - 1, x] = "1";
                            //下
                            dungeonMap[y + 1, x] = "1";
                            //左
                            dungeonMap[y, x - 1] = "1";
                            //左斜め上
                            dungeonMap[y - 1, x - 1] = "1";
                            //左斜め下
                            dungeonMap[y + 1, x - 1] = "1";
                            //右
                            dungeonMap[y, x + 1] = "1";
                            //右斜め上
                            dungeonMap[y - 1, x + 1] = "1";
                            //右斜め下
                            dungeonMap[y + 1, x + 1] = "1";
                        }
                    }
                }
                break;
            case 7:
                SceneManager.LoadScene("Result Scene");
                break;
            default: break;
        }

        yield return null;
    }

    /// <summary>
    /// BluePlayer処理
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public IEnumerator paintBlueMap(Vector2 pos)
    {
        var converterPos = positionConverter(pos);

        //paintがtureになるまで続ける
        while (!paint)
        {
            for (int y = 0; y < LineNumber; y++)
            {

                for (int x = 0; x < ColumnNumber; x++)
                {

                    if (y == converterPos.y && x == converterPos.x)
                    {

                        Debug.Log("縦[" + converterPos.y + "]　" + "横[" + converterPos.x + "]");
                        //Debug.Log("[" + tate + "]" + "[" + yoko + "]");
                        Debug.Log("マスデータ" + dungeonMap[y, x]);

                        top = new Vector2(converterPos.x, converterPos.y - 1);
                        bottom = new Vector2(converterPos.x, converterPos.y + 1);
                        right = new Vector2(converterPos.x + 1, converterPos.y);
                        left = new Vector2(converterPos.x - 1, converterPos.y);

                        //塗ろうとしているマスが塗れないなら終了
                        if (dungeonMap[y, x] == "2" || dungeonMap[y, x] == "7")
                        {
                            break;
                        }
                        //バケツ所持していない状態で相手のマスを塗ろうとしたら飛ばす
                        if (dungeonMap[y, x] == "1" && gameManager.redRePaint <= 0)
                        {
                            break;
                        }
                        //上のマスが配列外でない dungeonMap[(int)top.y, (int)top.x] != null
                        if (!neighbor && IsArrayRange((int)top.y, (int)top.x))
                        {
                            if (dungeonMap[(int)top.y, (int)top.x] == "2" ||
                                dungeonMap[(int)top.y, (int)top.x] == "7")
                            {
                                Debug.Log("上");
                                neighbor = true;
                            }
                        }
                        //右のマスが配列外でない dungeonMap[(int)right.y, (int)right.x] != null
                        if (!neighbor && IsArrayRange((int)right.y, (int)right.x))
                        {
                            if (dungeonMap[(int)right.y, (int)right.x] == "2" ||
                                dungeonMap[(int)right.y, (int)right.x] == "7")
                            {
                                Debug.Log("右");
                                neighbor = true;
                            }
                        }
                        //下のマスが配列外でないdungeonMap[(int)bottom.y, (int)bottom.x] != null
                        if (!neighbor && IsArrayRange((int)bottom.y, (int)bottom.x))
                        {
                            if (dungeonMap[(int)bottom.y, (int)bottom.x] == "2" ||
                                dungeonMap[(int)bottom.y, (int)bottom.x] == "7")
                            {
                                Debug.Log("下");
                                neighbor = true;
                            }
                        }
                        //左のマスが配列外でないdungeonMap[(int)left.y, (int)left.x] != null
                        if (!neighbor && IsArrayRange((int)left.y, (int)left.x))
                        {
                            if (dungeonMap[(int)left.y, (int)left.x] == "2" ||
                                dungeonMap[(int)left.y, (int)left.x] == "7")
                            {
                                Debug.Log("左");
                                neighbor = true;
                            }
                        }
                        if (neighbor)
                        {

                            /*
                            //配列外ならスキップ
                            //隣接したマスに何もなければ何もしない
                            else if (dungeonMap[tate, yoko + 1] == "1" || dungeonMap[tate + 1, yoko] == "1" ||
                                    dungeonMap[tate - 1, yoko] == "1" || dungeonMap[tate, yoko - 1] == "1" ||
                                    dungeonMap[tate, yoko + 1] == "6" || dungeonMap[tate + 1, yoko] == "6" ||
                                    dungeonMap[tate - 1, yoko] == "6" || dungeonMap[tate, yoko - 1] == "6") { 
                            */
                            //相手のマスだったら
                            if (dungeonMap[y, x] == "1" && gameManager.redRePaint > 0)
                            {
                                gameManager.redRePaint--;
                                dungeonMap[y, x] = "2";
                            }
                            //塗る前のデータ保持
                            formerData = int.Parse(dungeonMap[y, x]);
                            //対応した色に塗る
                            dungeonMap[y, x] = "2";

                            neighbor = false;
                            paint = true;
                        }
                    }
                }
            }
            //回し終わってもfalseだったらbreak
            if (!paint) break;
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
                //横山加筆
                //爆弾の処理
                for (int i = 0; i < LineNumber; i++)
                {
                    for (int j = 0; j < ColumnNumber; j++)
                    {
                        if (i == converterPos.y && j == converterPos.x)
                        {
                            //上
                            dungeonMap[i - 1, j] = "2";
                            //下
                            dungeonMap[i + 1, j] = "2";
                            //左
                            dungeonMap[i, j - 1] = "2";
                            //左斜め上
                            dungeonMap[i - 1, j - 1] = "2";
                            //左斜め下
                            dungeonMap[i + 1, j - 1] = "2";
                            //右
                            dungeonMap[i, j + 1] = "2";
                            //右斜め上
                            dungeonMap[i - 1, j + 1] = "2";
                            //右斜め下
                            dungeonMap[i + 1, j + 1] = "2";
                        }
                    }
                }
                break;
            case 6:
                SceneManager.LoadScene("ResultSceneA");
                break;
            default: break;
        }

        yield return null;
    }


    public Vector2 positionConverter(Vector2 pos)
    {
        pos = new Vector2(pos.x + 7.5f, pos.y + 4.5f);
        return pos;

    }

    private bool IsArrayRange(int x, int y)
    {
        return true
            && x >= 0
            && y >= 0
            && x < dungeonMap.GetLength(0)
            && y < dungeonMap.GetLength(1);
    }

    //横山加筆
    //アイテムをランダムで生成するためのメソッド
    void CreateItem(int tate_min, int tate_max, int yoko_min, int yoko_max, string Item)
    {
        while (true)
        {
            int Tate = Random.Range(tate_min, tate_max);
            int Yoko = Random.Range(yoko_min, yoko_max);

            //何もマップに置いて無かったら
            if (dungeonMap[Tate, Yoko] == "0")
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
