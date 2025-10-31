using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{

    //The currently equipped weapon that the player is using. 
    //The private setter ensures that only this class can modify it, 
    //while allowing other classes to read its value.
    public MonoBehaviour CurrentActiveWeapon { get; private set; }

    //A reference to the PlayerControls class, which likely manages the player's input controls. 
    //This reference will be used to check and handle player inputs.
    private PlayerControls playerControls;

    //A float variable that determines the cooldown time between consecutive attacks. 
    //This helps to prevent the player from attacking continuously without delay.
    private float timeBetweenAttacks;

    //Sets up the boolean variables attack button down and whether player is attacking or not.
    private bool attackButtonDown, isAttacking = false;

    /// <summary>
    /// This method initializes the playerControls 
    /// instance to manage player input within the Awake method.
    /// </summary>
    protected override void Awake(){
        //Set awake method to run from the parent class
        base.Awake();
        //Define new player controls
        playerControls = new PlayerControls();
    }

    /// <summary>
    /// This method plays when the script is enabled and it
    /// enables the player controls that have been created 
    /// in this script as well.
    /// </summary>
    private void OnEnable(){
        playerControls.Enable();
    }

    /// <summary>
    /// This method first plays when the game starts and it
    /// takes care of whether the player is attacking or not
    /// and the cooldown time between the attacks.
    /// </summary>
    private void Start(){

        //Set started player controls to player is attacking
        playerControls.Combat.Attack.started +=_=> StartAttacking();

        //Set canceled player controls to player is no longer attacking
        playerControls.Combat.Attack.canceled +=_=> StopAttacking();

        //Use Cooldown between the attacks of the player
        AttackCooldown();
    }

    /// <summary>
    /// This method updates as the game goes
    /// and it references the Attack method.
    /// </summary>
    private void Update(){
        Attack();
    }

    /// <summary>
    /// This method takes care of what a New Weapon 
    /// does when the player changes weapons.
    /// </summary>
    /// <param name="newWeapon"></param>
    public void NewWeapon(MonoBehaviour newWeapon) {
        //set Current Active Weapon object to the new weapon
        CurrentActiveWeapon = newWeapon;
        //Cooldown after each attack
        AttackCooldown();
        //Set the time between each attack to the cooldown time
        timeBetweenAttacks = (CurrentActiveWeapon as IWeapon).GetWeaponInfo().weaponCooldown;
    }

    /// <summary>
    /// This method set the active weapon object to
    /// null.
    /// </summary>
    public void WeaponNull(){
        CurrentActiveWeapon = null;
    }

    /// <summary>
    /// This method takes care of the cooldown time
    /// between each attack.
    /// </summary>
    private void AttackCooldown(){
        //set the player is currently attacking to true
        isAttacking = true;
        //Pause all the coroutines that are running
        StopAllCoroutines();
        //Start Time betwen attack routine
        StartCoroutine(TimeBetweenAttacksRoutine());
    }

    /// <summary>
    /// This method takes care of the time routine that runs
    /// between the attacks of the player.
    /// </summary>
    /// <returns></returns>
    private IEnumerator TimeBetweenAttacksRoutine(){
        //pause for a few seconds
        yield return new WaitForSeconds(timeBetweenAttacks);
        //set is attacking feature of the player to false meaning
        //at the moment player is not attacking
        isAttacking = false;
    }

    /// <summary>
    /// This method starts attacking by checking if the attack button
    /// of the mouse is being pressed.
    /// </summary>
    private void StartAttacking(){
        attackButtonDown = true;
    }

    /// <summary>
    /// This method stops attacking when teh attack button is not 
    /// being pressed.
    /// </summary>
    private void StopAttacking(){
        attackButtonDown = false;
    }

    /// <summary>
    /// This method attacks if the player is not already attacking
    /// the current weapon is set and the attack button is being
    /// pressed, it also checks for the attack cooldown which is the
    /// waiting time between attacks every time player attacks.
    /// </summary>
    private void Attack(){
        //Check if condition is met
        if (attackButtonDown && !isAttacking && CurrentActiveWeapon){
            //pause for a few seconds (cooldown)
            AttackCooldown();
            //Attack with the current weapon the player has in hands
            (CurrentActiveWeapon as IWeapon).Attack();
        }
    }
}
