using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeSpell1 : Spell
{
    [SerializeField]private GameObject spellBomb;
    protected override void spell_effect(Vector2 rotation)
    {
        Instantiate(spellBomb, this.transform.position, this.transform.rotation);
    }
}
