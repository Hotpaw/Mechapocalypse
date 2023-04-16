using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingAbility : BasicAbility
{
    public float healingAmount = 100;

    public override BasicAbility ActiveAbility(Unit unit)
    {
        List<GameObject> unitObject = new List<GameObject>();

        foreach (GameObject t in GameObject.FindGameObjectsWithTag("PlayerUnit"))
        {
            unitObject.Add(t.gameObject);
        }

        int random = Random.Range(0, unitObject.Count);

        GameObject targetunit = unitObject[random];

        

        targetunit.GetComponent<Unit>().HealHealth(healingAmount * unit.abilityMultiplier);

        

        ProjectileManager PM = FindObjectOfType<ProjectileManager>();
        PM.PlayParticle("HealingAbility", targetunit.transform);

        return this;
    }
}
