using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public TextAsset mapText; //マップの情報を取得

    bool player1Trun = true; //どちらが攻撃しているかを保存しておくフィールド

    GameObject playerObj1;
    GameObject playerObj2;

    //田中加筆
    //最大塗り回数
    private int redMaxMoveCounter = 3;
    //バケツ所持数
    public int redRePaint = 0;
    public static RedPlayerManager redPlayerManager;
    [SerializeField]
    GameObject player1;
    //プレイヤー1のHP
    public int redHp = 3;
    

    //最大塗り回数
    private int blueMaxMoveCounter = 3;
    //バケツ所持数
    public int blueRePaint = 0;
    public static BluePlayerManager bluePlayerManager;
    [SerializeField]
    GameObject player2;
    //プレイヤー2のHP
    public int blueHp = 3;

    //横山加筆
    //ポーションの最大所持数
    public int Portopn_limit = 9;
    //ポーションの所持数
    public int portopn_p1 = 0;
    public int portopn_p2 = 0;
    //行動回数アップ変数
    private int move_up;
    //バケツの最大所持数
    public int RePaint_limit = 10;

    public bool oneTime = false;

    // Start is called before the first frame update
    void Start()
    {
        playerObj1 = GameObject.Find("Square");
        playerObj2 = GameObject.Find("Square (1)");

        redPlayerManager = player1.GetComponent<RedPlayerManager>();
        bluePlayerManager = player2.GetComponent<BluePlayerManager>();

        //横山加筆
        //初期化
        redRePaint = 0;
        blueRePaint = 0;
        portopn_p1 = 0;
        portopn_p2 = 0;

        PPDisplay.ppDisplay.PointDisplay1(redMaxMoveCounter);
        PPDisplay.ppDisplay.PointDisplay2(blueMaxMoveCounter);
        PPDisplay.ppDisplay.BucketDisplay1(redRePaint);
        PPDisplay.ppDisplay.BucketDisplay2(blueRePaint);
    }

    // Update is called once per frame
    void Update()
    {

        if (player1Trun)
        {
            if (oneTime == false)
            {
                oneTime = true;
                // プレイヤー１の攻撃
                //playerObj1.GetComponent<Player1>().Attack();//プレイヤー1の行動を呼び出す

                Debug.Log("<color=orange> TrunChange! </color>");

                redPlayerManager.PlayertrunUpdate(player1Trun, redMaxMoveCounter, redRePaint);
            }
        }
        else
        {
            if (oneTime == false)
            {
                oneTime = true;
                // プレイヤー２の攻撃
                Debug.Log("<color=cyan> TrunChange! </color>");
                //playerObj2.GetComponent<Player2>().Attack();

                bluePlayerManager.PlayertrunUpdate(!player1Trun, blueMaxMoveCounter, blueRePaint);
            }
        }
        //デバッグ用
        if (Input.GetKeyDown(KeyCode.F1))
        {
            trunChange();
        }
    }

    //ターンチェンジ
    public void trunChange()
    {
        if (player1Trun)
        {
            //横山加筆
            redMaxMoveCounter = 3;
            move_up = redMaxMoveCounter + portopn_p1;
            redMaxMoveCounter = move_up;

            //菊地加筆
            PPDisplay.ppDisplay.PointDisplay1(redMaxMoveCounter);//P1側の塗ポイントを更新して表示


            player1Trun = false;
        }
        else
        {
            //横山加筆
            blueMaxMoveCounter = 3;
            move_up = blueMaxMoveCounter + portopn_p2;
            blueMaxMoveCounter = move_up;

            //菊地加筆
            PPDisplay.ppDisplay.PointDisplay2(blueMaxMoveCounter);//P2側の塗ポイントを更新して表示

            player1Trun = true;
        }
        oneTime = false;
    }

    //ポーションをとった処理
    public void addMoveCounter()
    {
        //横山加筆
        //P1側の処理
        if (player1Trun)
        {
            if (Portopn_limit > portopn_p1)
            {
                portopn_p1++;
            }
        }
        //P2側の処理
        else
        {
            if (Portopn_limit > portopn_p2)
            {
                portopn_p2++;
            }
        }
    }
    //バケツをとった処理
    public void addRePaint()
    {
        //横山加筆
        //P1側の処理
        if (player1Trun)
        {
            if (RePaint_limit >= redRePaint)
            {
                //バケツの所持数が９じゃなかったら
                if (redRePaint != 9)
                {
                    redRePaint += 2;
                }
                else
                {
                    redRePaint++;
                }
                //菊地加筆
                PPDisplay.ppDisplay.BucketDisplay1(redRePaint);//P1のバケツポイントを更新して表示
            }
        }
        //P2側の処理
        else
        {
            if (RePaint_limit >= blueRePaint)
            {
                Debug.Log("バケツ");

                //バケツの所持数が９じゃなかったら
                if (blueRePaint != 9)
                {
                    blueRePaint += 2;
                }
                else
                {
                    blueRePaint++;
                    
                }
                //菊地加筆
                PPDisplay.ppDisplay.BucketDisplay2(blueRePaint);//P2のバケツポイントを更新して表示
            }
        }
    }
    public void arrivalDestination()
    {
        //リザルト処理
    }
}
