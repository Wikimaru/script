using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPrototype : MonoBehaviour
{
    [SerializeField]private Rigidbody2D theRB;
    [SerializeField] private GameObject ImpactEffect;
    [SerializeField] private float Damage;
    [SerializeField] private float Speed;
    [SerializeField] private float lifeTime;
    private float lifeTimeCounter;
    // Start is called before the first frame update
    void Start()
    {
        lifeTimeCounter = lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = transform.right * Speed;
        if(lifeTimeCounter > 0) 
        {
            lifeTimeCounter -= Time.deltaTime;
            if(lifeTimeCounter<= 0) 
            {
                Destroy(this.gameObject);
            }
        }
    }
}
