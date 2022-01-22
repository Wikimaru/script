using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    public enum ItemType 
    {
        weapon,spell
    }
    [SerializeField]protected ItemType type;
    public abstract void pickUp(Player player); 

    public ItemType GetType() {return type;}
}
