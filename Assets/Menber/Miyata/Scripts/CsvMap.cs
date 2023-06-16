using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CsvMap : MonoBehaviour
{
    public TextAsset csvFile;
    List<string[]> csvDatas = new List<string[]>();

    string str = "";
    // Start is called before the first frame update
    void Start()
    {
        csvFile = Resources.Load("ExcelDate2") as TextAsset;
        StringReader reader = new StringReader(csvFile.text);

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            csvDatas.Add(line.Split(','));
        }
	    Debug.Log("a");
    }	
    // Update is called once per frame
    void Update()
    {
        
    }
}
