using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : State
{
    public Animator animator;
    public override State RunCurrentState()
    {

        //animator.SetBool("IsIdle", false);
        //animator.SetBool("IsChasing", false);
        //animator.SetBool("IsAttacking", false);
        animator.SetBool("isVictory", true);
      //  Debug.Log("WIN");

        return this;
    }

    // Start is called before the first frame update
   
}
