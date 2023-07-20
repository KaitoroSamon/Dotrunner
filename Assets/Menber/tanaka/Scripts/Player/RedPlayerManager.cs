﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedPlayerManager : MonoBehaviour
{
    Animator animator;
    public static Map map;
    public static GameManager gameManager;
    [Header("MapがついているGameObject")]
    [SerializeField]
    GameObject mapScrits;
    [Header("GameManagerがついているGameObject")]
    [SerializeField]
    GameObject gameManagerScripts;
    [Header("自分の子オブジェクトのスキン")]
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
    private Vector2 nowDirection = default;

    //コントローラーからのキー受け取り用
    float Dx = default;
    float Dy = default;
    bool nowMove = false;

    [Header("\n自分の孫オブジェクトのCursor")]
    [SerializeField]
    GameObject cursor;
    Image cursorImage;

    GameObject playerModel;

    [Header("\nスキン４方向")]
    [SerializeField]
    List<Sprite> sprites = new List<Sprite>();

    void Start()
    {
        map = mapScrits.GetComponent<Map>();
        gameManager = gameManagerScripts.GetComponent<GameManager>();
        cursorImage = cursor.GetComponent<Image>();
        playerModel = transform.Find("PlayerModel").gameObject;
        animator = cursor.GetComponent<Animator>();
        //スキン設定
        //カーソルを透明にする
        cursorImage.color = new Color32(0, 0, 0, 0);
        
        myTrun = false;
        nowPos = default;
        moveCounter = default;
        rePaint = default;
        nowMove = false;
        
    }

    //プレイヤーの相対移動度(一括移動は後で)
    void Update()
    {
        if (!GameManager.stopInputKey)
        {
            //自ターンのみ動かす
            if (myTrun)
            {

                if (tutorialManager.tutorialNow && !tutorialManager.launch01)
                {
                    tutorialManager.launch01 = true;
                }

                cursorImage.color = new Color32(255, 0, 0, 255);
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
                    if (tutorialManager.tutorialNow && !tutorialManager.launch03 && gameManager.redRePaint > 0 && tutorialManager.close02)
                    {
                        tutorialManager.launch03 = true;
                    }

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
                    //ターン終了時カーソルを透明にする
                    cursorImage.color = new Color32(0, 0, 0, 0);
                    nowMove = false;
                }
                if (!tutorialManager.tutorialNow && !nowMove && Input.GetButtonDown("DS4cross"))
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
                    cursorImage.color = new Color32(0, 0, 0, 0);
                    nowMove = false;
                }
            }
        }
        if (gameManager.redHp <= 0)
        {
            //Debug.Log("<color=cyan> Red Knock Out </color>");
            GameManager.nextScene = "blueWin";
            gameManager.sceneLoadtime();
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
        var Xpos = Mathf.Clamp((int)cursor.transform.localPosition.x, (int)0, (int)15);
        var Ypos = Mathf.Clamp((int)cursor.transform.localPosition.y, (int)-4, (int)4);
        Debug.Log(Xpos + " : " + Ypos);
        cursor.transform.localPosition = new Vector3(Xpos, Ypos, cursor.transform.position.z);
        //x -7.5～7.5 y -3.5～4.5
        var Xdir = Mathf.Clamp(nowDirection.x, -7.5f, 7.5f);
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


        //修正案件
        if (playerModel.transform.position == opponentPlayerPos.transform.position)
        {
            GameManager.isRedAttack = true;
            gameManager.blueHp--;
            gameManager.subMoveCounter();

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
