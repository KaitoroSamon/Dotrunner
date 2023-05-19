using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimationList : MonoBehaviour //使えるか不明瞭ですがアニメーションのリストです。
{
    public enum CURSORANIMATION_TYPE//カーソルのアニメーションリスト
    {
        STILLNESS,//一番最初のアニメーションしない状態
        SELECTING,//選択中で拡縮のアニメーションを行う状態
        SELECTED,//選択が完了し再び停止する状態
    }

    //public enum PLAYERANIMATION_TYPE //他にアニメーションが付くかわかりませんがテスト用に作成したプレイヤーのアニメーションリスト
    //{
    //    AWAIT,//プレイヤー待機中のアニメーション
    //    WALK,//プレイヤーが動いてるときのアニメーション
    //}

    public CURSORANIMATION_TYPE cursorAnim_type;//アニメーションの種類を変更するための変数
    //public PLAYERANIMATION_TYPE playerAnim_type;
    void Start()
    {
        cursorAnim_type = CURSORANIMATION_TYPE.STILLNESS;//変数初期化
        //playerAnim_type = PLAYERANIMATION_TYPE.AWAIT;
    }

}

