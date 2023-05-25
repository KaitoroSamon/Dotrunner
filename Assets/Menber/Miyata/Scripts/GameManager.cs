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
    private int rePaint = 0;
    public static PlayerManager playerManager;
    [SerializeField]
    GameObject player1;
    public bool oneTime = false;

    // Start is called before the first frame update
    void Start()
    {
        playerObj1 = GameObject.Find("Square");
        playerObj2 = GameObject.Find("Square (1)");

        playerManager = player1.GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (player1Trun)
        {
            if(oneTime == false)
            {
                oneTime = true;
                // プレイヤー１の攻撃
                //playerObj1.GetComponent<Player1>().Attack();//プレイヤー1の行動を呼び出す

                Debug.Log("<color=cyan> TrunChange! </color>");

                playerManager.PlayertrunUpdate(player1Trun, maxMoveCounter, rePaint);
            }
        }
        else
        {
            if (oneTime == false)
            {
                oneTime = true;
                // プレイヤー２の攻撃
                Debug.Log("<color=orange> TrunChange! </color>");
                //playerObj2.GetComponent<Player2>().Attack();
            }
        }
        //デバッグ用
        if (Input.GetKeyDown(KeyCode.F1)){
            trunChange();
        }
    }

    //ターンチェンジ
    public void trunChange()
    {
        if (player1Trun)
        {
            player1Trun = false;
        }
        else
        {
            player1Trun = true;
        }
        oneTime = false;
    }

    //ポーションをとった処理
    public void addMoveCounter()
    {

    }
    //バケツをとった処理
    public void addRePaint()
    {

    }
    public void arrivalDestination()
    {
        //リザルト処理
    }
}
