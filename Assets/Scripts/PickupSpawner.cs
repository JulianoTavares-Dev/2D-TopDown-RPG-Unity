using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    //Define serialized pickup items 
    [SerializeField] private GameObject goldCoin, healthGlobe, staminaGlobe;

    /// <summary>
    /// This method handles the items dropping for the player as
    /// the player destroys one of the destructible items.
    /// </summary>
    public void DropItems(){
        //Initialize random variable to get a random number between 1 and 5
        int randomNum = Random.Range(1, 5);

        //For the condition that is met below, drop one
        //of the 3 pickup items
        if (randomNum == 1){
            Instantiate(healthGlobe, transform.position, Quaternion.identity);
        }
        if (randomNum == 2){
            Instantiate(staminaGlobe, transform.position, Quaternion.identity);
        }
        if (randomNum == 3){
            int randomAmountOfGold = Random.Range(1, 4);

            for (int i = 0; i < randomAmountOfGold; i++){
                Instantiate(goldCoin, transform.position, Quaternion.identity);
            }
        }
    }
}
