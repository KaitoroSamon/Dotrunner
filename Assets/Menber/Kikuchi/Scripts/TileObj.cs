using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObj : MonoBehaviour
{
    [SerializeField] GameObject nowVolume;
    public int PositionInt = 0;

    public void ShowMovablePanel(bool isActive)
    {
        nowVolume.SetActive(isActive);
    }
}
