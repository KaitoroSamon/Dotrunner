using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
3-�Q�[���p�b�h�̏\���L�[���͂őI���ł���悤�ɂ���
3.5-�I�𒆂̃}�X�̕����ɃG�t�F�N�g�����킩��₷���悤�ɂ���
4-B�L�[���͌�}�X�̐F��ύX����
5-�Ō�ɐF��ύX���������Ƀv���C���[���ړ�������
5.1-�A�j���[�V�����̒ǉ��i�������ړ�������j
5.2-�A�C�e��������}�X��I��������A�C�e���̎擾�A�������s��
6-������l�̃v���C���[�̓�����󂯕t����
7-����̃}�X�ɏ������I��������[����̏�ɂ��ǂ蒅���ۂ͓h�|�C���g�҂�����g��Ȃ��ƃS�[���ł��Ȃ�]
8-�v���C���[��HP���Ǘ�����[����Ɠ����}�X�ɏ��ƍU��(�U�������������̎����̃^�[���s���s��)]
9-�}�X�̂Ȃ��ꏊ�ɂ͈ړ��ł��Ȃ��悤�ɂ���
10-�^�[���I���{�^����X�L�[�ɓ����
11-�L�����I����ʂŎ󂯎����ID�ɍ��킹�ăv���C���[�X�L����ύX����
*/

public class PlayerManager : MonoBehaviour
{

    //�����̃^�[�����ǂ����H(�}�l�[�W���[����󂯎��)
    private bool myTrun = false;

    //���̎����̍��W(�}�l�[�W���[����󂯎��)
    private Vector2 nowPos = default;

    //�ړ��ł����
    private int moveCounter = default;

    //�h��ւ������
    private int rePaint = default;

    //�I�𒆂̕���
    private Vector2 nowDirection = default;

    //�R���g���[���[����̃L�[�󂯎��p
    float Dx = default;
    float Dy = default;

    void Start()
    {
        //�X�L���ݒ�
    }

    //�v���C���[�̑��Έړ��x(�ꊇ�ړ��͌��)
    void Update()
    {
        //���^�[���̂ݓ�����
        if (myTrun)
        {
            Debug.Log("<color=green> TrunChange! </color>");
            Dx = Input.GetAxis("DpadX");
            Dy = Input.GetAxis("DpadY");
            nowDirection = new Vector2(Dx,Dy);

            //�J�[�\�����ړ�


            //�I��
            Dx = Input.GetAxis("DpadX");
            Dy = Input.GetAxis("DpadY");
            //�ǂ��炩�𓮂�������
            if (Dx != 0 && Dy != 0)
            {
                nowDirection += new Vector2(Dx, Dy);
                //�v���C���[�̍��W�Ɍ��݂̍��W�𑫂������l���}�l�[�W���[�ɓn��
                nowPos += nowDirection;
                //�}�l�[�W���[�œh�����}�X�������𔻕ʂ���d�g�݂��K�v

                //���h�����̂ōs�����ꌸ�炷
                moveCounter--;
            }

            //�s���񐔂�0�ɂȂ邩
            //�^�[���I�����{�^������������I��
            if (moveCounter <= 0)
            {
                myTrun = false;
                //�}�l�[�W���[�ɂ��I�������ƕԂ�

            }
            if (Input.GetButtonDown("DS4cross"))
            {
                //�ēx�m�F

                //�}�l�[�W���[�ɂ��I�������ƕԂ�

                //�s���񐔂��O�ɂ���(�o�O�΍�)
                moveCounter = 0;
                myTrun = false;
            }

        }


    }

    /// <summary>
    /// �v���C���[��񑗂��(�Q�[���}�l�[�W���[���)
    /// </summary>
    /// <param name="playerPos"></param>
    public void PlayerUpdate(bool nowTurn, Vector2 playerPos, int playerMove,int Repaint)
    {
        myTrun = nowTurn;
        nowPos = playerPos;
        moveCounter = playerMove;
        rePaint = Repaint;

    }
}
