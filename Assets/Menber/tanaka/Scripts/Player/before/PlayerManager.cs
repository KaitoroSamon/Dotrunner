using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    Animator animator;
    public static Map map;
    public static GameManager gameManager;
    [SerializeField]
    GameObject mapScrits;
    [SerializeField]
    GameObject gameManagerScripts;

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
    bool nowMove = false;

    [SerializeField]
    GameObject cursor;
    Image cursorImage;

    GameObject playerModel;

    private void Awake()
    {
        map = mapScrits.GetComponent<Map>();
        gameManager = gameManagerScripts.GetComponent<GameManager>();
        cursorImage = cursor.GetComponent<Image>();
        playerModel = transform.Find("PlayerModel").gameObject;
        //�������W
        //this.gameObject.transform.position = new Vector3(-7.5f, -0.5f, 0);
        //cursor.transform.position = new Vector3(-7.5f, -0.5f, 0);
    }

    void Start()
    {
        animator = cursor.GetComponent<Animator>();

        //�X�L���ݒ�
    }

    //�v���C���[�̑��Έړ��x(�ꊇ�ړ��͌��)
    void Update()
    {
        //���^�[���̂ݓ�����
        if (myTrun)
        {
            cursorImage.color = new Color32(255, 0, 217, 138);
            animator.SetBool("Selection",true);

            if (!nowMove)
            {
                Dx = (int)Input.GetAxis("DpadX");
                Dy = (int)Input.GetAxis("DpadY");

                //�J�[�\�����ړ�
                if (Dx != 0 || Dy != 0)
                {
                    nowMove = true;
                    StartCoroutine(cursorMove());
                }
            }

            //�h��
            if (!nowMove && Input.GetButtonDown("DS4circle"))
            {
                nowMove = true;
                StartCoroutine(map.paintRedMap(nowDirection));
                //�v���C���[�̍��W�Ɍ��݂̍��W�𑫂������l���}�l�[�W���[�ɓn��
                if (map.paint)
                {
                    //�}�b�v��������
                    map.mapRemake();

                    //�v���C���[���f���𓮂���
                    StartCoroutine(playerAnimation());

                    //���h�����̂ōs�����ꌸ�炷
                    moveCounter--;
                    map.paint = false;
                }
                else
                {
                    Debug.Log("<color=red>�אڂ����}�X������܂���B</color>");
                    nowMove = false;
                }
            }

            //�s���񐔂�0�ɂȂ邩
            //�^�[���I�����{�^������������I��
            if (!nowMove && moveCounter <= 0)
            {
                nowMove = true;
                animator.SetBool("Selection", false);
                myTrun = false;
                //�}�l�[�W���[�ɂ��I�������ƕԂ�
                gameManager.trunChange();
                //�^�[���I�����J�[�\���𓧖���
                cursorImage.color = new Color32(255, 0, 217, 0);
                cursor.transform.position = this.transform.position;
                nowMove = false;
            }
            if (!nowMove && Input.GetButtonDown("DS4cross"))
            {
                nowMove = true;
                //�ēx�m�F

                animator.SetBool("Selection", false);
                //�}�l�[�W���[�ɂ��I�������ƕԂ�
                gameManager.trunChange();
                //�s���񐔂��O�ɂ���(�o�O�΍�)
                moveCounter = 0;
                myTrun = false;
                //�^�[���I�����J�[�\���𓧖��ɂ���
                cursorImage.color = new Color32(255, 0, 217, 0);
                cursor.transform.position = this.transform.position;
                nowMove = false;
            }
        }
    }

    /// <summary>
    /// �J�[�\���𓮂�������
    /// </summary>
    /// <returns></returns>
    private IEnumerator cursorMove()
    {
        cursor.transform.position = new Vector3(cursor.transform.position.x + Dx, cursor.transform.position.y + Dy, cursor.transform.position.z);
        nowDirection = new Vector2(nowDirection.x+(int)Dx, nowDirection.y-(int)Dy);

        yield return new WaitForSeconds(0.25f);
        nowMove = false;
    }

    /// <summary>
    /// �v���C���[���f���̈ړ��A�j���[�V��������
    /// </summary>
    /// <returns></returns>
    private IEnumerator playerAnimation()
    {
        //�h�����}�X�Ɉړ�����
        playerModel.transform.position = new Vector3(cursor.transform.position.x + Dx,
            cursor.transform.position.y + Dy,
            playerModel.transform.position.z);

        cursor.transform.position = this.transform.position;
        yield return null;
        nowMove = false;
    }

    /// <summary>
    /// �v���C���[��񑗂��(�Q�[���}�b�v���)
    /// </summary>
    /// <param name="playerPos"></param>
    public void PlayerUpdate(Vector2 playerPos)
    {
        nowPos = playerPos;
        this.gameObject.transform.position = nowPos;
        nowDirection = nowPos;
    }
    /// <summary>
    /// �v���C���[�̏�񑗂��(�Q�[���}�l�[�W���[���)
    /// </summary>
    /// <param name="nowTurn"></param>
    public void PlayertrunUpdate(bool nowTurn, int playerMove, int Repaint)
    {
        myTrun = nowTurn;
        moveCounter = playerMove;
        rePaint = Repaint;
    }
}
