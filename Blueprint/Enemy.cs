using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour,Damageable
{
    [SerializeReference] protected float currentHP, currentblood, maxHp,currentArmor, currentResistance;
    [SerializeField] protected float baseHP, baseResistance, baseArmor, baseSpeed,baseAttackSpeed;


    [SerializeField] protected LayerMask whatIsTarget;
    [SerializeField] protected Rigidbody2D rB;


    protected void setup() 
    {
        rB = this.gameObject.GetComponent<Rigidbody2D>();
        this.gameObject.layer = 7;
        currentblood = baseHP;
        currentHP = baseHP;
        maxHp = baseHP;
        currentArmor = baseArmor;
        currentResistance = baseResistance;
    }
    protected float physicalDamage(float armor, float damage)
    {
        float damageMult;
        armor = armor / 2;
        damageMult = 1f - ((0.052f * armor) / (0.9f + (0.048f * armor)));
        return damageMult;
    }
    protected float magicalDamage(float magicalResistance, float damage)
    {
        float damageMult;
        if (magicalResistance > 90)
        {
            magicalResistance = 90;
        }
        damageMult = 1f - (magicalResistance / 100);
        return damageMult;
    }

    public void TakeDamage(float damage, float bleed, GameManager.DamageType type) 
    {
        switch (type)
        {
            case GameManager.DamageType.physical:
                currentHP -= damage * physicalDamage(currentArmor, damage);
                currentblood -= damage * physicalDamage(currentArmor, damage) * (bleed / 100);
                Debug.Log("Damage" + damage * physicalDamage(currentArmor, damage));
                break;
            case GameManager.DamageType.magical:
                currentHP -= damage * magicalDamage(currentResistance, damage);
                currentblood -= damage * magicalDamage(currentArmor, damage) * (bleed / 100);
                Debug.Log("Damage" + damage * magicalDamage(currentArmor, damage));
                break;
        }
    }

}
