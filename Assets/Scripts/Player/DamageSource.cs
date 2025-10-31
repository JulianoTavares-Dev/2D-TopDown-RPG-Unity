using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    //Define damage amount integer variable
    private int damageAmount;

    /// <summary>
    /// This method runs when the game first starts and 
    /// it retrieves the current weapon the player is with
    /// and the damage amount of the weapon the player has in 
    /// hands.
    /// </summary>
    private void Start(){
        //retrieves current weapon player is with
        MonoBehaviour currentActiveWeapon = ActiveWeapon.Instance.CurrentActiveWeapon;
        //check the damage amount of that weapon
        damageAmount = (currentActiveWeapon as IWeapon).GetWeaponInfo().weaponDamage;
    }

    /// <summary>
    /// This method runs when the collider 2d is triggered
    /// and it takes care of the enemy health when the player
    /// damages the enemy.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other){
        //get enemy health
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        //enemy takes damage and deduct damage amount from the weapon 
        //the player has attacked with
        enemyHealth?.TakeDamage(damageAmount);
    }
}
