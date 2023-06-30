using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PPDisplay : MonoBehaviour
{
    public static PPDisplay ppDisplay = null;
    public static RedPlayerManager redPlayerManager;
    public static BluePlayerManager bluePlayerManager;
    public static GameManager gManager;


    [SerializeField]
    private GameObject player1;
    [SerializeField]
    private GameObject player2;
    [SerializeField]
    private GameObject gameManager;
    private Animator animator;

    [Header("\n塗りポイントテキスト")]
    [SerializeField]
    private Text[] paintPoint;

    [Header("\nバケツポイントテキスト")]
    [SerializeField]
    private Text[] bucketPoint;

    [Header("\nPlayer1のHPUI")]
    [SerializeField]
    private GameObject[] p1HealthPoint;
    [Header("\nPlayer2のHPUI")]
    [SerializeField]
    private GameObject[] p2HealthPoint;

    [Header("ターン表示のテキスト")]
    [SerializeField]
    private Text turnText;

    private Text changeText;

    private int paintPoint1;
    private int paintPoint2;

    private int playerRed = 0;
    private int playerBlue = 1;

    private int p1NowHP;
    private int p2NowHP;

    private int p1decNumber = 0;
    private int p2decNumber = 0;

    private int nowTurn = 1;
    private void Awake()
    {
        if (ppDisplay == null)
        {
            ppDisplay = this; //ppDisplayがまだないならstaticなppDisplayというクラスを作成
        }
        else
        {
            Destroy(this.gameObject);
        }

        bluePlayerManager = player2.GetComponent<BluePlayerManager>();//青プレイヤーのスクリプト取得
        redPlayerManager = player1.GetComponent<RedPlayerManager>();//赤プレイヤーのスクリプト取得
        gManager = gameManager.GetComponent<GameManager>();//ゲームマネージャーの取得
    }
    private void Start() //ペイントポイント、バケツポイントが赤、青どっちが一つ目にアタッチされてもでもいいようにする処理
    {
        turnText.text = "Turn " + nowTurn;
        //ppDisplay.BucketDisplay1(10);
        p1NowHP = gManager.redHp;
        p2NowHP = gManager.blueHp;

        if(paintPoint[0].name == "PaintPointText2")
        {
            changeText = paintPoint[0];
            paintPoint[0] = paintPoint[1];
            paintPoint[1] = changeText;
        }

        if(bucketPoint[0].name == "P2BucketPointText")
        {
            changeText = bucketPoint[0];
            bucketPoint[0] = bucketPoint[1];
            bucketPoint[1] = changeText;
        }

    }

    private void Update()//塗りポイントの表示とターン進行中にリアルタイムの値を表示するための処理
    {
        if (redPlayerManager.moveCounter <= 0) //moveCounterが0の間(赤のターン以外の間)はpaintPoint1の値を表示
        {
            paintPoint[playerRed].text = "" + paintPoint1;
        }
        else//それ以外の時はmoveCounterの値を表示。
        {
            paintPoint[playerRed].text = "" + redPlayerManager.moveCounter;
        }
        //上記と同じ処理
        if (bluePlayerManager.moveCounter <= 0)
        {
            paintPoint[playerBlue].text = "" + paintPoint2;
        }
        else
        {
            paintPoint[playerBlue].text = "" + bluePlayerManager.moveCounter;
        }

        //バケツポイントの値をリアルタイムで表示するための処理

        bucketPoint[playerRed].text = "" + gManager.redRePaint;
        bucketPoint[playerBlue].text = "" + gManager.blueRePaint;

        //HPの値が元の値より減ったら配列で順番にHPの減少アニメーションをして行く処理
        if (gManager.redHp < p1NowHP)
        {
            p1NowHP = gManager.redHp;
            animator = p1HealthPoint[p1decNumber].GetComponent<Animator>();
            animator.SetBool("HPDecrease", true);
            p1decNumber++;
        }

        if (gManager.blueHp < p2NowHP)
        {
            p2NowHP = gManager.blueHp;
            animator = p2HealthPoint[p2decNumber].GetComponent<Animator>();
            animator.SetBool("HPDecrease", true);
            p2decNumber++;
        }
        

    }
    public void PointDisplay1(int Point1)//引数でペイントポイントに入れたい値を取得して代入、表示を行う処理
    {
        paintPoint1 = Point1;
        paintPoint[playerRed].text = "" + paintPoint1;
    }

    public void PointDisplay2(int Point2)
    {
        paintPoint2 = Point2;
        paintPoint[playerBlue].text = "" + paintPoint2;
    }

    public void TurnDisplay()//呼ばれたらターンの値を増やして表示する。
    {
        nowTurn++;
        turnText.text = "Turn " + nowTurn;
    }

}
