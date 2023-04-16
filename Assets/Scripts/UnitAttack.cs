using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAttack : MonoBehaviour
{
    public Unit unit;
    public UnitMovement unitMovement;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Attack()
    {
       
          
            Unit enemy = unitMovement.currentTarget.gameObject.GetComponent<Unit>();
                enemy.TakeDamage(unit.baseDamage, unit.unitTypeDamage);
      
        
    }
}
