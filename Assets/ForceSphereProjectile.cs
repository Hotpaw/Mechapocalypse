using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceSphereProjectile : MonoBehaviour
{
    public Unit unit;
    public Transform target;
    public float baseDamage;
    public float speed;

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
        }
        // Debug.LogFormat("Target name: {0}", target);

        if (target.gameObject.activeInHierarchy)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            transform.LookAt(target.position);

        }
        else
        {
            Destroy(gameObject);
        }



    }
    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject == target.gameObject)
        {
            Debug.Log("hit target:" + target.name);
            other.gameObject.GetComponent<Unit>().TakeDamage(unit.DamageDone(unit.baseDamage), unit.unitTypeDamage);
            DestroyParticle();
        }
    }
    public void DestroyParticle()
    {
        ProjectileManager PM = FindObjectOfType<ProjectileManager>();
        PM.PlayParticle("ForceSphere", target);

        Destroy(gameObject);
    }
}
