using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PPDisplay : MonoBehaviour
{
    public static PPDisplay ppDisplay = null;

    [SerializeField]
    [Header("PaintPointText")]
    private Text[] paintPoint;//塗りポイントの表示をするテキストをアタッチしてください
    [SerializeField]
    [Header("BucketPointText")]
    private Text[] bucketPoint;//バケツの個数表示をするテキストをアタッチしてください

    private Text changeUD;

    private void Awake()
    {
        if (ppDisplay == null)
        {
            ppDisplay = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        if(paintPoint[0].name != "PaintPointText1") //配列がどっちの順番でも動くように上下逆の場合変換
        {
            changeUD = paintPoint[0];
            paintPoint[0] = paintPoint[1];
            paintPoint[1] = changeUD;
        }
        if (bucketPoint[0].name != "BucketPointText1") //配列がどっちの順番でも動くように上下逆の場合変換
        {
            changeUD = bucketPoint[0];
            bucketPoint[0] = bucketPoint[1];
            bucketPoint[1] = changeUD;
        }
        paintPoint[0].text = null;
        paintPoint[1].text = null;
        bucketPoint[0].text = null;
        bucketPoint[1].text = null;
        //ppDisplay.BucketDisplay1(10);//呼び出し、表示テスト。この関数をバケツ、塗ポイントの更新時に呼び出して引数でバケツ、塗ポイントの変数を送って下さい。
    }
    public void PointDisplay1(int PaintPoint1)//この関数には左側に表示する塗ポイントの値を送ってください。
    {
        paintPoint[0].text = "" + PaintPoint1;
    }

    public void PointDisplay2(int PaintPoint2)//この関数には右側に表示する塗ポイントの値を送ってください。
    {
        paintPoint[1].text = "" + PaintPoint2;
    }

    public void BucketDisplay1(int BucketPoint1)//この関数には左側に表示するバケツポイントの値を送ってください。
    {
        bucketPoint[0].text = "" + BucketPoint1;
    }

    public void BucketDisplay2(int BucketPoint2)//この関数には右側に表示するバケツポイントの値を送ってください。
    {
        bucketPoint[1].text = "" + BucketPoint2;
    }
}
