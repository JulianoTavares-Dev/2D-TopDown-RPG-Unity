using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : Singleton<Stamina>
{
    //Get the current stamina of the player
    public int CurrentStamina { get; private set; }

    //Define serialized variables
    [SerializeField] private Sprite fullStaminaImage, emptyStaminaImage;
    [SerializeField] private int timeBetweenStaminaRefresh = 3;

    //Define other game variables
    private Transform staminaContainer;
    private int startingStamina = 3;
    private int maxStamina;

    //Define constant variables
    const string STAMINA_CONTAINER_TEXT = "Stamina Container";

    /// <summary>
    /// This method first runs when the game is first awake
    /// and gets the variables initialized above.
    /// </summary>
    protected override void Awake(){
        //Call the awake method from the parent class
        base.Awake();
        //Set the maximum stamina the player has to the starting stamina
        //the player starts the game with
        maxStamina = startingStamina;
        //Sets the current stamina of the player to the stamina
        //the player starts with as well
        CurrentStamina = startingStamina;
    }

    /// <summary>
    /// This method runs when the game first starts and
    /// it assings the variable stamina container accordingly.
    /// </summary>
    private void Start(){
        staminaContainer = GameObject.Find(STAMINA_CONTAINER_TEXT).transform;
    }

    /// <summary>
    /// This method takes care of 
    /// lowering the stamina as the player uses it.
    /// </summary>
    public void UseStamina(){

        //Lower current stamina by one
        CurrentStamina--;
        //Update the stamina UI on player's screen
        UpdateStaminaImages();
    }

    /// <summary>
    /// This method refreshes the stamina of the player.
    /// </summary>
    public void RefreshStamina(){

        //Check if current stamina is less than
        // the max stamina and add one to the current
        //stamina
        if (CurrentStamina < maxStamina){
            CurrentStamina++;
        }
        //update how the stamina UI looks on the player's screen
        UpdateStaminaImages();
    }

    /// <summary>
    /// This routine continuously refreshes the stamina
    /// of the palyer at intervals.
    /// </summary>
    /// <returns></returns>
    private IEnumerator RefreshStaminaRoutine(){
        //Check to see if condition is true
        //and keep looping while it is true
        while (true){
            //Pause for a few seconds
            yield return new WaitForSeconds(timeBetweenStaminaRefresh);
            //Call back on the refresh stamina method above
            RefreshStamina();
        }
    }

    /// <summary>
    /// This method updates the stamina UI displaying to the
    /// player and restarts the coroutine above.
    /// </summary>
    private void UpdateStaminaImages(){
        //Check to see if condition is true
        for (int i = 0; i < maxStamina; i++){
            if (i <= CurrentStamina - 1){
                staminaContainer.GetChild(i).GetComponent<Image>().sprite = fullStaminaImage;
            } else{
                staminaContainer.GetChild(i).GetComponent<Image>().sprite = emptyStaminaImage;
            }
        }
        //If condition is true, stop coroutines and 
        //start only the Refresh Stamina routine
        if (CurrentStamina < maxStamina){
            StopAllCoroutines();
            StartCoroutine(RefreshStaminaRoutine());
        }
    }
}
