using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeSkill1 : Skill
{
    [SerializeField]private GameObject SkillObject;
    // Start is called before the first frame update
    void Start()
    {
        Enable();
    }

    // Update is called once per frame
    void Update()
    {
        cooldownupdate();
    }

    protected override void skill_effect(Vector2 rotation)
    {
        
    }
}
