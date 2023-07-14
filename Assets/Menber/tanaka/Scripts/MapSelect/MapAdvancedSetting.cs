using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapAdvancedSetting : MonoBehaviour
{
    public static bool LockItem = false;
    public static bool LockAttack = false;

    private bool nowMove = false;

    public static bool stopAdvancedSettingKey = true;

    private int cicleAttack = 2;

    //�J�[�\���̈ʒu�Ǘ�
    private int checkBoxSelectNow = 0;

    //�R���g���[���[����̓��͎�t
    float Dy = default;

    [Header("�`�F�b�N�{�b�N�X")]
    [SerializeField]
    List<Image> checkBoxs = new List<Image>();

    [Header("�`�F�b�N�{�b�N�X�e�L�X�g")]
    [SerializeField]
    List<Text> checkBoxText = new List<Text>();

    [Header("�}�b�v�����e�L�X�g")]
    [SerializeField]
    Text featureText = default;

    [Header("�J�[�\��(��)")]
    [SerializeField]
    GameObject cursorSmall = default;

    [Header("�A�C�e���J�M")]
    [SerializeField]
    GameObject lockItem = default;
    [Header("�U���J�M")]
    [SerializeField]
    GameObject lockAttack = default;

    [SerializeField]
    Animator anim;

    void Start()
    {
        GameManager.NotItemCreate = false;
        CountRandom.lockFirst = false;
        CountRandom.lockSecond = false;
        stopAdvancedSettingKey = false;
        cicleAttack = 2;
        checkBoxSelectNow = 0;
    }

    void Update()
    {
        if (!stopAdvancedSettingKey)
        {
            anim.SetBool("animStop", false);
            if (!nowMove)
            {
                Reload();
                Dy = (int)Input.GetAxis("DpadY");
                //�J�[�\�����ړ�
                if (Dy != 0)
                {
                    //������������
                    if (Dy < 0)
                    {
                        if (checkBoxSelectNow >= 2)
                        {
                            nowMove = true;
                            checkBoxSelectNow = 0;
                            StartCoroutine(coolTime());
                        }
                        else
                        {
                            nowMove = true;
                            checkBoxSelectNow++;
                            StartCoroutine(coolTime());
                        }
                    }
                    //�����������
                    else
                    {
                        if (checkBoxSelectNow <= 0)
                        {
                            nowMove = true;
                            checkBoxSelectNow = 2;
                            StartCoroutine(coolTime());
                        }
                        else
                        {
                            nowMove = true;
                            checkBoxSelectNow--;
                            StartCoroutine(coolTime());
                        }
                    }
                }

                if (!nowMove && Input.GetButtonDown("DS4circle"))
                {
                    check();
                }
                if (!nowMove && Input.GetButtonDown("DS4cross"))
                {
                    checkBoxSelectNow = 0;
                    StartCoroutine(coolTime());
                    GameManager.NotItemCreate = false;
                    CountRandom.lockFirst = false;
                    CountRandom.lockSecond = false;
                    cicleAttack = 2;
                    this.gameObject.SetActive(false);
                    lockItem.SetActive(false);
                    lockAttack.SetActive(false);
                    MapSelect.StopMapSelectKey = false;
                }
            }
        }

        if (LockItem && Input.GetKeyDown(KeyCode.F12))
        {
            LockItem = false;
            lockItem.SetActive(true);
            checkBoxText[0].text = "��������";
            GameManager.NotItemCreate = true;
        }
    }

    private IEnumerator coolTime()
    {
        Debug.Log(checkBoxSelectNow);
        cursorSmall.transform.position = checkBoxs[checkBoxSelectNow].transform.position;
        yield return new WaitForSeconds(0.25f);
        nowMove = false;
    }

    public void Reload()
    {
        if (LockItem)
        {
            checkBoxText[0].text = "�������Ȃ�";
            GameManager.NotItemCreate = true;
        }
        if (LockAttack)
        {
            checkBoxText[1].text = "1P ��U";
            checkBoxText[1].color = new Color32(255, 0, 0, 255);
            CountRandom.lockFirst = true;
            CountRandom.lockSecond = false;
        }

    }

    private void check()
    {
        switch (checkBoxSelectNow)
        {
            case 0:
                {
                    if (!LockItem)
                    {
                        if (!GameManager.NotItemCreate)
                        {
                            checkBoxText[checkBoxSelectNow].text = "�������Ȃ�";
                            GameManager.NotItemCreate = true;
                        }
                        else
                        {
                            checkBoxText[checkBoxSelectNow].text = "��������";
                            GameManager.NotItemCreate = false;
                        }
                    }
                    break;
                }
            case 1:
                {
                    if (!LockAttack)
                    {
                        if (cicleAttack >= 2)
                        {
                            cicleAttack = 0;
                        }
                        else
                        {
                            cicleAttack++;
                        }
                        if (cicleAttack == 0)
                        {
                            checkBoxText[checkBoxSelectNow].text = "1P ��U";
                            checkBoxText[checkBoxSelectNow].color = new Color32(255, 0, 0, 255);
                            CountRandom.lockFirst = true;
                            CountRandom.lockSecond = false;
                        }
                        else if (cicleAttack == 1)
                        {
                            checkBoxText[checkBoxSelectNow].text = "2P ��U";
                            checkBoxText[checkBoxSelectNow].color = new Color32(0, 255, 255, 255);
                            CountRandom.lockFirst = false;
                            CountRandom.lockSecond = true;
                        }
                        else
                        {
                            checkBoxText[checkBoxSelectNow].text = "�����_��";
                            checkBoxText[checkBoxSelectNow].color = new Color32(255, 255, 255, 255);
                            CountRandom.lockFirst = false;
                            CountRandom.lockSecond = false;
                        }
                    }
                    break;
                }
            case 2:
                {
                    MoveScene();
                    break;
                }
            default: break;
        }
    }

    public void MoveScene()
    {
        SceneManager.LoadScene("MainScene");
        BGMManager.Instance.PlayBGM(BGMManager.BGM_TYPE.PLAY, 0.6f);//�e�n���M
    }
}
