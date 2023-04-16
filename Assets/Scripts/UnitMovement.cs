using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    public Transform currentTarget;
   public NavMeshAgent navmeshAgent;
   public Unit unit;
   
    // Start is called before the first frame update

    private void Start()
    {

       
       
       
    }
    // Update is called once per frame
    void Update()
    {
        
        navmeshAgent.speed = unit.moveSpeed;
    

    }
    public void getTarget(Transform target)
    {
        
        navmeshAgent.ResetPath();
        currentTarget = target;
        navmeshAgent.SetDestination(currentTarget.position);
       // navmeshAgent.destination = currentTarget.position;
        
        transform.LookAt(currentTarget.position);
       
    }
    public void LookAtTarget()
    {
        transform.LookAt(currentTarget.position);
    }
    
    public void Attack()
    { //If you want a ranged unit, changed its Unitattacktype to Ranged in the inspector.
        // Don't forgetto create an attackpoint empty gameobject and attatch to where you want the projectile to spawn.
        //Do this in the Units Unit script.
        if(unit.unitAttackType == Unit.UnitAttackType.Ranged)
        {
            GameObject projectile = Instantiate(unit.rangedProjectile, unit.attacPoint.transform.position, transform.rotation);

            projectile.GetComponent<RangedProjectile>().target = currentTarget;
            projectile.GetComponent<RangedProjectile>().unit = unit;
            projectile.GetComponent<RangedProjectile>().baseDamage = unit.baseDamage;

        }
        else
        {
                if(currentTarget != null)
                    {

                    Unit enemy = currentTarget.gameObject.GetComponent<Unit>();
                    enemy.TakeDamage(unit.DamageDone(unit.baseDamage), unit.unitTypeDamage);

                    }

        }




    }
}
