//using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Map : MonoBehaviour
{
    /***********マップデータ管理改**********
     *  
     *  一の位はマップ情報
     *  0　なし(塗れる)
     *  1　なし(塗れない)
     *  2　赤マス
     *  3  青マス
     *  4  赤側ゴール
     *  5　青側ゴール
     *  
     *  十の位はギミック+プレイヤー位置
     *  0　なし
     *  1　赤プレイヤー
     *  2　青プレイヤー
     *  3　ポーション
     *  4　バケツ
     *  5　爆弾
     *  6　プレイヤー重なり
     **************************************/


    /* StartSetting　から持ってくる
    private string[] textData;
    private string[,] fieldMap;
    private int LineNumber; // 行数に相当
    private int ColumnNumber; // 列数に相当
    */

    [SerializeField]
    private GameObject NothingSquare;
    [SerializeField]
    private GameObject WallSquare;
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

    //塗った元のマスのデータ保持
    private string formerData = default;
    private string firstPlace = default;
    private string secondPlace = default;

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

    //デバッグ用
    public bool NotItemCreate = false;

    private void Start()
    {
        gameManager = gameManagerScripts.GetComponent<GameManager>();
        RedPlayerManager = RedPlayerManagerScripts.GetComponent<RedPlayerManager>();
        BluePlayerManager = BluePlayerManagerScripts.GetComponent<BluePlayerManager>();

        /*
        if (csvFile == null)
        {
            csvFile = Resources.Load("mapData/BaseMap") as TextAsset;
        }
        string textLines = csvFile.text; // テキストの全体データの代入
        // 改行でデータを分割して配列に代入
        textData = textLines.Split('\n');
        // 行数と列数の取得
        ColumnNumber = textData[0].Split(',').Length;
        LineNumber = textData.Length;
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
        */

        if (!NotItemCreate)
        {
            //横山加筆
            //アイテム生成の処理
            for (int i = 1; i <= 10; i++)
            {
                CreateItem(0, 8, 1, 14, 3);
            }
            for (int i = 1; i <= 10; i++)
            {
                CreateItem(0, 8, 1, 14, 4);
            }
            for (int i = 1; i <= 5; i++)
            {
                CreateItem(3, 6, 2, 13, 5);
            }
        }

        mapRemake();

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

        for (int i = 0; i < StartSetting.LineNumber; i++)
        {
            for (int j = 0; j < StartSetting.ColumnNumber; j++)
            {
                if (StartSetting.fieldMap[i, j] != null)
                {
                    switch (carving(StartSetting.fieldMap[i, j], 1))
                    {
                        case "0"://なし(塗れる)
                            Instantiate(NothingSquare, new Vector3(-7.5f + j, 3.5f - i, 0), Quaternion.identity, this.transform);
                            break;

                        case "1"://なし(塗れない)
                            Instantiate(WallSquare, new Vector3(-7.5f + j, 3.5f - i, 0), Quaternion.identity, this.transform);
                            break;

                        case "2"://赤マス
                            Instantiate(Player1Square, new Vector3(-7.5f + j, 3.5f - i, 0), Quaternion.identity, this.transform);
                            break;

                        case "3"://青マス
                            Instantiate(Player2Square, new Vector3(-7.5f + j, 3.5f - i, 0), Quaternion.identity, this.transform);
                            break;

                        case "4"://赤側ゴール
                            Instantiate(GoalSquare, new Vector3(-7.5f + j, 3.5f - i, 0), Quaternion.identity, this.transform);
                            //一人目のプレイヤー配置
                            if (!setRedPlayer)
                            {
                                RedPlayerManager.PlayerUpdate(new Vector2(-7.5f + j, 3.5f - i));
                                setRedPlayer = true;
                            }
                            break;

                        case "5"://青側ゴール
                            Instantiate(GoalSquare, new Vector3(-7.5f + j, 3.5f - i, 0), Quaternion.identity, this.transform);
                            //二人目のプレイヤー配置
                            if (!setBluePlayer)
                            {
                                BluePlayerManager.PlayerUpdate(new Vector2(-7.5f + j, 3.5f - i));
                                setBluePlayer = true;
                            }
                            break;
                    }
                    switch (carving(StartSetting.fieldMap[i, j], 2))
                    {
                        case "0": //なし
                            break;
                        case "1": //赤プレイヤー
                            break;
                        case "2": //青プレイヤー
                            break;
                        case "3": //ポーション
                            Instantiate(Item1Square, new Vector3(-7.5f + j, 3.5f - i, -1), Quaternion.identity, this.transform);
                            break;
                        case "4": //バケツ
                            Instantiate(Item2Square, new Vector3(-7.5f + j, 3.5f - i, -1), Quaternion.identity, this.transform);
                            break;
                        case "5": //爆弾
                            Instantiate(Item3Square, new Vector3(-7.5f + j, 3.5f - i, -1), Quaternion.identity, this.transform);
                            break;
                        case "6": //プレイヤー重なり
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
            for (int y = 0; y < StartSetting.LineNumber; y++)
            {

                for (int x = 0; x < StartSetting.ColumnNumber; x++)
                {

                    if (y == converterPos.y && x == converterPos.x)
                    {

                        Debug.Log("縦[" + converterPos.y + "]　" + "横[" + converterPos.x + "]");
                        Debug.Log("マスデータ" + StartSetting.fieldMap[y, x]);

                        top = new Vector2(converterPos.x, converterPos.y - 1);
                        bottom = new Vector2(converterPos.x, converterPos.y + 1);
                        right = new Vector2(converterPos.x + 1, converterPos.y);
                        left = new Vector2(converterPos.x - 1, converterPos.y);

                        //塗ろうとしているマスが塗れないなら終了
                        if (carving(StartSetting.fieldMap[y, x], 1) == "1" || carving(StartSetting.fieldMap[y, x], 1) == "2" || carving(StartSetting.fieldMap[y, x], 1) == "4")
                        {
                            Debug.Log("自分の陣地");
                            break;
                        }
                        //バケツ所持していない状態で相手のマスを塗ろうとしたら飛ばす
                        if (carving(StartSetting.fieldMap[y, x], 1) == "3" && gameManager.redRePaint <= 0)
                        {
                            Debug.Log("バケツがありません");
                            break;
                        }
                        //上のマスが配列外でない dungeonMap[(int)top.y, (int)top.x] != null
                        if (!neighbor && IsArrayRange((int)top.y, (int)top.x))
                        {
                            if (carving(StartSetting.fieldMap[(int)top.y, (int)top.x], 1) == "1" ||
                                carving(StartSetting.fieldMap[(int)top.y, (int)top.x], 1) == "2" ||
                                carving(StartSetting.fieldMap[(int)top.y, (int)top.x], 1) == "4")
                            {
                                Debug.Log("上");
                                neighbor = true;
                            }
                        }
                        //右のマスが配列外でない dungeonMap[(int)right.y, (int)right.x] != null
                        if (!neighbor && IsArrayRange((int)right.y, (int)right.x))
                        {
                            if (carving(StartSetting.fieldMap[(int)right.y, (int)right.x], 1) == "1" ||
                                carving(StartSetting.fieldMap[(int)right.y, (int)right.x], 1) == "2" ||
                                carving(StartSetting.fieldMap[(int)right.y, (int)right.x], 1) == "4")
                            {
                                Debug.Log("右");
                                neighbor = true;
                            }
                        }
                        //下のマスが配列外でないdungeonMap[(int)bottom.y, (int)bottom.x] != null
                        if (!neighbor && IsArrayRange((int)bottom.y, (int)bottom.x))
                        {
                            if (carving(StartSetting.fieldMap[(int)bottom.y, (int)bottom.x], 1) == "1" ||
                                carving(StartSetting.fieldMap[(int)bottom.y, (int)bottom.x], 1) == "2" ||
                                carving(StartSetting.fieldMap[(int)bottom.y, (int)bottom.x], 1) == "4")
                            {
                                Debug.Log("下");
                                neighbor = true;
                            }
                        }
                        //左のマスが配列外でないdungeonMap[(int)left.y, (int)left.x] != null
                        if (!neighbor && IsArrayRange((int)left.y, (int)left.x))
                        {
                            if (carving(StartSetting.fieldMap[(int)left.y, (int)left.x], 1) == "1" ||
                                carving(StartSetting.fieldMap[(int)left.y, (int)left.x], 1) == "2" ||
                                carving(StartSetting.fieldMap[(int)left.y, (int)left.x], 1) == "4")
                            {
                                Debug.Log("左");
                                neighbor = true;
                            }
                        }
                        if (neighbor)
                        {
                            //塗る前のデータ保持
                            formerData = StartSetting.fieldMap[y, x];
                            firstPlace = carving(formerData, 1);
                            secondPlace = carving(formerData, 2);
                            switch (secondPlace)
                            {
                                case "3":
                                case "4":
                                case "5":
                                    setMapData(y, x, 2, 0);
                                    break;
                            }
                            //相手のマスだったら
                            if (carving(StartSetting.fieldMap[y, x], 1) == "3" && gameManager.redRePaint > 0)
                            {
                                gameManager.redRePaint--;
                                setMapData(y, x, 1, 2); //dungeonMap[y, x] = "1";
                            }
                            //対応した色に塗る
                            setMapData(y, x, 1, 2); //dungeonMap[y, x] = "1";

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
        switch (secondPlace)
        {
            case "3":
                gameManager.addMoveCounter();
                formerData = null;
                secondPlace = null;
                break;
            case "4":
                gameManager.addRePaint();
                formerData = null;
                secondPlace = null;
                break;
            case "5":
                //横山加筆
                //爆弾を使った後、おかしくなる時がある。
                //爆発した際にアイテムが中に存在する場合は、一旦消滅させる用にする
                //爆弾の処理
                for (int y = 0; y < StartSetting.LineNumber; y++)
                {
                    for (int x = 0; x < StartSetting.ColumnNumber; x++)
                    {
                        if (y == converterPos.y && x == converterPos.x)
                        {
                            //上
                            setMapData(y - 1, x, 1, 2);//dungeonMap[y - 1, x] = "1";
                            //下
                            setMapData(y + 1, x, 1, 2);//dungeonMap[y + 1, x] = "1";
                            //左
                            setMapData(y, x - 1, 1, 2);//dungeonMap[y, x - 1] = "1";
                            //右
                            setMapData(y, x + 1, 1, 2);//dungeonMap[y, x + 1] = "1";
                            //左斜め上
                            setMapData(y - 1, x - 1, 1, 2);//dungeonMap[y - 1, x - 1] = "1";
                            //左斜め下
                            setMapData(y + 1, x - 1, 1, 2);//dungeonMap[y + 1, x - 1] = "1";
                            //右斜め上
                            setMapData(y - 1, x + 1, 1, 2);//dungeonMap[y - 1, x + 1] = "1";
                            //右斜め下
                            setMapData(y + 1, x + 1, 1, 2);//dungeonMap[y + 1, x + 1] = "1";
                        }
                    }
                }
                formerData = null;
                secondPlace = null;
                break;
            default: break;
        }
        switch (firstPlace)
        {
            case "5":
                SceneManager.LoadScene("redWin");
                firstPlace = null;
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
            for (int y = 0; y < StartSetting.LineNumber; y++)
            {

                for (int x = 0; x < StartSetting.ColumnNumber; x++)
                {

                    if (y == converterPos.y && x == converterPos.x)
                    {

                        Debug.Log("縦[" + converterPos.y + "]　" + "横[" + converterPos.x + "]");
                        //Debug.Log("[" + tate + "]" + "[" + yoko + "]");
                        Debug.Log("マスデータ" + StartSetting.fieldMap[y, x]);

                        top = new Vector2(converterPos.x, converterPos.y - 1);
                        bottom = new Vector2(converterPos.x, converterPos.y + 1);
                        right = new Vector2(converterPos.x + 1, converterPos.y);
                        left = new Vector2(converterPos.x - 1, converterPos.y);

                        //塗ろうとしているマスが塗れないなら終了
                        if (carving(StartSetting.fieldMap[y, x], 1) == "1" || carving(StartSetting.fieldMap[y, x], 1) == "3" || carving(StartSetting.fieldMap[y, x], 1) == "5")
                        {
                            break;
                        }
                        //バケツ所持していない状態で相手のマスを塗ろうとしたら飛ばす
                        if (carving(StartSetting.fieldMap[y, x], 1) == "2" && gameManager.blueRePaint <= 0)
                        {
                            break;
                        }
                        //上のマスが配列外でない dungeonMap[(int)top.y, (int)top.x] != null
                        if (!neighbor && IsArrayRange((int)top.y, (int)top.x))
                        {
                            if (carving(StartSetting.fieldMap[(int)top.y, (int)top.x], 1) == "1" ||
                                carving(StartSetting.fieldMap[(int)top.y, (int)top.x], 1) == "3" ||
                                carving(StartSetting.fieldMap[(int)top.y, (int)top.x], 1) == "5")
                            {
                                Debug.Log("上");
                                neighbor = true;
                            }
                        }
                        //右のマスが配列外でない dungeonMap[(int)right.y, (int)right.x] != null
                        if (!neighbor && IsArrayRange((int)right.y, (int)right.x))
                        {
                            if (carving(StartSetting.fieldMap[(int)right.y, (int)right.x], 1) == "1" ||
                                carving(StartSetting.fieldMap[(int)right.y, (int)right.x], 1) == "3" ||
                                carving(StartSetting.fieldMap[(int)right.y, (int)right.x], 1) == "5")
                            {
                                Debug.Log("右");
                                neighbor = true;
                            }
                        }
                        //下のマスが配列外でないdungeonMap[(int)bottom.y, (int)bottom.x] != null
                        if (!neighbor && IsArrayRange((int)bottom.y, (int)bottom.x))
                        {
                            if (carving(StartSetting.fieldMap[(int)bottom.y, (int)bottom.x], 1) == "1" ||
                                carving(StartSetting.fieldMap[(int)bottom.y, (int)bottom.x], 1) == "3" ||
                                carving(StartSetting.fieldMap[(int)bottom.y, (int)bottom.x], 1) == "5")
                            {
                                Debug.Log("下");
                                neighbor = true;
                            }
                        }
                        //左のマスが配列外でないdungeonMap[(int)left.y, (int)left.x] != null
                        if (!neighbor && IsArrayRange((int)left.y, (int)left.x))
                        {
                            if (carving(StartSetting.fieldMap[(int)left.y, (int)left.x], 1) == "1" ||
                                carving(StartSetting.fieldMap[(int)left.y, (int)left.x], 1) == "3" ||
                                carving(StartSetting.fieldMap[(int)left.y, (int)left.x], 1) == "5")
                            {
                                Debug.Log("左");
                                neighbor = true;
                            }
                        }
                        if (neighbor)
                        {
                            //塗る前のデータ保持
                            formerData = StartSetting.fieldMap[y, x];
                            firstPlace = carving(formerData, 1);
                            secondPlace = carving(formerData, 2);
                            switch (secondPlace)
                            {
                                case "3":
                                case "4":
                                case "5":
                                    setMapData(y, x, 2, 0);
                                    break;
                            }
                            //相手のマスだったら
                            if (carving(StartSetting.fieldMap[y, x], 1) == "2" && gameManager.blueRePaint > 0)
                            {
                                gameManager.blueRePaint--;
                                setMapData(y, x, 1, 3); // dungeonMap[y, x] = "2";
                            }

                            //対応した色に塗る
                            setMapData(y, x, 1, 3); // dungeonMap[y, x] = "2";
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
        switch (secondPlace)
        {
            case "3":
                gameManager.addMoveCounter();
                formerData = null;
                secondPlace = null;
                break;
            case "4":
                gameManager.addRePaint();
                formerData = null;
                secondPlace = null;
                break;
            case "5":
                //横山加筆
                //爆弾の処理
                for (int y = 0; y < StartSetting.LineNumber; y++)
                {
                    for (int x = 0; x < StartSetting.ColumnNumber; x++)
                    {
                        if (y == converterPos.y && x == converterPos.x)
                        {
                            //上
                            setMapData(y - 1, x, 1, 3);//dungeonMap[y - 1, x] = "1";
                            //下
                            setMapData(y + 1, x, 1, 3);//dungeonMap[y + 1, x] = "1";
                            //左
                            setMapData(y, x - 1, 1, 3);//dungeonMap[y, x - 1] = "1";
                            //右
                            setMapData(y, x + 1, 1, 3);//dungeonMap[y, x + 1] = "1";
                            //左斜め上
                            setMapData(y - 1, x - 1, 1, 3);//dungeonMap[y - 1, x - 1] = "1";
                            //左斜め下
                            setMapData(y + 1, x - 1, 1, 3);//dungeonMap[y + 1, x - 1] = "1";
                            //右斜め上
                            setMapData(y - 1, x + 1, 1, 3);//dungeonMap[y - 1, x + 1] = "1";
                            //右斜め下
                            setMapData(y + 1, x + 1, 1, 3);//dungeonMap[y + 1, x + 1] = "1";
                        }
                    }
                }
                formerData = null;
                secondPlace = null;
                break;

            default: break;
        }
        switch (firstPlace)
        {
            case "4":
                SceneManager.LoadScene("blueWin");
                firstPlace = null;
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
            && x < StartSetting.fieldMap.GetLength(0)
            && y < StartSetting.fieldMap.GetLength(1);
    }

    //横山加筆
    //アイテムをランダムで生成するためのメソッド
    void CreateItem(int tate_min, int tate_max, int yoko_min, int yoko_max, int Item)
    {
        int cc = 0;
        while (true)
        {
            int Tate = Random.Range(tate_min, tate_max);
            int Yoko = Random.Range(yoko_min, yoko_max);

            Debug.Log(string.Join(",", StartSetting.fieldMap.Cast<string>()));
            //何もマップに置いて無かったら
            if (carving(StartSetting.fieldMap[Tate, Yoko], 2) == "0")
            {
                setMapData(Tate, Yoko, 2, Item);//dungeonMap[Tate, Yoko] = Item;

                /*
                switch (carving(dungeonMap[Tate, Yoko], 2))
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
                */

                //処理を抜ける
                break;
                
            }
            cc++;
            if (cc >= 20) Debug.LogError("ERROR"); break;
        }
    }
    //dungeonMap[,] 桁数
    public string carving(string num, int digit)
    {
        if (num != null)
        {
            int n = int.Parse(num);
            int res = 0;
            res = (int)(n / Mathf.Pow(10, digit - 1)) % 10;
            return res.ToString();
        }
        return null;
    }
    //一次、二次、桁数、変更する数字
    public void setMapData(int o, int p, int digit, int setData)
    {
        int d = int.Parse(StartSetting.fieldMap[o, p]);
        //壱の位
        int n = d % 10;
        //十の位
        int m = (d / 10) % 10; ;
        int ans;
        if (digit == 1)
        {
            n = setData;
            ans = (m * 10) + n;
        }
        else
        {
            m = setData;
            ans = (m * 10) + n;
        }

        //Debug.Log(ans);
        StartSetting.fieldMap[o, p] = ans.ToString("D2");
    }
}
