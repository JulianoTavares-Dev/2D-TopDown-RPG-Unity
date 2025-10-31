using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    //Defines Getting Knocked Back property from outside of this script
    public bool GettingKnockedBack { get; private set; }

    //Set the serialized knock back time
    [SerializeField] private float knockBackTime = .2f;

    //Define the Rigidbody 2d for the player and the enemies
    private Rigidbody2D rb;

    /// <summary>
    /// This method runs when the game first starts (Awake) and
    /// it gets the rigidbody 2d component from the player object
    /// and the enemy objects inside Unity.
    /// </summary>
    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// This method calls back on the getting knocked back
    /// by setting the player getting knocked back to true,
    /// checking the position of the player, the force that
    /// will be added to the rigid body 2d and running the routine.
    /// </summary>
    /// <param name="damageSource"></param>
    /// <param name="knockBackThrust"></param>
    public void GetKnockedBack(Transform damageSource, float knockBackThrust){
        //Setting getting knocked back property to true
        GettingKnockedBack = true;
        //Check the position difference between the player now and the player after being knocked back
        Vector2 difference = (transform.position - damageSource.position).normalized * knockBackThrust * rb.mass;
        //Use add force property of the rigid body 2d
        rb.AddForce(difference, ForceMode2D.Impulse);
        //Run Knock routine method below
        StartCoroutine(KnockRoutine());
    }

    /// <summary>
    /// This method pauses for a specified amount of time,
    /// set the velocity of the player to 0 stopping the player
    /// and set the knockback variable to false meaning the player
    /// is no longer being knocked back.
    /// </summary>
    /// <returns></returns>
    private IEnumerator KnockRoutine(){
        //Pauses for the time set to the variable knock back time
        yield return new WaitForSeconds(knockBackTime);
        //Set velocity of the player's rigidbody 2d to 0
        //stopping th player
        rb.velocity = Vector2.zero;
        //Set the getting knocked back variable to false
        GettingKnockedBack = false;
    }
}
