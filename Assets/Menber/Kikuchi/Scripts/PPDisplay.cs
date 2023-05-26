using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PPDisplay : MonoBehaviour
{
    public static PPDisplay ppDisplay = null;

    [SerializeField]
    [Header("PaintPointText")]
    private Text[] paintPoint;//�h��|�C���g�̕\��������e�L�X�g���A�^�b�`���Ă�������
    [SerializeField]
    [Header("BucketPointText")]
    private Text[] bucketPoint;//�o�P�c�̌��\��������e�L�X�g���A�^�b�`���Ă�������

    private Text changeUD;

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
    }
    private void Start()
    {
        if(paintPoint[0].name != "PaintPointText1") //�z�񂪂ǂ����̏��Ԃł������悤�ɏ㉺�t�̏ꍇ�ϊ�
        {
            changeUD = paintPoint[0];
            paintPoint[0] = paintPoint[1];
            paintPoint[1] = changeUD;
        }
        if (bucketPoint[0].name != "BucketPointText1") //�z�񂪂ǂ����̏��Ԃł������悤�ɏ㉺�t�̏ꍇ�ϊ�
        {
            changeUD = bucketPoint[0];
            bucketPoint[0] = bucketPoint[1];
            bucketPoint[1] = changeUD;
        }
        paintPoint[0].text = null;
        paintPoint[1].text = null;
        bucketPoint[0].text = null;
        bucketPoint[1].text = null;
        //ppDisplay.BucketDisplay1(10);//�Ăяo���A�\���e�X�g�B���̊֐����o�P�c�A�h�|�C���g�̍X�V���ɌĂяo���Ĉ����Ńo�P�c�A�h�|�C���g�̕ϐ��𑗂��ĉ������B
    }
    public void PointDisplay1(int PaintPoint1)//���̊֐��ɂ͍����ɕ\������h�|�C���g�̒l�𑗂��Ă��������B
    {
        paintPoint[0].text = "" + PaintPoint1;
    }

    public void PointDisplay2(int PaintPoint2)//���̊֐��ɂ͉E���ɕ\������h�|�C���g�̒l�𑗂��Ă��������B
    {
        paintPoint[1].text = "" + PaintPoint2;
    }

    public void BucketDisplay1(int BucketPoint1)//���̊֐��ɂ͍����ɕ\������o�P�c�|�C���g�̒l�𑗂��Ă��������B
    {
        bucketPoint[0].text = "" + BucketPoint1;
    }

    public void BucketDisplay2(int BucketPoint2)//���̊֐��ɂ͉E���ɕ\������o�P�c�|�C���g�̒l�𑗂��Ă��������B
    {
        bucketPoint[1].text = "" + BucketPoint2;
    }
}
