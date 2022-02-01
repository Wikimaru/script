using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordPrototype : Weapon
{
    [SerializeField] private ParticleSystem AttackPartical1;
    [SerializeField] private ParticleSystem AttackPartical2;
    private bool attacknum = false;

    private void FixedUpdate()
    {
        attackCooldownUpdate();
    }
    public override void Attack(float attackSpeed)
    {
        if (attackcounter <= 0)
        {
            weaponAnimator.SetTrigger("Attack");
            attackcounter = attackSpeedCalulator(attackSpeed, baseAttackTime);
        }
    }

    public void playPartical()
    {
        attacknum = !attacknum;
        if (attacknum)
        {
            AttackPartical1.Play();
        }
        else
        {
            AttackPartical2.Play();
        }
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackArea, whatIsEnemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {

            enemiesToDamage[i].GetComponent<Damageable>().TakeDamage(damage,sharpness,Damagetype);
            //if (enemiesToDamage[i].GetComponent<EnemyState>().currentHP > 0)
            //{
                
            //}
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackArea);
    }
}
