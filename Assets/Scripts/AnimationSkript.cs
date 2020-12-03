using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSkript : MonoBehaviour
{
    public Animator animator;
   
       public void leftPunch()
    {
        animator.Play("MainLeft", -1, 0f);
    }
    public void rightPunch()
    {

        animator.Play("MainRightPunch", -1, 0f);
    }
    public void showUzi()
    {
       
        animator.SetBool("uzi", true);
        animator.SetBool("gun", false);
        animator.SetBool("mp", false);
      
        animator.SetBool("dead", false);
    }
    public void showGun()
    {
        
        animator.SetBool("uzi", false);
        animator.SetBool("gun", true);
        animator.SetBool("mp", false);
      
        animator.SetBool("dead", false);
    }

    public void showMp()
    {
        
        animator.SetBool("uzi", false);
        animator.SetBool("gun", false);
        animator.SetBool("mp", true);

        animator.SetBool("dead", false);
    }
    public void showIdle()
    {
        
        animator.SetBool("uzi", false);
        animator.SetBool("gun", false);
        animator.SetBool("mp", false);
        animator.SetBool("dead", false);
    }
    public void showDead()
    {
      
        animator.SetBool("uzi", false);
        animator.SetBool("gun", false);
        animator.SetBool("mp", false);
      
        animator.SetBool("dead", true);
    }
}
