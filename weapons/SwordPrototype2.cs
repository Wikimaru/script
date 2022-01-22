using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordPrototype2 : Weapon
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
    }
}
