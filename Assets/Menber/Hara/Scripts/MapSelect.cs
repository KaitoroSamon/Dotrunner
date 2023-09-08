using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapSelect : MonoBehaviour
{
    //�J�[�\���̈ʒu�Ǘ�
    private int buttonSelectNow = 0;

    //�R���g���[���[����̓��͎�t
    float Dx = default;
    float Dy = default;

    private bool nowMove = false;

    public static bool StopMapSelectKey = false;

    [Header("�{�^��")]
    [SerializeField]
    List<GameObject> button = new List<GameObject>();

    [Header("�J�[�\��(��)")]
    [SerializeField]
    GameObject cursorLarge = default;

    [Header("���}�b�v�̕\��")]
    [SerializeField]
    GameObject tentative = default;

    [Header("�}�b�v���C���[�W�I�u�W�F�N�g")]
    [SerializeField]
    Image mapImage = default;

    [Header("�}�b�v���C���[�W")]
    [SerializeField]
    List<Sprite> tentativeImage = new List<Sprite>();

    [Header("�}�b�v�̓����e�L�X�g�I�u�W�F�N�g")]
    [SerializeField]
    Text featureText = default;

    [Header("�}�b�v�̓����e�L�X�g")]
    [SerializeField]
    List<string> tentativefeatureText = new List<string>();

    [Header("�A�C�e���J�M")]
    [SerializeField]
    GameObject lockItem = default;
    [Header("�U���J�M")]
    [SerializeField]
    GameObject lockAttack = default;

    private GameObject BGMGameobject;
    private SEManager seManager;

    [SerializeField]
    Animator anim;

    private void Start()
    {
        buttonSelectNow = 0;
        StopMapSelectKey = false;
        tentative.SetActive(false);
        lockItem.SetActive(false);
        lockAttack.SetActive(false);


        BGMGameobject = GameObject.Find("SEManager");
        seManager = BGMGameobject.GetComponent<SEManager>();
    }
    private void Update()
    {
        if (!StopMapSelectKey)
        {
            anim.SetBool("animStop", false);
            if (!nowMove)
            {
                Dx = (int)Input.GetAxis("DpadX");
                Dy = (int)Input.GetAxis("DpadY");
                //�J�[�\�����ړ�
                if (Dx != 0)
                {
                    //������������
                    if (Dx < 0)
                    {
                        if (buttonSelectNow <= 0)
                        {
                            nowMove = true;
                            buttonSelectNow = 5;
                            StartCoroutine(coolTime());
                        }
                        else
                        {
                            nowMove = true;
                            buttonSelectNow--;
                            StartCoroutine(coolTime());
                        }

                        seManager.Play((SEManager.SE_TYPE)0);
                    }
                    //�E����������
                    else
                    {
                        if (buttonSelectNow >= 5)
                        {
                            nowMove = true;
                            buttonSelectNow = 0;
                            StartCoroutine(coolTime());
                        }
                        else
                        {
                            nowMove = true;
                            buttonSelectNow++;
                            StartCoroutine(coolTime());
                        }
                        seManager.Play((SEManager.SE_TYPE)0);
                    }
                }
                if (Dy != 0)
                {
                    //������������
                    if (Dy < 0)
                    {
                        if (buttonSelectNow >= 3)
                        {
                            nowMove = true;
                            buttonSelectNow = (buttonSelectNow - 5) + 2;
                            StartCoroutine(coolTime());
                        }
                        else
                        {
                            nowMove = true;
                            buttonSelectNow = buttonSelectNow + 3;
                            StartCoroutine(coolTime());
                        }
                        seManager.Play((SEManager.SE_TYPE)0);
                    }
                    //�����������
                    else
                    {
                        if (buttonSelectNow <= 2)
                        {
                            nowMove = true;
                            buttonSelectNow = buttonSelectNow + 3;
                            StartCoroutine(coolTime());
                        }
                        else
                        {
                            nowMove = true;
                            buttonSelectNow = (buttonSelectNow - 5) + 2;
                            StartCoroutine(coolTime());
                        }
                        seManager.Play((SEManager.SE_TYPE)0);
                    }
                }

                if (!nowMove && Input.GetButtonDown("DS4circle"))
                {
                    switch (buttonSelectNow)
                    {
                        case 0:
                            TutorialMapOnePlayer();
                            mapImage.sprite = tentativeImage[buttonSelectNow];
                            break;
                        case 1:
                            BaseMap();
                            mapImage.sprite = tentativeImage[buttonSelectNow];
                            break;
                        case 2:
                            kaneko_Map2_ver1();
                            mapImage.sprite = tentativeImage[buttonSelectNow];
                            break;
                        case 3:
                            kaneko_Map3_ver3();
                            mapImage.sprite = tentativeImage[buttonSelectNow];
                            break;
                        case 4:
                            Tomita_Map_Sample4();
                            mapImage.sprite = tentativeImage[buttonSelectNow];
                            break;
                        case 5:
                            TitleBack();
                            break;
                        default: break;
                    }
                    if(buttonSelectNow != 5)
                    {
                        tentative.SetActive(true);
                        lockItem.SetActive(MapAdvancedSetting.LockItem);
                        lockAttack.SetActive(MapAdvancedSetting.LockAttack);
                        StopMapSelectKey = true;
                        anim.SetBool("animStop", true);
                        MapAdvancedSetting.stopAdvancedSettingKey = false;
                    }
                    seManager.Play((SEManager.SE_TYPE)1);
                }
            }
        }
    }

    private IEnumerator coolTime()
    {
        Debug.Log(buttonSelectNow);
        cursorLarge.transform.position = button[buttonSelectNow].transform.position;
        yield return new WaitForSeconds(0.25f);
        nowMove = false;
    }

    #region ButtonScripts
    public void TutorialMapOnePlayer()
    {
        tutorialManager.tutorialNow=true;
        MapAdvancedSetting.LockItem = true;
        MapAdvancedSetting.LockAttack = true;
        StartSetting.test = "/StreamingAssets/csv/TutorialMapOnePlayer.csv";
    }

    public void BaseMap()
    {
        tutorialManager.tutorialNow=false;
        MapAdvancedSetting.LockItem = false;
        MapAdvancedSetting.LockAttack = false;
        StartSetting.test = "/StreamingAssets/csv/BaseMap.csv";
    }

    public void kaneko_Map2_ver1()
    {
        tutorialManager.tutorialNow=false;
        MapAdvancedSetting.LockItem = true;
        MapAdvancedSetting.LockAttack = false;
        StartSetting.test = "/StreamingAssets/csv/kaneko_Map2_ver1.csv";
    }

    public void kaneko_Map3_ver3()
    {
        tutorialManager.tutorialNow=false;
        MapAdvancedSetting.LockItem = true;
        MapAdvancedSetting.LockAttack = false;
        StartSetting.test = "/StreamingAssets/csv/kaneko_Map3_ver3.csv";
    }

    public void Tomita_Map_Sample4()
    {
        tutorialManager.tutorialNow=false;
        MapAdvancedSetting.LockItem = true;
        MapAdvancedSetting.LockAttack = false;
        StartSetting.test = "/StreamingAssets/csv/Tomita_Map_Sample4.csv";
    }

    public void TitleBack()
    {
        SceneManager.LoadScene("TitleScene");
        BGMManager.Instance.PlayBGM(BGMManager.BGM_TYPE.TITLE, 0.6f);
    }
#endregion
}
