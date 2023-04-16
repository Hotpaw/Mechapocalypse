using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public List<ParticleSystem> prefabParticles;
   public List<ParticleSystem> inGameParticleSystems;
    public GameObject[] AbilityProjectiles;
    public Transform particleList;
    // Start is called before the first frame update
    void Start()
    {
        foreach(ParticleSystem particle in prefabParticles)
        {
            inGameParticleSystems.Add(particle);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayParticle(string AbilityName, Transform target)
    {

       

        switch (AbilityName)
        {
            case "HealingAbility":
                inGameParticleSystems[0].transform.position = target.position;  inGameParticleSystems[0].Play(); 
                break;
            case "ForceSphere":
                inGameParticleSystems[0].transform.position = target.position; inGameParticleSystems[0].Play();
                break;


        }
        
       

    }
}
