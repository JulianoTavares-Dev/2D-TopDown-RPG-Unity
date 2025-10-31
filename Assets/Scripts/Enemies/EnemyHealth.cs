using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    //Set up serialized variables
    [SerializeField] private int startingHealth = 3;
    [SerializeField] private GameObject deathVFXPrefab;
    [SerializeField] private float knockBackThrust = 15f;

    //Setting up current health int variable,
    //knockback and flash.
    private int currentHealth;
    private Knockback knockback;
    private Flash flash;

    /// <summary>
    /// This method is called when the game first start and it
    /// gets the component for flash as set up above and does the same
    /// for the knockback.
    /// </summary>
    private void Awake(){

        //Get component for flash
        flash = GetComponent<Flash>();

        //Get component for knockback
        knockback = GetComponent<Knockback>();
    }

    /// <summary>
    /// This method is called when the game first starts just like
    /// the Awake method but it is called on the frame when
    /// a script is enabled and it set the currentHealth to the enemy
    /// starting health.
    /// </summary>
    private void Start(){
        currentHealth = startingHealth;
    }

    /// <summary>
    /// This method is for when the enemy gets hit/takes damage and it
    /// minus the damage taken by the enemy from its current health, knocks
    /// the enemy back and uses flash as an effect to show enemy has been hit.
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage) {

        //minus the damage from the health of the enemy
        currentHealth -= damage;

        //enemy gets knocked back
        knockback.GetKnockedBack(PlayerController.Instance.transform, knockBackThrust);

        //Start coroutine for the flash effect
        StartCoroutine(flash.FlashRoutine());

        //Start coroutine to check if the enemy is dead
        StartCoroutine(CheckDetectDeathRoutine());
    }

    /// <summary>
    /// This method Checks for the Death of the Enemy routine
    /// it wait for the time to flash and call on the detect death
    /// method below.
    /// </summary>
    /// <returns></returns>
    private IEnumerator CheckDetectDeathRoutine(){
        //Wait for time to flash
        yield return new WaitForSeconds(flash.GetRestoreMatTime());

        //Call on the detect death method below
        DetectDeath();
    }

    /// <summary>
    /// This method Detects the death of the enemy if the 
    /// health of the enemy is lower or equal to 0.
    /// </summary>
    public void DetectDeath()
    {
        //Checks if the current health of the enemy is less or equal to 0
        if (currentHealth <= 0){

            //If condition is met, instantiate using the death visual effect, position and identity
            Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);

            //Drop pickup items when enemy dies
            GetComponent<PickupSpawner>().DropItems();

            //Destroys the enemy object making it disappear
            Destroy(gameObject); 
        }
    }
}
