using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class feetConroll : MonoBehaviour
{
    public Animator animator;
   
    // Update is called once per frame
    void FixedUpdate()
    {
        lookatDir();
        animator.SetBool("walking", GetComponentInParent<PlayerMovement>().isWalking);
    }

    private void lookatDir()
    {
     
       
        
        Vector2 direction = new Vector2(GetComponentInParent<PlayerMovement>().Horizontal, GetComponentInParent<PlayerMovement>().Vertical);
        // set vector of transform directly
        transform.up = direction;
    }


}
