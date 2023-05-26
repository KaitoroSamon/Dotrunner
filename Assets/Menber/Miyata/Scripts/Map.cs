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
    }

    public void mapRemake()
    {
        Debug.Log("マップ生成");
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
                            RedPlayerManager.PlayerUpdate(new Vector2(-7.5f + j, 3.5f - i));

                            break;
                        case "7"://青ゴール
                            Instantiate(GoalSquare, new Vector3(-7.5f + j, 3.5f - i, 0), Quaternion.identity, this.transform);
                            //二人目のプレイヤー配置
                            BluePlayerManager.PlayerUpdate(new Vector2(-7.5f + j, 3.5f - i));
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
        //Debug.Log("["+pos.x+ "]" + "["+pos.y+"]");

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

                        top = new Vector2(converterPos.x, converterPos.y + 1);
                        bottom = new Vector2(converterPos.x, converterPos.y - 1);
                        right = new Vector2(converterPos.x + 1, converterPos.y);
                        left = new Vector2(converterPos.x - 1, converterPos.y);

                        //塗ろうとしているマスが塗れないなら終了
                        if (dungeonMap[y, x] == "1" || dungeonMap[y, x] == "6")
                        {
                            break;
                        }

                        //上のマスが配列外でない
                        if (dungeonMap[(int)top.y, (int)top.x] != null)
                        {
                            Debug.Log("top");
                            if (dungeonMap[(int)top.y, (int)top.x] == "1" ||
                                dungeonMap[(int)top.y, (int)top.x] == "6"){
                                neighbor = true;
                            }
                        }
                        //右のマスが配列外でない
                        if (dungeonMap[(int)right.y, (int)right.x] != null)
                        {
                            Debug.Log("right");
                            if (dungeonMap[(int)right.y, (int)right.x] == "1" ||
                                dungeonMap[(int)right.y, (int)right.x] == "6"){
                                neighbor = true;
                            }
                        }
                        //左のマスが配列外でない
                        if (dungeonMap[(int)bottom.y, (int)bottom.x] != null)
                        {
                            Debug.Log("bottom");
                            if (dungeonMap[(int)bottom.y, (int)bottom.x] == "1" ||
                                dungeonMap[(int)bottom.y, (int)bottom.x] == "6"){
                                neighbor = true;
                            }
                        }
                        //下のマスが配列外でない
                        if (dungeonMap[(int)left.y, (int)left.x] != null)
                        {
                            Debug.Log("left");
                            if (dungeonMap[(int)left.y, (int)left.x] == "1" ||
                                dungeonMap[(int)left.y, (int)left.x] == "6"){
                                neighbor = true;
                            }
                        }
                        if (dungeonMap[y, x] == "2" && gameManager.redRePaint < 0)
                        {
                            neighbor = false;
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
                //爆弾の処理
                break;
            case 7:
                SceneManager.LoadScene("Result Scene");
                break;
            default: break;
        }

        yield return null;
    }

    public IEnumerator paintBlueMap(Vector2 pos)
    {
        var converterPos = positionConverter(pos);
        //Debug.Log("["+pos.x+ "]" + "["+pos.y+"]");

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

                        top = new Vector2(converterPos.x, converterPos.y + 1);
                        bottom = new Vector2(converterPos.x, converterPos.y - 1);
                        right = new Vector2(converterPos.x + 1, converterPos.y);
                        left = new Vector2(converterPos.x - 1, converterPos.y);

                        //塗ろうとしているマスが塗れないなら終了
                        if (dungeonMap[y, x] == "2" || dungeonMap[y, x] == "7")
                        {
                            break;
                        }

                        //上のマスが配列外でない
                        if (dungeonMap[(int)top.y, (int)top.x] != null)
                        {
                            Debug.Log("top");
                            if (dungeonMap[(int)top.y, (int)top.x] == "2" ||
                                dungeonMap[(int)top.y, (int)top.x] == "7")
                            {
                                neighbor = true;
                            }
                        }
                        //右のマスが配列外でない
                        if (dungeonMap[(int)right.y, (int)right.x] != null)
                        {
                            Debug.Log("right");
                            if (dungeonMap[(int)right.y, (int)right.x] == "2" ||
                                dungeonMap[(int)right.y, (int)right.x] == "7")
                            {
                                neighbor = true;
                            }
                        }
                        //左のマスが配列外でない
                        if (dungeonMap[(int)bottom.y, (int)bottom.x] != null)
                        {
                            Debug.Log("bottom");
                            if (dungeonMap[(int)bottom.y, (int)bottom.x] == "2" ||
                                dungeonMap[(int)bottom.y, (int)bottom.x] == "7")
                            {
                                neighbor = true;
                            }
                        }
                        //下のマスが配列外でない
                        if (dungeonMap[(int)left.y, (int)left.x] != null)
                        {
                            Debug.Log("left");
                            if (dungeonMap[(int)left.y, (int)left.x] == "2" ||
                                dungeonMap[(int)left.y, (int)left.x] == "7")
                            {
                                neighbor = true;
                            }
                        }
                        if (dungeonMap[y, x] == "1" && gameManager.redRePaint < 0)
                        {
                            neighbor = false;
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
                                gameManager.blueRePaint--;
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
                //爆弾の処理
                break;
            case 6:
                SceneManager.LoadScene("Result Scene A");
                break;
            default: break;
        }

        yield return null;
    }

    public Vector2 positionConverter(Vector2 pos)
    {
        //  x/y   x   y
        //  0/0 -7.5 3.5
        //  0/8 -7.5 -4.5
        //  15/0 7.5 3.5
        //  15/8 7.5 -4.5

        //(0,0)を起点にする
        pos = new Vector2(pos.x + 7.5f, pos.y + 4.5f);
        return pos;

    }
}
