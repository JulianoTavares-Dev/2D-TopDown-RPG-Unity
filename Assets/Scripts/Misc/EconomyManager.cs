using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EconomyManager : Singleton<EconomyManager>
{
    //Set up gold text and current gold the player has.
    private TMP_Text goldText;
    private int currentGold = 0;

    //Set up the Text for the Gold Coin Amount.
    const string COIN_AMOUNT_TEXT = "Gold Amount Text";

    /// <summary>
    /// This method Updates the current gold coin amount the player
    /// has by adding 1 to the current gold coin amount.
    /// </summary>
    public void UpdateCurrentGold(){
        
        //Add 1 to the current amount of gold coins the player has
        currentGold += 1;

        //If there is a gold coin text available, find that game object
        if (goldText == null){
            goldText = GameObject.Find(COIN_AMOUNT_TEXT).GetComponent<TMP_Text>();
        }

        goldText.text = currentGold.ToString("D3");
    }
}
