using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
     [SerializeField] TextAsset mapText; //マップの情報を取得

    int selectPlayer; //どちらが攻撃しているかを保存しておくフィールド

    GameObject playerObj1;
    GameObject playerObj2;

    // Start is called before the first frame update
    void Start()
    {
        playerObj1 = GameObject.Find("Square");
        playerObj2 = GameObject.Find("Square (1)");
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetKeyDown(KeyCode.Space))
        {
            if (selectPlayer == 0)
            {
                // プレイヤー１の攻撃
                playerObj1.GetComponent<Player1>().Attack();//プレイヤー1の行動を呼び出す
                selectPlayer = 1;
            }
            else
            {
                // プレイヤー２の攻撃
                playerObj2.GetComponent<Player2>().Attack();
                selectPlayer = 0;
            }
        }
    }
}
