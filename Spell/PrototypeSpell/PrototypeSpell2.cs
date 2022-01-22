using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeSpell2 : Spell
{
    [SerializeField]private GameObject Bullet;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float castRange;
    
    protected override void spell_effect(Vector2 rotation)
    {
        spawnPoint.localPosition = rotation * castRange;
        float angle;
        angle = Mathf.Atan2(rotation.y*castRange, rotation.x*castRange);
        spawnPoint.rotation = Quaternion.Euler(0, 0, angle*Mathf.Rad2Deg);
        Instantiate(Bullet, spawnPoint.position, spawnPoint.rotation);
    }
}
