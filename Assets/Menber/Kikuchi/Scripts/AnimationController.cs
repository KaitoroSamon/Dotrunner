using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{

    public Animator[] animator = new Animator[0];

    AnimationList animationList;
    void Start()
    {
        animationList = this.gameObject.GetComponent<AnimationList>();
        //StartCoroutine("AnimTest");//アニメーションの変更がちゃんとできるか確認用のコルーチン
    }

    //public void AnimControll(AnimationList.CURSORANIMATION_TYPE cursorAnim_type, AnimationList.PLAYERANIMATION_TYPE playerAnim_type)
 public void AnimControll(AnimationList.CURSORANIMATION_TYPE cursorAnim_type)
    {
        animationList.cursorAnim_type = cursorAnim_type;
        //animationList.playerAnim_type = playerAnim_type;

        if (animationList.cursorAnim_type == AnimationList.CURSORANIMATION_TYPE.SELECTING)
        {
            animator[0].SetBool("Selection", true);
        }
        else if (animationList.cursorAnim_type == AnimationList.CURSORANIMATION_TYPE.STILLNESS
             || animationList.cursorAnim_type == AnimationList.CURSORANIMATION_TYPE.SELECTED)
        {
            animator[0].SetBool("Selection", false);
        }

    //    if (animationList.playerAnim_type == AnimationList.PLAYERANIMATION_TYPE.AWAIT)
    //    {
    //        Debug.Log("Await");
    //        //animator[値].SetBool("", );
    //    }
    //    else if (animationList.playerAnim_type == AnimationList.PLAYERANIMATION_TYPE.WALK)
    //    {
    //        Debug.Log("Walk");
    //    }
    }

    //public IEnumerator AnimTest()
    //{
    //    yield return new WaitForSeconds(1);
    //    AnimControll(AnimationList.CURSORANIMATION_TYPE.SELECTING,animationList.playerAnim_type);
    //    yield return new WaitForSeconds(5);
    //    AnimControll(AnimationList.CURSORANIMATION_TYPE.SELECTED, AnimationList.PLAYERANIMATION_TYPE.WALK);
    //}
}
