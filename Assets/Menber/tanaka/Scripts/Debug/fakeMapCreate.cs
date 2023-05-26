using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*２-3*3のダミーマップの上にプレイヤーを載せる
 * 
 */

public class fakeMapCreate : MonoBehaviour
{
    /*仮データ数値
    * 0 - null
    * 1 - red
    * 2 - blue
    * 3 - redgoal
    * 4 - bluegoal
    * 5 - potion
    * 6 - bucket
    * 7 - bomb
    * 8 - players
    */
    public int[,] map = new int[3,3];
    private Vector2 goalPos = new Vector2(1, 1);

    public Vector2 player1 = new Vector2();

    void Awake()
    {
        //マップ初期化
        for (int i = 0; i < 3; i++)
        {
            for(int j = 0; i < 3; j++)
            {
                map[i,j] = 0;
                if (i == goalPos.x && j == goalPos.y)
                {
                    map[i, j] = 3;
                }
            }
        }
        //プレイヤー初期化
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; i < 3; j++)
            {
                if (map[i,j] == 3)
                {
                    player1 = new Vector2(i,j);
                }
            }
        }

    }

    void Update()
    {
        
    }


}
