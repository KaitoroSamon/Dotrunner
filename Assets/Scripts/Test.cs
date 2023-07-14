using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public PlayerMove1 player1;
    public PlayerMove2 player2;

    private bool isPlayer1Turn = true; // Player1のターンかどうかのフラグ

    // Update is called once per frame
    void Update()
    {
        if (isPlayer1Turn)
        {
            Player1Turn();
        }
        else
        {
            Player2Turn();
        }

        // ターン終了条件（仮）：スペースキーが押された場合
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPlayer1Turn = !isPlayer1Turn; // ターンを切り替える
        }

        if (Input.GetKeyDown(KeyCode.Escape))//勝敗の判別をしたらスコア等に移動する処理（仮）
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#endif
        }
    }

    void Player1Turn()
    {
        player1.enabled = true; // Player1を有効にする
        player2.enabled = false; // Player2を無効にする
    }

    void Player2Turn()
    {
        player1.enabled = false; // Player1を無効にする
        player2.enabled = true; // Player2を有効にする
    }
}
