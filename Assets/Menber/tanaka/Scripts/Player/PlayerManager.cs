using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
5-最後に色を変更した方向にプレイヤーを移動させる
5.1-アニメーションの追加（ゆっくり移動させる）
5.2-アイテムがあるマスを選択したらアイテムの取得、発動を行う
6-もう一人のプレイヤーの動作を受け付ける
7-特定のマスに乗ったら終了させる[相手の城にたどり着く際は塗ポイントぴったり使わないとゴールできない]
8-プレイヤーのHPを管理する[相手と同じマスに乗ると攻撃(攻撃した側が次の自分のターン行動不可)]
11-キャラ選択画面で受け取ったIDに合わせてプレイヤースキンを変更する
*/

public class PlayerManager : MonoBehaviour
{
    string Horizontal = "DpadX";
    string Vertical = "DpadY";
    string select = "DS4circle";
    string end = "DS4circle";

    //自分のターンかどうか？(マネージャーから受け取り)
    private bool myTrun = false;

    //今の自分の座標(マネージャーから受け取り)
    private Vector2 nowPos = default;

    //移動できる回数
    private int moveCounter = default;

    //塗り替えせる回数
    private int rePaint = default;

    //選択中の方向
    private Vector2 nowDirection = default;

    //コントローラーからのキー受け取り用
    float Dx = default;
    float Dy = default;

    void Start()
    {

    }

    //プレイヤーの相対移動度(一括移動は後で)
    void Update()
    {
        //自ターンのみ動かす
        if (myTrun)
        {
            Debug.Log("<color=green> TrunChange! </color>");
            Dx = Input.GetAxis(Horizontal);
            Dy = Input.GetAxis(Vertical);
            nowDirection = new Vector2(Dx,Dy);

            //[0,0]じゃなければ([0,0]は自分と同じマス)
            if (nowDirection != new Vector2(0,0))
            {
                //カーソルを移動


                //選択
                if (Input.GetButtonDown(select))
                {
                    //プレイヤーの座標に現在の座標を足した数値をマネージャーに渡す
                    nowPos += nowDirection;
                    //マネージャーで塗ったマスが何かを判別する仕組みが必要

                    //一回塗ったので行動を一減らす
                    moveCounter--;
                }

            }

            //行動回数が0になるか
            //ターン終了時ボタンを押したら終了
            if(moveCounter <= 0)
            {
                myTrun = false;
                //マネージャーにも終了したと返す

            }
            if (Input.GetButtonDown(end))
            {
                //再度確認

                //マネージャーにも終了したと返す

                //行動回数を０にする(バグ対策)
                moveCounter = 0;
                myTrun = false;
            }

        }


    }

    /// <summary>
    /// プレイヤー情報送り先(ゲームマネージャーより)
    /// </summary>
    /// <param name="playerPos"></param>
    public void PlayerUpdate(bool nowTurn, Vector2 playerPos, int playerMove,int Repaint)
    {
        myTrun = nowTurn;
        nowPos = playerPos;
        moveCounter = playerMove;
        rePaint = Repaint;

    }
}
