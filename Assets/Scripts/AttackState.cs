using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : State
{
    public IdleState idleState;
    public UnitMovement unitMovement;
    public bool isNotInRange;
    public Animator animator;
    public Unit unit;
    public NavMeshAgent agent;
    public ChaseState chaseState;
    // Start is called before the first frame update
    public override State RunCurrentState()
    {
      //  Debug.Log("Entering Attack State");

        
        // Lös så ranged attack fungerar utan Play
        // Något med... vissa karaktärer som är fast i Attack stage när de rör sig?

       

        float dist = Vector3.Distance(unitMovement.currentTarget.position, transform.position); //CALCULATES IF THE UNIT IS IN ATTACK RANGE OF THE ENEMY UNIT
       
        if (!unitMovement.currentTarget.gameObject.activeInHierarchy ) // IF THE OBJECT IS NOT ACTIVE IN THE HIERACHY OR IS DISABLED GO BACK TO IDLE STATE
        {
            animator.SetBool("IsAttacking", false);
           
            animator.StopPlayback();
            return idleState;
        }
      
       
        if(dist < unit.attackRange)
        {if(animator != null)
            {

            animator.SetBool("IsAttacking", true);
           animator.Play("Attack01");
            unitMovement.LookAtTarget();
            }

            return this;
        }
        else
        {
            animator.SetBool("IsAttacking", false);



            return idleState;
        }
       


    }
}
