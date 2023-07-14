using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountRandom : MonoBehaviour
{
    [SerializeField]
    GameManager turn;
    int rnd = 0;

    public static bool lockFirst = false;
    public static bool lockSecond = false;

    // Start is called before the first frame update
    void Awake()
    {
        if (!tutorialManager.tutorialNow)
        {
            Rand();
        }
    }

    void Rand()
    {
        rnd = Random.Range(0, 2);
        //Debug.Log(rnd)

        if (lockFirst) rnd = 1;
        if (lockSecond) rnd = -1;

        Debug.Log(rnd);

        if (rnd == 1)
        {
            GameManager.player1Trun = true;
            //Debug.Log("1p");

            Map.MakePortion1 = 10;  //‰¡ŽR‰Á•M
            Map.MakePortion2 = 9;  //‰¡ŽR‰Á•M
            Map.MakePortion3 = 12;  //‰¡ŽR‰Á•M
        }

        else
        {
            GameManager.player1Trun = false;
            //Debug.Log("2p");

            Map.MakePortion1 = 9;  //‰¡ŽR‰Á•M
            Map.MakePortion2 = 8;  //‰¡ŽR‰Á•M
            Map.MakePortion3 = 12;  //‰¡ŽR‰Á•M
        }

        Debug.Log(GameManager.player1Trun);
    }
}
