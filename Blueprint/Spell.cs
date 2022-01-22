using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{

    public enum SpellType 
    {
        aimable, press, drag
    }
    [SerializeField] protected SpellType type;
    [SerializeField] protected float cooldown;
    [SerializeField] protected GameObject spellpickup;
    protected Player player;
    protected bool isUse = false;

    protected abstract void spell_effect(Vector2 rotation);
    protected void Enable()
    {
        switch (type) 
        {
            case SpellType.aimable:
                ControllerManager.spellbutton.onActivateSkill.AddListener(spell_use);
                break;
            case SpellType.press:
                ControllerManager.spellbutton.onPointerDown.AddListener(isPress);
                ControllerManager.spellbutton.onActivateSkill.AddListener(spell_use);
                break;
            case SpellType.drag:
                ControllerManager.spellbutton.onPointerDown.AddListener(spell_use);
                break;
        }
    }
    protected void Disable()
    {
        switch (type)
        {
            case SpellType.aimable:
                ControllerManager.spellbutton.onActivateSkill.RemoveListener(spell_use);
                break;
            case SpellType.press:
                ControllerManager.spellbutton.onPointerDown.RemoveListener(isPress);
                ControllerManager.spellbutton.onActivateSkill.RemoveListener(spell_use);
                break;
            case SpellType.drag:
                ControllerManager.spellbutton.onPointerDown.RemoveListener(spell_use);
                break;
        }
    }
    protected void isPress(int i)
    {
        ControllerManager.skillCancel.enabled = false;
    }
    protected void spell_use(int i) 
    {
        switch (type) 
        {
            case SpellType.aimable:
                if(player.spellCooldownCounter <= 0&&isUse) 
                {
                    player.spellCooldownCounter = cooldown - (cooldown * (0 / 100));
                    ControllerManager.spellbutton.SetActiveState(false);
                    spell_effect(ControllerManager.spellbutton.direction);
                    Debug.Log("use Spell aimable");
                }

                break;
            case SpellType.press:
                if (player.spellCooldownCounter <= 0&&isUse)
                {
                    player.spellCooldownCounter = cooldown - (cooldown * (0 / 100));
                    ControllerManager.spellbutton.SetActiveState(false);
                    ControllerManager.skillCancel.enabled = true;
                    ControllerManager.skillCancel.isAnyFingerDown = false;
                    spell_effect(ControllerManager.spellbutton.direction);
                    Debug.Log("use Spell press");
                }

                break;
            case SpellType.drag:
                spell_effect(ControllerManager.spellbutton.direction);
                Debug.Log("use Spell Drag");
                break;
        }      
    }
    public void setup(Player player) 
    {
        switch (type)
        {
            case SpellType.aimable:
                ControllerManager.spellbutton.isAimable = true;
                break;
            case SpellType.press:
                ControllerManager.spellbutton.isAimable = false;
                break;
            case SpellType.drag:
                ControllerManager.spellbutton.isAimable = true;
                break;
        }
        Enable();
        isUse = true;
        this.player = player;
    }
    public void Drop(Transform transform) 
    {
        isUse = false;
        Instantiate(spellpickup, transform.position, transform.rotation);
        Disable();
        Destroy(this.gameObject);
    }
}
