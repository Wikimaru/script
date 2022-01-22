using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum CheckType 
    {
        player
    }
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }

    public Player player;
    public GameObject playerGameObject;

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

}
