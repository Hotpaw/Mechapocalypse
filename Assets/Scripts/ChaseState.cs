using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    public AttackState attackState;
    public UnitMovement unitMovement;
   public  IdleState idleState;
    public Unit unit;
    public Animator animator;
    
    
    public bool isInAttackRange;
    public override State RunCurrentState()
    {
      //  Debug.Log("Entering chase State");
        unit.moveSpeed = unit.maxSpeed;
        animator.SetBool("IsChasing",true);

      
        float dist = Vector3.Distance(unitMovement.currentTarget.position, transform.position); //CALCULATES IF THE UNIT IS IN ATTACK RANGE OF THE ENEMY UNIT


        if (!unitMovement.currentTarget.gameObject.activeInHierarchy) // IF THE OBJECT IS NOT ACTIVE IN THE HIERACHY OR IS DISABLED GO BACK TO IDLE STATE
        {
            animator.SetBool("IsChasing", false);
            return idleState;
        }
        if(dist < unit.attackRange)  // IF IT IS WITHIN ATTACKRANGE SET THE BOOL TO TRUE
        {
            animator.SetBool("IsChasing", false);

            return attackState; // MOVE ON TO ATTACK STATE
        }
        else
        {
            unit.navmeshAgent.SetDestination(unitMovement.currentTarget.position);
           gameObject.transform.LookAt(unitMovement.currentTarget.position);

            return this;
        }
       
     
       
        
        

    }
}
