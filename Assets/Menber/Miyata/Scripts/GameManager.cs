using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    Animator animator;
    [SerializeField]
    GameObject CutIn;
    [SerializeField]
    Text CutInText;

    public bool stopInputKey = false;

    public static bool player1Trun = true; //どちらが攻撃しているかを保存しておくフィールド

    //田中加筆
    [Header("\nプレイヤー(赤色)")]
    [SerializeField]
    GameObject player1;
    //最大塗り回数
    private int redMaxMoveCounter = 3;
    public RedPlayerManager redPlayerManager;
    
    //プレイヤー1のHP
    public int redHp = 2;

    [Header("\nプレイヤー(青色)")]
    [SerializeField]
    GameObject player2;
    //最大塗り回数
    private int blueMaxMoveCounter = 3;
    public BluePlayerManager bluePlayerManager;
    //プレイヤー2のHP
    public int blueHp = 2;


    [Header("\nポーション所持数")]
    //↓↓↓↓↓横山追記
    //ポーションの最大所持数
    public int portion_limit = 6;
    //ポーションの所持数
    public int portion_red = 0;
    public int portion_blue = 0;
    //行動回数アップ変数
    private int move_up;

    [Header("\nバケツの最大所持数")]
    //バケツの最大所持数
    public int RePaint_limit = 10;
    //↑↑↑↑↑
    //バケツ所持数
    public int redRePaint = 0;
    public int blueRePaint = 0;
    public bool oneTime = false;

    // Start is called before the first frame update
    void Start()
    {
        redPlayerManager = player1.GetComponent<RedPlayerManager>();
        bluePlayerManager = player2.GetComponent<BluePlayerManager>();

        animator = CutIn.GetComponent<Animator>();

        //↓↓↓↓↓横山追記
        //初期化
        redRePaint = 0;
        blueRePaint = 0;
        portion_red = 0;
        portion_blue = 0;
        //↑↑↑↑↑

        //菊地加筆
        PPDisplay.ppDisplay.PointDisplay1(redMaxMoveCounter);
        PPDisplay.ppDisplay.PointDisplay2(blueMaxMoveCounter);
    }

    // Update is called once per frame
    void Update()
    {

        if (player1Trun)
        {
            CutInText.color = new Color32(255, 122, 0, 255);
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
            CutInText.color = new Color32(0, 122, 255, 255);
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
        if (Input.GetKeyDown(KeyCode.F11))
        {
            redHp--;
            Debug.Log("redHp" + redHp);
        }
        if (Input.GetKeyDown(KeyCode.F12))
        {
            blueHp--;
            Debug.Log("blueHp" + blueHp);
        }
    }

    //ターンチェンジ
    public void trunChange()
    {
        if (player1Trun)
        {
            StartCoroutine(CutInAnimator());

            //横山追記
            redMaxMoveCounter = 3;
            move_up = redMaxMoveCounter + portion_red;
            redMaxMoveCounter = move_up;

            //菊地加筆
            PPDisplay.ppDisplay.PointDisplay1(redMaxMoveCounter);//P1側の塗ポイントを更新して表示


            player1Trun = false;
        }
        else
        {
            StartCoroutine(CutInAnimator());

            //横山追記
            blueMaxMoveCounter = 3;
            move_up = blueMaxMoveCounter + portion_blue;
            blueMaxMoveCounter = move_up;

            //菊地加筆
            PPDisplay.ppDisplay.PointDisplay2(blueMaxMoveCounter);//P2側の塗ポイントを更新して表示

            player1Trun = true;

            //菊地加筆
            PPDisplay.ppDisplay.TurnDisplay();
        }
        oneTime = false;
    }

    //ポーションをとった処理
    public void addMoveCounter()
    {
        //横山追記
        //P1側の処理
        if (player1Trun)
        {
            if (portion_limit > portion_red)
            {
                portion_red++;
            }
        }
        //P2側の処理
        else
        {
            if (portion_limit > portion_blue)
            {
                portion_blue++;
            }
        }
    }
    //バケツをとった処理
    public void addRePaint()
    {
        //横山追記
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
            }
        }
        //P2側の処理
        else
        {
            if (RePaint_limit >= blueRePaint)
            {
                //バケツの所持数が９じゃなかったら
                if (blueRePaint != 9)
                {
                    blueRePaint += 2;
                }
                else
                {
                    blueRePaint++;

                }
            }
        }
    }
    private IEnumerator CutInAnimator()
    {
        stopInputKey = true;
        animator.SetBool("TrunChange", true);
        yield return new WaitForSecondsRealtime(1f);
        animator.SetBool("TrunChange", false);
        stopInputKey = false;
    }
}
