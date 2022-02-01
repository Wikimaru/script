using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] protected Animator weaponAnimator;
    [SerializeField] protected SpriteRenderer weaponSprite;
    [SerializeField] protected GameObject weaponPickup;
    [SerializeField] protected float baseAttackTime,damage,sharpness;
    [SerializeField] protected float attackArea;
    [SerializeField] protected GameManager.DamageType Damagetype;
    [SerializeField] protected Transform attackPos;
    [SerializeField] protected LayerMask whatIsEnemies;
    protected float attackcounter;

    [SerializeField] protected bool isEquip;
    public abstract void Attack(float attackSpeed);

    protected float attackSpeedCalulator(float attackSpeed,float baseAttack) 
    {
        float attackRate;
        attackRate = 1/(attackSpeed / (baseAttack * 100));
        return attackRate;
    }
    protected virtual void attackCooldownUpdate() 
    {
        if (attackcounter >=0) 
        {
            attackcounter -= Time.deltaTime;
        }
    }

    public void equip(bool isEquip) 
    {
        this.isEquip = isEquip;
        weaponSprite.enabled = isEquip;
    }

    public void dropWeapon(Transform transform) 
    {
        Instantiate(weaponPickup,transform.position,transform.rotation);
        Destroy(this.gameObject);
    }
}
