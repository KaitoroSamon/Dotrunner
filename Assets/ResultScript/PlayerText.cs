using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerText : MonoBehaviour
{

    [SerializeField] Text text;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Space))
        {
            text.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            text.enabled = true;
        }

    }
}
