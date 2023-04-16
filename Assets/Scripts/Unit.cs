using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : UnitMovement
{
    //UI
    public Slider healthSlider;

    //Animation
    public Animator anim;


    //Unit Alignment player units will attack enemies, remember to change their tag to the corresponding enum.
    public enum UnitAlignment { Player, Enemy};
    public UnitAlignment Alignment;
    //Unit Attack type, if ranged or melee
    public enum UnitAttackType { Melee, Ranged };
    public UnitAttackType unitAttackType;
    //Unit Damage Type
    public enum UnitTypeDamage { Physical, Magical};
    public UnitTypeDamage unitTypeDamage;
    //The tribe the unit belongs to
    public enum UnitTribe { Gorehammer, Cackling_Coalition, Coven_of_the_unseen, Firebelly_Tribe, Wild_Beasts};
    public UnitTribe unitTribe;
    //The Class of the unit
    public enum UnitClass { Guardian, Warrior, Ranger, Mage , Shaman, Rogue};
    public UnitClass unitClass;

    //STATS
    public float maxHealth;
    public float currentHealth;

    public float maxmana;
    public float currentMana;
    public float manaRegen;
    public float manaRegenCooldown;
    float manaTimer;
    public float baseDamage;
    public float abilityDamage;
    public float abilityMultiplier;

    public float armor;
    public float resistance;
    [Tooltip("Create Parameter Speed in Attack animation and set speed multiplier to Speed of animation")]
    public float timeBetweenAttacks;
    public float detectRange;
    public float attackRange;
    public float critChance;
    public float critMultiplier;

    public float moveSpeed;
    public float maxSpeed;


    //Ranged unit Particles
    public GameObject rangedProjectile;
    public GameObject attacPoint;

    public BasicAbility basicAbility;
    public bool hasActiveAbility = false;
    private void Start()
    {
    
    StartUp();
        
    }
    public void Update()
    {
        // Changes attack speed at runtime of character
        //Remember to add Paramter speed in attack animation, and set animation clip speed multiplier to that parameter on each unit.
        anim.SetFloat("Speed", timeBetweenAttacks);
    }
    public void FixedUpdate()
    {
        ManaRegenration();
    }
    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, detectRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    public void ManaRegenration()
    {
      
        manaTimer += Time.deltaTime;
        if(manaTimer >= manaRegenCooldown)
        {
            currentMana += maxmana;
            manaTimer = 0;
        }

        if (currentMana == maxmana && hasActiveAbility == true)
        {
            useAbility(basicAbility);
            currentMana = 0;
        }
    }
    public void StartUp()
    {
        navmeshAgent.stoppingDistance = attackRange - 0.2f;
        healthSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.green;
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
        moveSpeed = maxSpeed;
        currentMana = 0;
    

    }
    public void useAbility(BasicAbility basicAbility)
    {
        basicAbility.ActiveAbility(this);
    }
    public void HealHealth(float amount)
    {
        currentHealth += amount;
        HealthSliderValue();
    }
    public void TakeDamage(float amount, UnitTypeDamage damageType)
    {
        Debug.Log("Total Damage Before reduction: " + amount);

        currentMana += manaRegen;
        if(damageType == UnitTypeDamage.Physical)
        {
        currentHealth -= physicalDamageCalculation(amount);
        }
        else if(damageType == UnitTypeDamage.Magical)
        {
            currentHealth -= magicalDamageCalculation(amount);
        }

        HealthSliderValue();
        if(currentHealth < 0)
        {
            gameObject.SetActive(false);
        }
    }
   public void HealthSliderValue()
    {
       healthSlider.value = currentHealth;
        if(healthSlider.value < maxHealth / 2)
        {
            healthSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.yellow;
        }
        if (healthSlider.value < maxHealth / 4)
        {
            healthSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = Color.yellow;
        }
    }

    public float physicalDamageCalculation(float amount) // Calculate the reduction of physical damage against armor
    {
        float damageCalculation = amount / (amount + armor);

        float totalDamage = amount * damageCalculation;

        Debug.Log("Total Physical Damage After reduction: " + totalDamage);
        return totalDamage;
    }
    public float magicalDamageCalculation(float amount) // Calculate the reduction of physical damage against armor
    {
        float damageCalculation = amount / (amount + resistance);

        float totalDamage = amount * damageCalculation;

        Debug.Log("Total Physical Damage After reduction: " + totalDamage);
        return totalDamage;
    }
    public float DamageDone(float amount)
    {
         
         float critcheck = Random.Range(1, 100);
        float damageCalculation;

        if(critcheck < critChance)
        {
            Debug.Log("Crit");
            damageCalculation = amount * critMultiplier;
            
        }
        else
        {
            damageCalculation = amount;
        }
       
            return damageCalculation;
        
    }
    public void IncreaseAttackSpeed(float amount)
    {
        timeBetweenAttacks += amount;
        anim.SetFloat("Speed", timeBetweenAttacks);
    }
}
