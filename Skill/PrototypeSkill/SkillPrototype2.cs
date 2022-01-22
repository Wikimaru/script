using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPrototype2 : Skill
{
    [SerializeField] private GameObject Bullet;
    [SerializeField] private Transform aimPoint;
    [SerializeField] private float castRange;
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
        aimPoint.localPosition = rotation * castRange;
        float angle;
        angle = Mathf.Atan2(rotation.y * castRange, rotation.x * castRange);
        aimPoint.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);
        Instantiate(Bullet, aimPoint.position, aimPoint.rotation);
    }
}
