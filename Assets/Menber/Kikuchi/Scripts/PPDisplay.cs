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
    private Text[] paintPoint;//�h��|�C���g�̕\��������e�L�X�g���A�^�b�`���Ă�������
    [SerializeField]
    [Header("BucketPointText")]
    private Text[] bucketPoint;//�o�P�c�̌��\��������e�L�X�g���A�^�b�`���Ă�������
    [SerializeField]
    [Header("Player1HP")]
    private GameObject[] p1HealthPoint;//HP�̌��\��������摜���A�^�b�`���Ă�������
    [SerializeField]
    [Header("Player2HP")]
    private GameObject[] p2HealthPoint;//HP�̌��\��������摜���A�^�b�`���Ă�������

    private Text changeText;

    private GameObject[] Test;

    private int paintPoint1;
    private int paintPoint2;

    private int playerRed = 0;
    private int playerBlue = 1;

    private int p1NowHP;
    private int p2NowHP;

    private int p1decNumber = 0;
    private int p2decNumber = 0;
    private int maxHP = 2;
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
        //ppDisplay.BucketDisplay1(10);
        gManager.redHp = p1NowHP;
        gManager.blueHp = p2NowHP;

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

        if(p1HealthPoint[0].name == "P1Hp2")
        {

        }

        if (p2HealthPoint[0].name == "P2Hp2")
        {

        }

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

        //HP����
        if((gManager.redHp < p1NowHP) && p1decNumber < maxHP)
        {
            p1NowHP = gManager.redHp;
            p1HealthPoint[p1decNumber].GetComponent<Image>().color = new Color(0, 0, 0, -255);
            p1decNumber++;
        }

        if ((gManager.blueHp < p2NowHP) && p2decNumber < maxHP)
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

    public void BucketDisplay1(int BucketPoint1)//���̊֐��ɂ͍����ɕ\������o�P�c�|�C���g�̒l�𑗂��Ă��������B
    {
        bucketPoint[playerRed].text = "" + BucketPoint1;
    }

    public void BucketDisplay2(int BucketPoint2)//���̊֐��ɂ͉E���ɕ\������o�P�c�|�C���g�̒l�𑗂��Ă��������B
    {
        Debug.Log("dousa");
        bucketPoint[playerBlue].text = "" + BucketPoint2;
    }
}
