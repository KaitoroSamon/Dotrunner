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
    private int  maxMoveCounter = 3;
    //バケツ所持数
    public static int rePaint_p1 = 0; //横山一部変更
    PlayerManager playerManager;

    //横山加筆
    //ポーションの最大所持数
    public int Portopn_limit = 9;
    //ポーションの所持数
    private int portopn_p1 = 0;
    private int portopn_p2 = 0;
    //行動回数アップ変数
    private int move_up;
    //バケツの最大所持数
    public int RePaint_limit = 0;
    //バケツの所持数
    public static int rePaint_p2 = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerObj1 = GameObject.Find("Square");
        playerObj2 = GameObject.Find("Square (1)");

        //横山加筆
        //初期化
        portopn_p1 = 0;
        portopn_p2 = 0;
        rePaint_p1 = 0;
        rePaint_p2 = 0;
    }

    // Update is called once per frame
    void Update()
    {
    
         if (Input.GetKeyDown(KeyCode.Space))
        {
            if (player1Trun)
            {
                // プレイヤー１の攻撃
                playerObj1.GetComponent<Player1>().Attack();//プレイヤー1の行動を呼び出す

                playerManager.PlayertrunUpdate(player1Trun, maxMoveCounter, rePaint_p1);  //横山一部変更
            }
            else
            {
                // プレイヤー２の攻撃
                playerObj2.GetComponent<Player2>().Attack();
            }
        }
    }

    //ターンチェンジ
    public void trunChange()
    {
        if (player1Trun)
        {
            //横山加筆
            maxMoveCounter = 3;
            move_up = maxMoveCounter + portopn_p1;
            maxMoveCounter = move_up;

            player1Trun = false;
        }
        else
        {
            //横山加筆
            //maxMoveCounter = 3;
            //move_up = maxMoveCounter + portopn_p2;
            //maxMoveCounter = move_up;

            player1Trun = true;
        }
    }

    //ポーションをとった処理
    public void addMoveCounter()
    {
        //横山加筆
        //P1側の処理
        if(player1Trun)
        {
            if(Portopn_limit > portopn_p1)
            {
                portopn_p1++;
            }
        }
        /*
        //P2側の処理
        else
        {
            if (Portopn_limit > portopn_p2)
            {
                portopn_p2++;
            }
        }
        */
    }
    //バケツをとった処理
    public void addRePaint()
    {
        //横山加筆
        //P1側の処理
        if (player1Trun)
        {
            if (RePaint_limit > rePaint_p1)
            {
                //バケツの所持数が９じゃなかったら
                if(rePaint_p1 != 9)
                {
                    rePaint_p1 += 2;
                }
                else
                {
                    rePaint_p1++;
                }
            }
        }
        /*
        //P2側の処理
        else
        {
            if (RePaint_limit > rePaint_p2)
            {
                //バケツの所持数が９じゃなかったら
                if(rePaint_p2 != 9)
                {
                    rePaint_p2 += 2;
                }
                else
                {
                    rePaint_p2++;
                }
            }
        }
        */
    }
    public void arrivalDestination()
    {
        //リザルト処理
    }
}

//横山加筆
//P2側に情報を反映させる処理が見当たらなかったので、
//処理だけ書いてコメントアウトしています。
