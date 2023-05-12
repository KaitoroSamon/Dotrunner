using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimationList : MonoBehaviour
{
    public enum ANIMATION_TYPE
    {
        BEFORESELECT,
        SELECTING,
        SELECTED,
    }

    public ANIMATION_TYPE anim_type;
    void Start()
    {
        anim_type = ANIMATION_TYPE.BEFORESELECT;
    }

}

