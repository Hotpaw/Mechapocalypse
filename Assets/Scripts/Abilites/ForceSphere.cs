using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceSphere : BasicAbility
{
    public override BasicAbility ActiveAbility(Unit unit)
    {
        ProjectileManager PM = FindObjectOfType<ProjectileManager>();
        Instantiate(PM.AbilityProjectiles[0]);

        PM.AbilityProjectiles[0].GetComponent<ForceSphereProjectile>();

        GameObject projectile = Instantiate(PM.AbilityProjectiles[0], unit.attacPoint.transform.position, transform.rotation);

        projectile.GetComponent<RangedProjectile>().target = unit.currentTarget;
        projectile.GetComponent<RangedProjectile>().unit = unit;
        projectile.GetComponent<RangedProjectile>().baseDamage = unit.abilityDamage * unit.abilityMultiplier;

        return this;
    }
}
