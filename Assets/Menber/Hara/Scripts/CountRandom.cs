using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountRandom : MonoBehaviour
{
    [SerializeField]
    GameManager turn;
    int rnd = 0;
    
    // Start is called before the first frame update
    void Start()
    {
         rnd = Random.Range(0,2);
        Debug.Log(rnd);
        if (rnd == 1)
        {
            GameManager.player1Trun = true;
            Debug.Log("1p");
        }
       
        else
        {
            GameManager.player1Trun = false;
            Debug.Log("2p");
        }
    }

    // Update is called once per frame
    void Update()
    {
        

    }
}
