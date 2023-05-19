using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controllerDebug : MonoBehaviour
{

    /* 入力キーコード
     * プレイヤー1
     * DS4square(□)
     * DS4cross(×)
     * DS4circle(〇)
     * DS4Triangle(△)
     * DpadX - 十字キー横(右+　左-)
     * DpadY - 十字キー縦(上+　下-)
     * 
     * DS4square2(□)
     * DS4cross2(×)
     * DS4circle2(〇)
     * DS4Triangle2(△)
     * DpadX2 - 十字キー横(右+　左-)
     * DpadY2 - 十字キー縦(上+　下-)
     */
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
            Debug.Log("<color=red>□</color>");
        }
        if (Input.GetButtonDown("DS4cross"))
        {
            //×
            Debug.Log("<color=red>×</color>");
        }
        if (Input.GetButtonDown("DS4circle"))
        {
            //〇
            Debug.Log("<color=red>〇</color>");
        }
        if (Input.GetButtonDown("DS4triangle"))
        {
            //△
            Debug.Log("<color=red>△</color>");
        }

        //2

        if (Input.GetButtonDown("DS4square2"))
        {
            //□
            Debug.Log("<color=green>□</color>");
        }
        if (Input.GetButtonDown("DS4cross2"))
        {
            //×
            Debug.Log("<color=green>×</color>");
        }
        if (Input.GetButtonDown("DS4circle2"))
        {
            //〇
            Debug.Log("<color=green>〇</color>");
        }
        if (Input.GetButtonDown("DS4triangle2"))
        {
            //△
            Debug.Log("<color=green>△</color>");
        }

        //十字キー 
        //横→DpadX(右が「+」、左が「-」)
        //縦→DpadY(上が「+」、下が「-」)
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
