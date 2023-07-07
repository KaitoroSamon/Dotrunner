using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class VolumeController : MonoBehaviour
{
    float space = 0f;
    public List<TileObj> volumeTiles = new List<TileObj>();
    public enum VOLUME_TYPE
    {
        MASTER,
        BGM,
        SE,
    }

    [SerializeField]
    VOLUME_TYPE volumeType = VOLUME_TYPE.MASTER;


    private enum PHASE
    {
        Play,
        VolumeChange,
    }
    PHASE volumeChange = PHASE.Play;

    void Start()
    {
        for (int tilePos = 0; tilePos < 10; tilePos++)
        {
            TileObj tileObj;
            GameObject volumeTile = (GameObject)Resources.Load("VolumeTile");
            Vector3 objPos = volumeTile.transform.position;
            objPos.x += space;
            space  += 0.5f;
            tileObj = volumeTile.GetComponent<TileObj>();
            tileObj.PositionInt = tilePos + 1;
            volumeTiles.Add(tileObj);
            volumeTile = Instantiate(volumeTile, objPos, Quaternion.identity);
            
            Debug.Log(tileObj);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            volumeChange = PHASE.VolumeChange;
        }
        if(volumeChange == PHASE.VolumeChange && Input.GetMouseButtonDown(0))
        {
            ChangePhase();
        }
    }

    void ChangePhase()
    {

    }


    // Update is called once per frame
    public void VolumeChange()
    {
        switch (volumeType)
        {
            case VOLUME_TYPE.MASTER:
                //BGMManager.Instance.BGMVolumeChange(slider.value);
                break;
            case VOLUME_TYPE.BGM:
                break;
            case VOLUME_TYPE.SE:
                break;
        }
    }

}
