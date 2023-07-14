using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            BGMManager.Instance.Play(BGMManager.BGM_TYPE.STAGESELECT);
        }
    }
}
