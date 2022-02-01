using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Damageable
{
    void TakeDamage(float damage,float bleed,GameManager.DamageType type);
}
