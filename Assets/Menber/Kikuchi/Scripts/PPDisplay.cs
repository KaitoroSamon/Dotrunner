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

    [SerializeField]
    [Header("PaintPointText")]
    private Text[] paintPoint;
    [SerializeField]
    [Header("BucketPointText")]
    private Text[] bucketPoint;
    [SerializeField]
    [Header("Player1HP")]
    private GameObject[] p1HealthPoint;
    [SerializeField]
    [Header("Player2HP")]
    private GameObject[] p2HealthPoint;

    [SerializeField]
    [Header("TurnText")]
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
            ppDisplay = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        bluePlayerManager = player2.GetComponent<BluePlayerManager>();
        redPlayerManager = player1.GetComponent<RedPlayerManager>();
        gManager = gameManager.GetComponent<GameManager>();
    }
    private void Start()
    {
        turnText.text = "Turn" + nowTurn;
        //ppDisplay.BucketDisplay1(10);
        p1NowHP = gManager.redHp;
        p2NowHP = gManager.blueHp;

        if(paintPoint[0].name == "PaintPointText2")
        {
            changeText = paintPoint[0];
            paintPoint[0] = paintPoint[1];
            paintPoint[1] = changeText;
        }

        if(bucketPoint[0].name == "BucketPointText2")
        {
            changeText = bucketPoint[0];
            bucketPoint[0] = bucketPoint[1];
            bucketPoint[1] = changeText;
        }

    }

    private void Update()
    {
        Debug.Log(gManager.redHp);
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

        bucketPoint[playerRed].text = "" + gManager.redRePaint;
        bucketPoint[playerBlue].text = "" + gManager.blueRePaint;

        //HP
        if (gManager.redHp < p1NowHP)
        {
            p1NowHP = gManager.redHp;
            p1HealthPoint[p1decNumber].GetComponent<Image>().color = new Color(0, 0, 0, -255);
            p1decNumber++;
            Debug.Log("aaaaaaaaaaaaaaaaaaa");
        }

        if (gManager.blueHp < p2NowHP)
        {
            p2NowHP = gManager.blueHp;
            p2HealthPoint[p2decNumber].GetComponent<Image>().color = new Color(0, 0, 0, -255);
            p2decNumber++;
        }
        

    }
    public void PointDisplay1(int Point1)//���̊֐��ɂ͍����ɕ\������h�|�C���g�̒l�𑗂��Ă��������B
    {
        paintPoint1 = Point1;
        paintPoint[playerRed].text = "" + paintPoint1;
    }

    public void PointDisplay2(int Point2)//���̊֐��ɂ͉E���ɕ\������h�|�C���g�̒l�𑗂��Ă��������B
    {
        paintPoint2 = Point2;
        paintPoint[playerBlue].text = "" + paintPoint2;
    }

    public void TurnDisplay()
    {
        nowTurn++;
        turnText.text = "Turn" + nowTurn;
    }

    //public void BucketDisplay1(int BucketPoint1)//���̊֐��ɂ͍����ɕ\������o�P�c�|�C���g�̒l�𑗂��Ă��������B
    //{
    //    bucketPoint[playerRed].text = "" + BucketPoint1;
    //}

    //public void BucketDisplay2(int BucketPoint2)//���̊֐��ɂ͉E���ɕ\������o�P�c�|�C���g�̒l�𑗂��Ă��������B
    //{
    //    Debug.Log("dousa");
    //    bucketPoint[playerBlue].text = "" + BucketPoint2;
    //}
}
