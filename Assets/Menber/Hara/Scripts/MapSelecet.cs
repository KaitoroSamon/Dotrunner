using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelecet : MonoBehaviour
{
    [SerializeField]
    StartSetting set;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Onclick()
    {
        set.test = "/StreamingAssets/csv/BaseMap.csv";
    }

}
