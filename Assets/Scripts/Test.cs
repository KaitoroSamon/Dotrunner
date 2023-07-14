using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public PlayerMove1 player1;
    public PlayerMove2 player2;

    private bool isPlayer1Turn = true; // Player1�̃^�[�����ǂ����̃t���O

    // Update is called once per frame
    void Update()
    {
        if (isPlayer1Turn)
        {
            Player1Turn();
        }
        else
        {
            Player2Turn();
        }

        // �^�[���I�������i���j�F�X�y�[�X�L�[�������ꂽ�ꍇ
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPlayer1Turn = !isPlayer1Turn; // �^�[����؂�ւ���
        }

        if (Input.GetKeyDown(KeyCode.Escape))//���s�̔��ʂ�������X�R�A���Ɉړ����鏈���i���j
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
#endif
        }
    }

    void Player1Turn()
    {
        player1.enabled = true; // Player1��L���ɂ���
        player2.enabled = false; // Player2�𖳌��ɂ���
    }

    void Player2Turn()
    {
        player1.enabled = false; // Player1�𖳌��ɂ���
        player2.enabled = true; // Player2��L���ɂ���
    }
}
