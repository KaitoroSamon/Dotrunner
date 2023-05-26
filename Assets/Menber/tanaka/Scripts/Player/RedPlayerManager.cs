using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedPlayerManager : MonoBehaviour
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
    public Vector2 nowDirection = default;

    //コントローラーからのキー受け取り用
    float Dx = default;
    float Dy = default;
    bool nowMove = false;

    [SerializeField]
    GameObject cursor;
    Image cursorImage;

    GameObject playerModel;

    private void Awake()
    {
        map = mapScrits.GetComponent<Map>();
        gameManager = gameManagerScripts.GetComponent<GameManager>();
        cursorImage = cursor.GetComponent<Image>();
        playerModel = transform.Find("PlayerModel").gameObject;
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
            cursorImage.color = new Color32(255, 0, 30, 138);
            animator.SetBool("Selection", true);

            if (!nowMove)
            {
                Dx = (int)Input.GetAxis("DpadX");
                Dy = (int)Input.GetAxis("DpadY");

                //カーソルを移動
                if (Dx != 0 || Dy != 0)
                {
                    nowMove = true;
                    StartCoroutine(cursorMove());
                }
            }

            //塗り
            if (!nowMove && Input.GetButtonDown("DS4circle"))
            {
                nowMove = true;
                StartCoroutine(map.paintRedMap(nowDirection));
                //プレイヤーの座標に現在の座標を足した数値をマネージャーに渡す
                if (map.paint)
                {
                    //マップ書き換え
                    map.mapRemake();

                    //プレイヤーモデルを動かす
                    StartCoroutine(playerAnimation());

                    //一回塗ったので行動を一減らす
                    moveCounter--;
                    map.paint = false;
                }
                else
                {
                    Debug.Log("<color=red>隣接したマスがありません。</color>");
                    nowMove = false;
                }
            }

            //行動回数が0になるか
            //ターン終了時ボタンを押したら終了
            if (!nowMove && moveCounter <= 0)
            {
                nowMove = true;
                animator.SetBool("Selection", false);
                myTrun = false;
                //マネージャーにも終了したと返す
                gameManager.trunChange();
                //ターン終了時カーソルを透明
                cursorImage.color = new Color32(255, 0, 30, 0);
                nowMove = false;
            }
            if (!nowMove && Input.GetButtonDown("DS4cross"))
            {
                nowMove = true;
                //再度確認

                animator.SetBool("Selection", false);
                //マネージャーにも終了したと返す
                gameManager.trunChange();
                //行動回数を０にする(バグ対策)
                moveCounter = 0;
                myTrun = false;
                //ターン終了時カーソルを透明にする
                cursorImage.color = new Color32(255, 0, 30, 0);
                nowMove = false;
            }
        }
    }

    /// <summary>
    /// カーソルを動かす処理
    /// </summary>
    /// <returns></returns>
    private IEnumerator cursorMove()
    {
        cursor.transform.position = new Vector3(cursor.transform.position.x + Dx, cursor.transform.position.y + Dy, cursor.transform.position.z);
        nowDirection = new Vector2(nowDirection.x + (int)Dx, nowDirection.y - (int)Dy);

        yield return new WaitForSeconds(0.25f);
        nowMove = false;
    }

    /// <summary>
    /// プレイヤーモデルの移動アニメーション処理
    /// </summary>
    /// <returns></returns>
    private IEnumerator playerAnimation()
    {
        //塗ったマスに移動する
        playerModel.transform.position = new Vector3(cursor.transform.position.x + Dx,
            cursor.transform.position.y + Dy,
            playerModel.transform.position.z);

        yield return null;
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
