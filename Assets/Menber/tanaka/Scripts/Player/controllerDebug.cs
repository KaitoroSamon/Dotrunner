using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controllerDebug : MonoBehaviour
{

    /* ���̓L�[�R�[�h
     * �v���C���[1
     * DS4square(��)
     * DS4cross(�~)
     * DS4circle(�Z)
     * DS4Triangle(��)
     * DpadX - �\���L�[��(�E+�@��-)
     * DpadY - �\���L�[�c(��+�@��-)
     * 
     * DS4square2(��)
     * DS4cross2(�~)
     * DS4circle2(�Z)
     * DS4Triangle2(��)
     * DpadX2 - �\���L�[��(�E+�@��-)
     * DpadY2 - �\���L�[�c(��+�@��-)
     */
    void Start()
    {
        Debug.Log("�f�o�b�O���[�h");
    }

    void Update()
    {
        //if(Input.GetButtonDown("�L�[�R�[�h(String�^)){}"
        if (Input.GetButtonDown("DS4square"))
        {
            //��
            Debug.Log("<color=red>��</color>");
        }
        if (Input.GetButtonDown("DS4cross"))
        {
            //�~
            Debug.Log("<color=red>�~</color>");
        }
        if (Input.GetButtonDown("DS4circle"))
        {
            //�Z
            Debug.Log("<color=red>�Z</color>");
        }
        if (Input.GetButtonDown("DS4triangle"))
        {
            //��
            Debug.Log("<color=red>��</color>");
        }

        //2

        if (Input.GetButtonDown("DS4square2"))
        {
            //��
            Debug.Log("<color=green>��</color>");
        }
        if (Input.GetButtonDown("DS4cross2"))
        {
            //�~
            Debug.Log("<color=green>�~</color>");
        }
        if (Input.GetButtonDown("DS4circle2"))
        {
            //�Z
            Debug.Log("<color=green>�Z</color>");
        }
        if (Input.GetButtonDown("DS4triangle2"))
        {
            //��
            Debug.Log("<color=green>��</color>");
        }

        //�\���L�[ 
        //����DpadX(�E���u+�v�A�����u-�v)
        //�c��DpadY(�オ�u+�v�A�����u-�v)
        float Dx = Input.GetAxis("DpadX");
        float Dy = Input.GetAxis("DpadY");
        if (Dx != 0)
        {
            Debug.Log("DpadX : " + Dx);
        }
        if (Dy != 0)
        {
            Debug.Log("DpadY :" + Dy);
        }



        float Dx2 = Input.GetAxis("DpadX2");
        float Dy2 = Input.GetAxis("DpadY2");
        if (Dx2 != 0)
        {
            Debug.Log("DpadX2 : " + Dx2);
        }
        if (Dy2 != 0)
        {
            Debug.Log("DpadY2 :" + Dy2);
        }
    }
}
