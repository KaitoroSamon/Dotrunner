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
        //ppDisplay.BucketDisplay1(10);//�Ăяo���A�\���e�X�g�B���̊֐����o�P�c�A�h�|�C���g�̍X�V���ɌĂяo���Ĉ����Ńo�P�c�A�h�|�C���g�̕ϐ��𑗂��ĉ������B
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
    public void p1HPDisplay(int BucketPoint1)//���̊֐��ɂ͍����ɕ\������o�P�c�|�C���g�̒l�𑗂��Ă��������B
    {
        //�n�[�g����������
    }

    public void p2HPDisplay(int BucketPoint2)//���̊֐��ɂ͉E���ɕ\������o�P�c�|�C���g�̒l�𑗂��Ă��������B
    {
        //�n�[�g����������
    }
}
