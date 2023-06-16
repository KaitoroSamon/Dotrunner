using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BluePlayerManager : MonoBehaviour
{
    Animator animator;
    public static Map map;
    public static GameManager gameManager;
    [Header("MapがついているGameObject")]
    [SerializeField]
    GameObject mapScrits;
    [Header("\nGameManagerがついているGameObject")]
    [SerializeField]
    GameObject gameManagerScripts;
    [Header("\n自分の子オブジェクトのスキン")]
    [SerializeField]
    GameObject opponentPlayerPos;

    //自分のターンかどうか？(マネージャーから受け取り)
    private bool myTrun = false;

    //今の自分の座標(マネージャーから受け取り)
    private Vector2 nowPos = default;

    //移動できる回数
    public int moveCounter = default;

    //塗り替えせる回数
    private int rePaint = default;

    //選択中の方向
    public Vector2 nowDirection = default;

    //コントローラーからのキー受け取り用
    float Dx = default;
    float Dy = default;
    bool nowMove = false;

    [Header("\n自分の孫オブジェクトのCursor")]
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
        //初期座標はマップから受け取り
    }

    void Start()
    {
        animator = cursor.GetComponent<Animator>();

        //スキン設定
    }

    //プレイヤーの相対移動度(一括移動は後で)
    void Update()
    {
        if (!gameManager.stopInputKey)
        {
            //自ターンのみ動かす
            if (myTrun)
            {
                cursorImage.color = new Color32(30, 0, 255, 138);
                animator.SetBool("Selection", true);

                if (!nowMove)
                {
                    Dx = (int)Input.GetAxis("DpadX2");
                    Dy = (int)Input.GetAxis("DpadY2");

                    //カーソルを移動
                    if (Dx != 0 || Dy != 0)
                    {
                        nowMove = true;
                        StartCoroutine(cursorMove());
                    }
                }

                //塗り
                if (!nowMove && Input.GetButtonDown("DS4circle2"))
                {
                    nowMove = true;
                    StartCoroutine(map.paintBlueMap(nowDirection));
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
                    //ターン終了時カーソルを透明感
                    cursorImage.color = new Color32(30, 0, 255, 0);
                    nowMove = false;
                }
                if (!nowMove && Input.GetButtonDown("DS4cross2"))
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
                    cursorImage.color = new Color32(30, 0, 255, 0);
                    nowMove = false;
                }
            }
        }
        if (gameManager.blueHp <= 0)
        {
            //Debug.Log("<color=orange> Blue Knock Out </color>");
            SceneManager.LoadScene("redWin");
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

        //x[-15～0]の範囲にy[-4～4]の範囲に制限する
        var Xpos = Mathf.Clamp((int)cursor.transform.localPosition.x, (int)-15, (int)0);
        var Ypos = Mathf.Clamp((int)cursor.transform.localPosition.y, (int)-4, (int)4);
        Debug.Log(Xpos +" : "+ Ypos);
        cursor.transform.localPosition = new Vector3(Xpos, Ypos, cursor.transform.position.z);
        //x -7.5～7.5 y -3.5～4.5
        var Xdir = Mathf.Clamp(nowDirection.x,-7.5f,7.5f);
        var Ydir = Mathf.Clamp(nowDirection.y, -4.5f, 3.5f);
        nowDirection = new Vector2(Xdir, Ydir);

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

        if (playerModel.transform.position == opponentPlayerPos.transform.position)
        {
            gameManager.redHp--;
        }

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
