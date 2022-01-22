using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeBombs : MonoBehaviour
{
    private float lifetime = 2,lifetimecounter;
    void Start()
    {
        lifetimecounter = lifetime;
    }

    // Update is called once per frame
    void Update()
    {
        if (lifetimecounter > 0) 
        {
            lifetimecounter -= Time.deltaTime;
            if(lifetimecounter <= 0) 
            {
                Destroy(this.gameObject);
            }
        }
    }
}
