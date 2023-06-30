using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class tutorialManager : MonoBehaviour
{
    public static bool tutorialNow = true;

    public static bool launch01 = false;
    private bool close01 = false;
    public static bool launch02 = false;
    private bool close02 = false;
    public static bool launch03 = false;
    private bool close03 = false;
    public static bool launch04 = false;
    private bool close04 = false;
    public static bool launch05 = false;
    private bool close05 = false;
    public static bool launch06 = false;
    private bool close06 = false;


    [SerializeField]
    GameObject tutorial01;
    [SerializeField]
    GameObject tutorial02;
    [SerializeField]
    GameObject tutorial03;
    [SerializeField]
    GameObject tutorial04;
    [SerializeField]
    GameObject tutorial05;
    [SerializeField]
    GameObject tutorial06;

    void Update()
    {
        if (!tutorialNow)
        {
            this.gameObject.SetActive(false);
        }
        if (launch01 && !close01)
        {
            Time.timeScale = 0;
            tutorial01.gameObject.SetActive(true);
            if (UnityEngine.Input.GetButtonDown("DS4cross"))
            {
                close01 = true;
                tutorial01.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
        }
        if (launch02 && !close02)
        {
            Time.timeScale = 0;
            tutorial02.gameObject.SetActive(true);
            if (UnityEngine.Input.GetButtonDown("DS4cross"))
            {
                close02 = true;
                tutorial02.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
        }
        if (launch03 && !close03)
        {
            Time.timeScale = 0;
            tutorial03.gameObject.SetActive(true);
            if (UnityEngine.Input.GetButtonDown("DS4cross"))
            {
                close03 = true;
                tutorial03.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
        }
        if (launch04 && !close04)
        {
            Time.timeScale = 0;
            tutorial04.gameObject.SetActive(true);
            if (UnityEngine.Input.GetButtonDown("DS4cross"))
            {
                close04 = true;
                tutorial04.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
        }
        if (launch05 && !close05)
        {
            Time.timeScale = 0;
            tutorial05.gameObject.SetActive(true);
            if (UnityEngine.Input.GetButtonDown("DS4cross"))
            {
                close05 = true;
                tutorial05.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
        }
        if (launch06 && !close06)
        {
            Time.timeScale = 0;
            tutorial06.gameObject.SetActive(true);
            if (UnityEngine.Input.GetButtonDown("DS4cross"))
            {
                close06 = true;
                tutorial06.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }
}
