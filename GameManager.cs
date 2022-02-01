using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum CheckType 
    {
        player
    }
    public enum DamageType 
    {
        physical, magical
    }
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }

    public Player player;
    public GameObject playerGameObject;

    [SerializeField]private Slider healthUI, bloodUI;
    [SerializeField]private Slider manaUI,soulUI;

    public bool Check(CheckType type) 
    {
        switch (type)
        {
            case CheckType.player:
                if (player != null && playerGameObject != null)
                {
                    return true;
                }
                break;
        }
        return false;
    }
    public void Update_health(float currentHP,float currentBlood,float maxHP) 
    {
        healthUI.maxValue = maxHP;
        healthUI.value = currentHP;
        bloodUI.maxValue = maxHP;
        bloodUI.value = currentBlood;
    }
    public void Update_mana(float currentMana,float currentSoul,float maxMana) 
    {
        manaUI.maxValue = maxMana;
        manaUI.value = currentMana;
        soulUI.maxValue = maxMana;
        soulUI.value = currentSoul;
    }

}
