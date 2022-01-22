using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public enum SkillType
    {
        aimable,press,drag
    }
    [SerializeField]protected SkillType type;
    [SerializeField] private int buttonNum;
    [SerializeField]protected float skillCooldown;
    private float skillCooldownCounter;

    private void skill_use(int i)
    {
        switch (type) 
        {
            case SkillType.aimable:
                if (skillCooldownCounter <= 0)
                {
                    skillCooldownCounter = skillCooldown - (skillCooldown * (0 / 100));
                    ControllerManager.instance.skillbutton[buttonNum].SetActiveState(false);
                    skill_effect(ControllerManager.instance.skillbutton[buttonNum].direction);
                    Debug.Log("useSkillaimable Button =" + buttonNum);
                }
                break;
            case SkillType.press:
                if (skillCooldownCounter <= 0)
                {
                    skillCooldownCounter = skillCooldown - (skillCooldown * (0 / 100));
                    ControllerManager.instance.skillbutton[buttonNum].SetActiveState(false);
                    ControllerManager.skillCancel.enabled = true;
                    ControllerManager.skillCancel.isAnyFingerDown = false;
                    skill_effect(ControllerManager.instance.skillbutton[buttonNum].direction);
                    Debug.Log("useSkillPress Button =" + buttonNum);
                }
                break;
        }
    }

    protected abstract void skill_effect(Vector2 rotation); 

    protected void Enable() 
    {
        switch (type) 
        {
            case SkillType.aimable:
                ControllerManager.instance.skillbutton[buttonNum].onCancelSkill.AddListener(cancelSkill);
                ControllerManager.instance.skillbutton[buttonNum].onActivateSkill.AddListener(skill_use);
                break;
            case SkillType.press:
                ControllerManager.instance.skillbutton[buttonNum].onPointerDown.AddListener(isPress);
                ControllerManager.instance.skillbutton[buttonNum].onActivateSkill.AddListener(skill_use);
                break;
            case SkillType.drag:
                ControllerManager.instance.skillbutton[buttonNum].onPointerDown.AddListener(skill_use);
                break;
        }
    }
    protected void Disable() 
    {
        switch (type)
        {
            case SkillType.aimable:
                ControllerManager.instance.skillbutton[buttonNum].onCancelSkill.RemoveListener(cancelSkill);
                ControllerManager.instance.skillbutton[buttonNum].onActivateSkill.RemoveListener(skill_use);
                break;
            case SkillType.press:
                ControllerManager.instance.skillbutton[buttonNum].onPointerDown.RemoveListener(isPress);
                ControllerManager.instance.skillbutton[buttonNum].onActivateSkill.RemoveListener(skill_use);
                break;
            case SkillType.drag:
                ControllerManager.instance.skillbutton[buttonNum].onPointerDown.RemoveListener(skill_use);
                break;
        }
        Enable();
    }
    
    protected void cancelSkill(int i) 
    {

    }
    protected void isPress(int i) 
    {
        ControllerManager.skillCancel.enabled = false;
    }
    protected void cooldownupdate() 
    {
        if(skillCooldownCounter > 0f) 
        {
            skillCooldownCounter -= Time.deltaTime;
            ControllerManager.instance.skillbutton[buttonNum].SetText(skillCooldownCounter.ToString("F1"));
            if (skillCooldownCounter <= 0) 
            {
                ControllerManager.instance.skillbutton[buttonNum].SetActiveState(true);
                ControllerManager.instance.skillbutton[buttonNum].SetText(" ");
            }
        }
    }

    public void setup(int skillnum) 
    {
        buttonNum = skillnum;
        switch (type) 
        {
            case SkillType.aimable:
                ControllerManager.instance.skillbutton[buttonNum].isAimable = true;
                break;
            case SkillType.press:
                ControllerManager.instance.skillbutton[buttonNum].isAimable = false;
                break;
            case SkillType.drag:
                ControllerManager.instance.skillbutton[buttonNum].isAimable = true;
                break;
        }

    }
}
