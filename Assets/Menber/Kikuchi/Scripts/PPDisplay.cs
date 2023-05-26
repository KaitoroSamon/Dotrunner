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


    [SerializeField]
    private GameObject player1;
    [SerializeField]
    private GameObject player2;
    [SerializeField]
    private GameObject gManager;

    [SerializeField]
    [Header("PaintPointText")]
    private Text[] paintPoint;//塗りポイントの表示をするテキストをアタッチしてください
    [SerializeField]
    [Header("BucketPointText")]
    private Text[] bucketPoint;//バケツの個数表示をするテキストをアタッチしてください
    [SerializeField]
    [Header("Player1HP")]
    private GameObject[] p1HealthPoint;//HPの個数表示をする画像をアタッチしてください
    [SerializeField]
    [Header("Player2HP")]
    private GameObject[] p2HealthPoint;//HPの個数表示をする画像をアタッチしてください

    private Text changeText;

    private int paintPoint1;
    private int paintPoint2;

    private int playerRed = 0;
    private int playerBlue = 1;

    private void Awake()
    {
        if (ppDisplay == null)
        {
            ppDisplay = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        bluePlayerManager = player2.GetComponent<BluePlayerManager>();
        redPlayerManager = player1.GetComponent<RedPlayerManager>();
    }
    private void Start()
    {
        playerRed = 0;
        playerBlue = 1;
        //ppDisplay.BucketDisplay1(10);//呼び出し、表示テスト。この関数をバケツ、塗ポイントの更新時に呼び出して引数でバケツ、塗ポイントの変数を送って下さい。
    }

    private void Update()
    {
        if (redPlayerManager.moveCounter <= 0)
        {
            paintPoint[playerRed].text = "" + paintPoint1;
        }
        else
        {
            paintPoint[playerRed].text = "" + redPlayerManager.moveCounter;
        }

        if (bluePlayerManager.moveCounter <= 0)
        {
            paintPoint[playerBlue].text = "" + paintPoint2;
        }
        else
        {
            paintPoint[playerBlue].text = "" + bluePlayerManager.moveCounter;
        }

        //HP処理
       
        


    }
    public void PointDisplay1(int Point1)//この関数には左側に表示する塗ポイントの値を送ってください。
    {
        paintPoint1 = Point1;
        paintPoint[playerRed].text = "" + paintPoint1;
    }

    public void PointDisplay2(int Point2)//この関数には右側に表示する塗ポイントの値を送ってください。
    {
        paintPoint2 = Point2;
        paintPoint[playerBlue].text = "" + paintPoint2;
    }

    public void BucketDisplay1(int BucketPoint1)//この関数には左側に表示するバケツポイントの値を送ってください。
    {
        bucketPoint[playerRed].text = "" + BucketPoint1;
    }

    public void BucketDisplay2(int BucketPoint2)//この関数には右側に表示するバケツポイントの値を送ってください。
    {
        Debug.Log("dousa");
        bucketPoint[playerBlue].text = "" + BucketPoint2;
    }
    public void p1HPDisplay(int BucketPoint1)//この関数には左側に表示するバケツポイントの値を送ってください。
    {
        //ハートを消す処理
    }

    public void p2HPDisplay(int BucketPoint2)//この関数には右側に表示するバケツポイントの値を送ってください。
    {
        //ハートを消す処理
    }
}
