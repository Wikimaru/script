using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    //player_Status
    [SerializeField]protected float currentHP,currentMaxHP, maxHp;
    [SerializeField] protected float attackSpeed,baseAttackSpeed;
    [SerializeField] protected float baseArmor,baseResistance;
    protected float currentArmor,currentResistance,currentAttackSpeed;

    //Player_movement
    [SerializeField]protected float currentSpeed, basdSpeed;
    [SerializeField] protected float dashCooldown, dashSpeed, dashTime;
    protected Vector2 moveInput,dashDirection;
    protected bool lockmove = false,isDash = false;
    protected float dashCooldownCounter, dashTimeCounter;

    //Player_hand
    [SerializeField] protected float autoAimRange,pickUpRange;
    [SerializeField] protected LayerMask whatIsEnemies,whatIsPickup;
    [SerializeField] protected Collider2D enemy;

    //item/weapon/spell_Slot
    [SerializeField] protected int weaponSlot;
    [SerializeField] protected Weapon[] weapon = new Weapon[] { };
    protected int selectWeapon;
    protected bool hasSwitch,hasPickup;
    [SerializeField] protected Skill[] skill;
    [HideInInspector] public float spellCooldownCounter;
    [SerializeField] protected Spell spell;

    //Player_Component
    [SerializeField] protected Rigidbody2D rB;
    [SerializeField] protected Animator playerAnim;
    [SerializeField] protected GameObject hand,spellSlot,skillSlot;



    protected void playerSetup() 
    {
        skill[0].setup(0);
        skill[1].setup(1);
        selectWeaponSlot(selectWeapon);
        if(spell != null) 
        {
            spell.setup(this);
        }
        GameManager.instance.player = this;
        GameManager.instance.playerGameObject = this.gameObject;
    }
    protected virtual void playerMovement() 
    {
        if (!lockmove) 
        {
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");
            if (ControllerManager.Dpad.isFingerDown) 
            {
                moveInput = ControllerManager.Dpad.direction;
            }
            moveInput.Normalize();
            rB.velocity = moveInput * currentSpeed;
        }
        if (isDash) 
        {
            dashDirection.Normalize();
            rB.velocity = dashDirection * dashSpeed;
        }
        playerAnimation();

    }

    protected virtual void playerAnimation() 
    {
        if (!lockmove) 
        {
            if(moveInput != Vector2.zero) 
            {
                playerAnim.SetBool("Run", true);
            }
            else 
            {
                playerAnim.SetBool("Run", false);
            }
        }

    }

    protected virtual void handController(GameObject playerGameObject)
    {
        Collider2D[] enemiesToAim = Physics2D.OverlapCircleAll(playerGameObject.transform.position, autoAimRange, whatIsEnemies);
        float distanceToClosestEnemy = Mathf.Infinity;
        Collider2D closetEnemy = null;
        for (int i = 0; i < enemiesToAim.Length; i++)
        {
            float distanceToEnemy = (enemiesToAim[i].transform.position - playerGameObject.transform.position).sqrMagnitude;
            if (distanceToEnemy < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = distanceToEnemy;
                closetEnemy = enemiesToAim[i];
            }
        }
        enemy = closetEnemy;
        if (enemiesToAim.Length > 0) 
        {
            Vector3 direction = enemy.transform.position - hand.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            hand.transform.rotation = rotation;
            if(hand.transform.eulerAngles.z > 90 && hand.transform.eulerAngles.z < 270) 
            {
                playerGameObject.transform.localScale = new Vector3(-1f, 1f, 1f);
                hand.transform.localScale = new Vector3(-1f, -1f, 1f);
                spellSlot.transform.localScale = new Vector3(-1f, 1f, 1f);
                skillSlot.transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
                playerGameObject.transform.localScale = Vector3.one;
                hand.transform.localScale = Vector3.one;
                spellSlot.transform.localScale = Vector3.one;
                skillSlot.transform.localScale = Vector3.one;
            }
        }
        else 
        {
            if (moveInput.x < 0)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                spellSlot.transform.localScale = new Vector3(-1f, 1f, 1f);
                skillSlot.transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            if (moveInput.x > 0)
            {
                transform.localScale = Vector3.one;
                spellSlot.transform.localScale = Vector3.one;
                skillSlot.transform.localScale = Vector3.one;
            }
        }

    }
    protected void playerActionController() 
    {
        if (Input.GetKeyDown(KeyCode.Space) || ControllerManager.attackbutton.isFingerDown)
        {
            playerAttack(attackSpeed);
        }
        if (ControllerManager.Dpad.isPointerUpOutOfBound)
        {
            playerDash();
            ControllerManager.Dpad.isPointerUpOutOfBound = false;
        }
        if (ControllerManager.switchButton.isFingerDown) 
        {
            if (!hasSwitch) 
            {
                hasSwitch = true;
                switchWeapon();
            }
        }
        else if(hasSwitch) 
        {
            hasSwitch = false;
        }
        if (ControllerManager.pickupButton.isFingerDown) 
        {
            if (!hasPickup) 
            {
                hasPickup = true;
                playerPickup();
            }
        }
        else if (hasPickup) 
        {
            hasPickup = false;
        }
    }

    protected virtual void playerAttack(float attackspeed) 
    {
        weapon[selectWeapon].Attack(attackspeed);
    }

    protected void switchWeapon() 
    {
        selectWeapon++;
        if (selectWeapon != weaponSlot) 
        {
            if (weapon[selectWeapon] == null) 
            {
                selectWeapon--;
            }
        }

        if(selectWeapon >= weaponSlot) 
        {
            selectWeapon = 0;
        }
        for(int i = 0; i < weaponSlot; i++) 
        {
            if (weapon[i] != null) 
            {
                weapon[i].equip(false);
            }
        }
        weapon[selectWeapon].equip(true);
    }
    protected void selectWeaponSlot(int select) 
    {
        selectWeapon = select;
        for (int i = 0; i < weaponSlot; i++)
        {
            if (weapon[i] != null)
            {
                weapon[i].equip(false);
            }
        }
        weapon[selectWeapon].equip(true);
    }

    protected virtual void playerDash() 
    {
        if(dashCooldownCounter<=0) 
        {
            playerAnim.SetTrigger("Dash");
            dashDirection = ControllerManager.Dpad.direction;
            lockmove = true;
            isDash = true;
            dashCooldownCounter = dashCooldown;
            dashTimeCounter = dashTime;
            Debug.Log("Dash");
        }
    }

    protected void playerPickup() 
    {
        Collider2D[] PickUp = Physics2D.OverlapCircleAll(transform.position, pickUpRange, whatIsPickup);
        float distanceToClosestPickup = Mathf.Infinity;
        Collider2D closetPickup = null;
        for (int i = 0; i < PickUp.Length; i++)
        {
            float distanceToPickup = (PickUp[i].transform.position - this.transform.position).sqrMagnitude;
            if (distanceToPickup < distanceToClosestPickup)
            {
                distanceToClosestPickup = distanceToPickup;
                closetPickup = PickUp[i];
            }
        }
        if(closetPickup != null) 
        {
            closetPickup.GetComponent<Pickup>().pickUp(this);
        }
    }

    protected virtual void playerUpdate() 
    {
        if(dashCooldownCounter >= 0) 
        {
            dashCooldownCounter -= Time.deltaTime;
        }
        if (dashTimeCounter >= 0) 
        {
            dashTimeCounter -= Time.deltaTime;
        }
        else if (isDash) 
        {
            isDash = false;
            lockmove = false;
        }
        if(spellCooldownCounter > 0) 
        {
            spellCooldownCounter -= Time.deltaTime;
            ControllerManager.spellbutton.SetText(spellCooldownCounter.ToString("F1"));
            if (spellCooldownCounter<=0) 
            {
                ControllerManager.spellbutton.SetActiveState(true);
                ControllerManager.spellbutton.SetText(" ");
            }
        }
    }

    protected float physicalDamage(float armor,float damage) 
    {
        float damageMult;
        armor = armor / 2;
        damageMult = 1f - ((0.052f * armor) / (0.9f+(0.048f * armor)));
        return damageMult;
    }
    protected float magicalDamage(float magicalResistance,float damage) 
    {
        float damageMult;
        if(magicalResistance > 90) 
        {
            magicalResistance = 90;
        }
        damageMult = 1f-(magicalResistance/100);
        return damageMult;
    }


    //Public----------------------------------------------
    public void pickUpWeapon(Weapon weaponPickup) 
    {
        bool hasPickup = false;
        for(int i = 0; i < weaponSlot; i++) 
        {
            if (weapon[i]==null) 
            {
                Weapon weaponClone = Instantiate(weaponPickup);
                weaponClone.transform.parent = hand.transform;
                weaponClone.transform.position = hand.transform.position;
                weaponClone.transform.localRotation = Quaternion.Euler(Vector3.zero);
                weaponClone.transform.localScale = Vector3.one;

                weapon[i] = weaponClone;
                selectWeaponSlot(i);
                hasPickup = true;
            }
        }
        if (!hasPickup) 
        {
            Weapon weaponClone = Instantiate(weaponPickup);
            weaponClone.transform.parent = hand.transform;
            weaponClone.transform.position = hand.transform.position;
            weaponClone.transform.localRotation = Quaternion.Euler(Vector3.zero);
            weaponClone.transform.localScale = Vector3.one;

            weapon[selectWeapon].dropWeapon(transform);
            weapon[selectWeapon] = weaponClone;
            selectWeaponSlot(selectWeapon);
            hasPickup = true;
        }
    }
    public void pickUpSpell(Spell spell) 
    {
        if(this.spell != null) 
        {
            Spell spellClone = Instantiate(spell);
            spellClone.transform.parent = spellSlot.transform;
            spellClone.transform.position = spellSlot.transform.position;
            spellClone.transform.localRotation = Quaternion.Euler(Vector3.zero);
            spellClone.transform.localScale = Vector3.one;
            this.spell.Drop(transform);
            this.spell = spellClone;
            this.spell.setup(this);
        }
        else 
        {
            Spell spellClone = Instantiate(spell);
            spellClone.transform.parent = spellSlot.transform;
            spellClone.transform.position = spellSlot.transform.position;
            spellClone.transform.localRotation = Quaternion.Euler(Vector3.zero);
            spellClone.transform.localScale = Vector3.one;
            this.spell = spellClone;
            this.spell.setup(this);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.transform.position, autoAimRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, pickUpRange);
    }
}
