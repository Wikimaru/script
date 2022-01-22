using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrototype : Player
{
    // Start is called before the first frame update
    void Start()
    {
        playerSetup();
        Debug.Log("Damage"+physicalDamage(10, 100));
        Debug.Log("magical" + magicalDamage(86.75f, 200));
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void FixedUpdate()
    {
        playerActionController();
        playerMovement();
        handController(this.gameObject);
        playerUpdate();
    }

}
