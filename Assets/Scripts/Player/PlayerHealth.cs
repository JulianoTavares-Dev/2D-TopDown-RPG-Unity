using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : Singleton<PlayerHealth>
{
    //Define boolean to check if player is dead or not
    public bool isDead { get; private set; }

    //Define serialized variables for the player's health
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockBackThrustAmount = 10f;
    [SerializeField] private float damageRecoveryTime = 1f;

    //Define other variables related to the health of the player
    //the damage and teh death of the player
    private Slider healthSlider;
    public int currentHealth;
    public int deathCount = 0;
    private int maxDeathCount = 5;
    private bool canTakeDamage = true;
    private Knockback knockback;
    private Flash flash;
    public GameManager gameManager;

    //Define constants for the game
    const string HEALTH_SLIDER_TEXT = "Health Slider";
    const string TOWN_TEXT = "Core Nexus";
    const string MAIN_MENU = "Command Centre";
    readonly int DEATH_HASH = Animator.StringToHash("Death");

    /// <summary>
    /// This method runs when the game is first awake
    /// and it starts the components of the game.
    /// </summary>
    protected override void Awake(){
        //Call the Awake method at parent class level
        base.Awake();
        //Get components for the player
        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
    }

    /// <summary>
    /// This method runs when the game starts and it takes care
    /// of setting the player health to full.
    /// </summary>
    private void Start(){
        //Set the player to not be dead yet
        isDead = false;

        //Set current health of the player to max health
        if (currentHealth == 0) 
        {
            currentHealth = maxHealth;
        }

        //Update health slider to be full
        UpdateHealthSlider();
    }

    /// <summary>
    /// This method runs when there is a collision between
    /// the player and the enemy and player takes damage.
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionStay2D(Collision2D other){
        //Start the enemies of the game
        EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();

        //Check if enemy touched player and if it did, player takes damage
        if (enemy){
            TakeDamage(1, other.transform);
        }
    }

    /// <summary>
    /// This method heals the player when the player
    /// gets a health globe in the game.
    /// </summary>
    public void HealPlayer(){
        //Check if the health of the player isn't full when
        //player gets the health globe
        if(currentHealth < maxHealth){
            //If player isn't with full health,
            //heal the player by 1
            currentHealth += 1;
            //Update the health slider to go up
            UpdateHealthSlider();
        }
    }

    /// <summary>
    /// This method handles the player taking a hit/damage.
    /// </summary>
    /// <param name="damageAmount"></param>
    /// <param name="hitTransform"></param>
    public void TakeDamage(int damageAmount, Transform hitTransform){
        //Check if player can take damage
        if(!canTakeDamage) { return; }
        
        //Run and start the variables and references some methods
        ScreenShakeManager.Instance.ShakeScreen();
        knockback.GetKnockedBack(hitTransform, knockBackThrustAmount);
        StartCoroutine(flash.FlashRoutine());
        canTakeDamage = false;
        currentHealth -= damageAmount;
        StartCoroutine(DamageRecoveryRoutine());
        UpdateHealthSlider();
        CheckIfPlayerDeath();
    }

    /// <summary>
    /// This method checks to know whether the player is dead
    /// or the player is still alive.
    /// </summary>
    private void CheckIfPlayerDeath(){
        //Check if the player is dead and the health of the player is at 0
        if (currentHealth <= 0 && !isDead){

            //If condition is met set is dead bool to true
            isDead = true;
            //Destroy the player game object
            Destroy(ActiveWeapon.Instance.gameObject);

            //Set the health of the player back to 0
            currentHealth = 0;
            //Add 1 to the death count of the player
            deathCount++;
            //Display the added death count of the player
            Debug.Log("Death count added, Current death count = " + deathCount);

            //Get the animator component of the player's death
            GetComponent<Animator>().SetTrigger(DEATH_HASH);

            //Check if the death count of the player is more than
            //the max death count which is 5, if it is, player goes
            //back to the main menu.
            if (deathCount >= maxDeathCount)
            {
                StartCoroutine(TransitionToMainMenu());
            } else {
                StartCoroutine(DeathLoadScreenRoutine());
            }
        }
    }

    /// <summary>
    /// This method defines the current position of the player.
    /// </summary>
    /// <param name="position"></param>
    public void SetPlayerPosition(Vector3 position)
    {
        //Set position of the player
        transform.position = position;
    }

    /// <summary>
    /// This routine runs after the player has died.
    /// </summary>
    /// <returns></returns>
    private IEnumerator DeathLoadScreenRoutine(){
        //Pause for 2 seconds
        yield return new WaitForSeconds(2f);
        //Destory the game object which is the player
        Destroy(gameObject);
        //Load the firs Scene/Level
        SceneManager.LoadScene(TOWN_TEXT);
    }

    /// <summary>
    /// This routine runs when the death counts
    /// are more than the max death count and 
    /// the player has lost.
    /// </summary>
    /// <returns></returns>
    private IEnumerator TransitionToMainMenu()
    {
        //Pause for 2 seconds
        yield return new WaitForSeconds(2f);
        //Destory the player game object
        Destroy(gameObject);
        //Load the Main Menu
        SceneManager.LoadScene(MAIN_MENU);
    }

    /// <summary>
    /// This routine runs when the player is recovering
    /// from the damage taken.
    /// </summary>
    /// <returns></returns>
    private IEnumerator DamageRecoveryRoutine(){
        //Pause for a few seconds
        yield return new WaitForSeconds(damageRecoveryTime);
        //Set the variable that the player can take damage back
        //to true
        canTakeDamage = true;
    }
    
    /// <summary>
    /// This method updates the health slider that shows
    /// the health of the player accordingly.
    /// </summary>
    private void UpdateHealthSlider(){
        //If the health slider bar is equal to null
        if (healthSlider == null){
            //Get the Health Slider object
            healthSlider = GameObject.Find(HEALTH_SLIDER_TEXT).GetComponent<Slider>();
        }

        //Set the health slider max value to be the same as the player's
        //max health
        healthSlider.maxValue = maxHealth;
        //Set the current health slider value to be the same as the player's
        //current health
        healthSlider.value = currentHealth;
    }
}
