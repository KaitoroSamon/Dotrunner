using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controllerDebug : MonoBehaviour
{
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
            Debug.Log("��");
        }
        if (Input.GetButtonDown("DS4cross"))
        {
            //�~
            Debug.Log("�~");
        }
        if (Input.GetButtonDown("DS4circle"))
        {
            //�Z
            Debug.Log("�Z");
        }
        if (Input.GetButtonDown("DS4triangle"))
        {
            //��
            Debug.Log("��");
        }

        //���X�e�B�b�N
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        if (x != 0)
        {
            Debug.Log(x);
        }
        if (y != 0)
        {
            Debug.Log(y);
        }


        //�\���L�[ 
        //����DpadX(�E���u+�v�A�����u-�v)
        //�c��DpadY(�オ�u+�v�A�����u-�v)
        float Dx = Input.GetAxis("DpadX");
        float Dy = Input.GetAxis("DpadY");
        if (Dx != 0)
        {
            Debug.Log("DpadX: " + Dx);
        }
        if (Dy != 0)
        {
            Debug.Log("DpadY :" + Dy);
        }

    }
}
