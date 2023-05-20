using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
3-ゲームパッドの十字キー入力で選択できるようにする
3.5-選択中のマスの方向にエフェクトを入れわかりやすいようにする
4-Bキー入力後マスの色を変更する
5-最後に色を変更した方向にプレイヤーを移動させる
5.1-アニメーションの追加（ゆっくり移動させる）
5.2-アイテムがあるマスを選択したらアイテムの取得、発動を行う
6-もう一人のプレイヤーの動作を受け付ける
7-特定のマスに乗ったら終了させる[相手の城にたどり着く際は塗ポイントぴったり使わないとゴールできない]
8-プレイヤーのHPを管理する[相手と同じマスに乗ると攻撃(攻撃した側が次の自分のターン行動不可)]
9-マスのない場所には移動できないようにする
10-ターン終了ボタンをXキーに入れる
11-キャラ選択画面で受け取ったIDに合わせてプレイヤースキンを変更する
*/

public class PlayerManager : MonoBehaviour
{
    Animator animator;
    public static Map map;
    public static GameManager gameManager;
    [SerializeField]
    GameObject mapScrits;
    [SerializeField]
    GameObject gameManagerScripts;

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
    bool nowMove = false;

    [SerializeField]
    GameObject cursor;

    private void Awake()
    {
        map = mapScrits.GetComponent<Map>();
        gameManager = gameManagerScripts.GetComponent<GameManager>();

        //初期座標
        //this.gameObject.transform.position = new Vector3(-7.5f, -0.5f, 0);
        //cursor.transform.position = new Vector3(-7.5f, -0.5f, 0);
    }

    void Start()
    {
        animator = cursor.GetComponent<Animator>();

        //スキン設定
    }

    //プレイヤーの相対移動度(一括移動は後で)
    void Update()
    {
        //自ターンのみ動かす
        if (myTrun)
        {
            animator.SetBool("Selection",true);

            if (!nowMove)
            {
                Dx = (int)Input.GetAxis("DpadX");
                Dy = (int)Input.GetAxis("DpadY");

                //カーソルを移動
                if (Dx != 0 || Dy != 0)
                {
                    nowMove = true;
                    StartCoroutine("cursorMove");
                }
            }

            //塗り
            if (Input.GetButtonDown("DS4circle"))
            {
                //プレイヤーの座標に現在の座標を足した数値をマネージャーに渡す
                if (map.paintRedMap(nowDirection))
                {
                    //塗ったマスに移動する

                    //一回塗ったので行動を一減らす
                    moveCounter--;
                }
                else
                {
                    //Debug.Log("<color=red>隣接したマスがありません。</color>");
                }
            }

            //行動回数が0になるか
            //ターン終了時ボタンを押したら終了
            if (moveCounter <= 0)
            {
                animator.SetBool("Selection", false);
                myTrun = false;
                //マネージャーにも終了したと返す
                gameManager.trunChange();
            }
            if (Input.GetButtonDown("DS4cross"))
            {
                //再度確認

                animator.SetBool("Selection", false);
                //マネージャーにも終了したと返す
                gameManager.trunChange();
                //行動回数を０にする(バグ対策)
                moveCounter = 0;
                myTrun = false;
            }

            //ターン終了時カーソルを透明感
        }
    }

    private IEnumerator cursorMove()
    {
        cursor.transform.position = new Vector3(cursor.transform.position.x + Dx, cursor.transform.position.y + Dy, cursor.transform.position.z);
        nowDirection = new Vector2(nowDirection.x+(int)Dx, nowDirection.y+(int)Dy);

        yield return new WaitForSeconds(0.25f);
        nowMove = false;

    }

    /// <summary>
    /// プレイヤー情報送り先(ゲームマップより)
    /// </summary>
    /// <param name="playerPos"></param>
    public void PlayerUpdate(Vector2 playerPos)
    {
        nowPos = playerPos;
        this.gameObject.transform.position = nowPos;
        nowDirection = nowPos;
    }
    /// <summary>
    /// プレイヤーの情報送り先(ゲームマネージャーより)
    /// </summary>
    /// <param name="nowTurn"></param>
    public void PlayertrunUpdate(bool nowTurn, int playerMove, int Repaint)
    {
        myTrun = nowTurn;
        moveCounter = playerMove;
        rePaint = Repaint;
    }
}
