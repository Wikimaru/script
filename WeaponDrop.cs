using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDrop : Pickup
{
    [SerializeField]private Weapon weapon;
    private Vector3 moveDirection;
    private float moveSpeed = 3f;
    private float deceleration = 5f;
    public override void pickUp(Player player) 
    {
        player.pickUpWeapon(weapon);
        Destroy(this.gameObject);
        Debug.Log("get");
    }
    private void Start()
    {
        moveDirection.x = Random.Range(-moveSpeed, moveSpeed);
        moveDirection.y = Random.Range(-moveSpeed, moveSpeed);
    }
    private void Update()
    {
        transform.position += moveDirection * Time.deltaTime;
        moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, deceleration * Time.deltaTime);
    }

}
