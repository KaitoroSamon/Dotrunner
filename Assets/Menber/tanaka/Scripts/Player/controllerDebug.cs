using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controllerDebug : MonoBehaviour
{
    void Start()
    {
        Debug.Log("デバッグモード");
    }

    void Update()
    {
        //if(Input.GetButtonDown("キーコード(String型)){}"
        if (Input.GetButtonDown("DS4square"))
        {
            //□
            Debug.Log("□");
        }
        if (Input.GetButtonDown("DS4cross"))
        {
            //×
            Debug.Log("×");
        }
        if (Input.GetButtonDown("DS4circle"))
        {
            //〇
            Debug.Log("〇");
        }
        if (Input.GetButtonDown("DS4triangle"))
        {
            //△
            Debug.Log("△");
        }

        //左スティック
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


        //十字キー 
        //横→DpadX(右が「+」、左が「-」)
        //縦→DpadY(上が「+」、下が「-」)
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
