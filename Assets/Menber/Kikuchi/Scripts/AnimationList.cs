using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimationList : MonoBehaviour //�g���邩�s���Ăł����A�j���[�V�����̃��X�g�ł��B
{
    public enum CURSORANIMATION_TYPE//�J�[�\���̃A�j���[�V�������X�g
    {
        STILLNESS,//��ԍŏ��̃A�j���[�V�������Ȃ����
        SELECTING,//�I�𒆂Ŋg�k�̃A�j���[�V�������s�����
        SELECTED,//�I�����������Ăђ�~������
    }

    //public enum PLAYERANIMATION_TYPE //���ɃA�j���[�V�������t�����킩��܂��񂪃e�X�g�p�ɍ쐬�����v���C���[�̃A�j���[�V�������X�g
    //{
    //    AWAIT,//�v���C���[�ҋ@���̃A�j���[�V����
    //    WALK,//�v���C���[�������Ă�Ƃ��̃A�j���[�V����
    //}

    public CURSORANIMATION_TYPE cursorAnim_type;//�A�j���[�V�����̎�ނ�ύX���邽�߂̕ϐ�
    //public PLAYERANIMATION_TYPE playerAnim_type;
    void Start()
    {
        cursorAnim_type = CURSORANIMATION_TYPE.STILLNESS;//�ϐ�������
        //playerAnim_type = PLAYERANIMATION_TYPE.AWAIT;
    }

}

