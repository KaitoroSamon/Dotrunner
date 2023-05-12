using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AnimationList;

public class AnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    AnimationList animationList;
    void Start()
    {
        animationList = this.gameObject.GetComponent<AnimationList>();
        StartCoroutine("AnimTest");
    }

    private void Update()
    {
        if (animationList.anim_type == ANIMATION_TYPE.SELECTING)
        {
            animator.SetBool("Selection", true);
        }
        else if (animationList.anim_type == ANIMATION_TYPE.BEFORESELECT
             || animationList.anim_type == ANIMATION_TYPE.SELECTING)
        {
            animator.SetBool("Selection", false);
        }
    }

    public IEnumerator AnimTest()
    {
        yield return new WaitForSeconds(5);
        animationList.anim_type = ANIMATION_TYPE.SELECTING;
    }
}
