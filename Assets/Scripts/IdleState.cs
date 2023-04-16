using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleState : State
{
   public enum Alignment { Player, Enemy};
    public Alignment alignment;
    public ChaseState chaseState;
    public WinState winState;
 
    public Unit unit;
    public bool isInDetectRange;
    public UnitMovement unitMovement;
   public  List<Transform> enemyPool;
    float maxdistance;
    public Animator animator;
    private void Update()
    {
        
    }

    // Start is called before the first frame update
    public override State RunCurrentState()
    {
      //  unitMovement.currentTarget = null;
      //  Debug.Log("Entering Idol State");
        animator.SetBool("IsIdle", true);
       
       
        maxdistance = unit.attackRange;
        //CHANGE DEPENDING ON ENEMY OR PLAYER UNIT
        if(unit.GetComponent<Unit>().Alignment == Unit.UnitAlignment.Player)
        {
            // FIND EACH COLLIDER WITHIN DETECTION DISTANCE
            Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, unit.detectRange);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.CompareTag("EnemyUnit")) //IF THE OBJECT IS OF TAG ENEMYUNIT
                {


                    if (hitCollider.gameObject.activeInHierarchy)
                    {

                        enemyPool.Add(hitCollider.transform); // ADD THE OBJECT TO A LIST OF ENEMIES
                    }

                    isInDetectRange = true; // SINCE AN ENEMY EXISTS WITHIN RANGE, THIS BOOL BECOMES TRUE


                }
                else
                {

                }
            }

        }
        if (unit.GetComponent<Unit>().Alignment == Unit.UnitAlignment.Enemy)
        {
            // FIND EACH COLLIDER WITHIN DETECTION DISTANCE
            Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, unit.detectRange);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.CompareTag("PlayerUnit")) //IF THE OBJECT IS OF TAG ENEMYUNIT
                {

                    if (hitCollider.gameObject.activeInHierarchy) {
                    
                    enemyPool.Add(hitCollider.transform); // ADD THE OBJECT TO A LIST OF ENEMIES
                    }
                    



                    isInDetectRange = true; // SINCE AN ENEMY EXISTS WITHIN RANGE, THIS BOOL BECOMES TRUE


                }
                else
                {

                }
            }

        }



        if (isInDetectRange && enemyPool.Count !> 0)
        {
            animator.SetBool("IsIdle", false);
            unitMovement.getTarget(GetClosestEnemy(enemyPool)); // CHOOSES THE TARGET UNIT THAT IS THE CLOSEST TO THIS UNIT
            enemyPool.Clear(); // EMPTY THE LIST FOR NEXT TIME THIS FUNCTION RUNS WHEN AN ENEMY DIES
            return chaseState; // GO TO CHASE STATE
        }
        else if(enemyPool.Count! > 0)
        {
            return this;
        }
        else
        {
           
            animator.SetBool("IsIdle", false);
            animator.SetBool("IsAttacking", false);
            animator.SetBool("IsChasing", false);
            return winState;
        }




    }
   
    Transform GetClosestEnemy(List<Transform> enemies) // CALCULATES THE CLOSEST ENEMY WITHIN RANGE
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Transform potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        return bestTarget;
    }
}
