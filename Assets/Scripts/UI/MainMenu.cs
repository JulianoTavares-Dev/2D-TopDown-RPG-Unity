using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using UnityEngine.UI; 
using TMPro;

public class MainMenu : MonoBehaviour
{
    //Define texts
    public TMP_InputField playerNameInputField;
    public TMP_Text errorText;
    
    /// <summary>
    /// This method plays the game when the play game button is clicked.
    /// </summary>
    public void PlayGame()
    {
        //Get player input and trim whitespace
        string playerName = playerNameInputField.text.Trim();

        //Example regular expression for a valid name (letters, spaces, hyphens, apostrophes)
        //Adjust as per your validation requirements
        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z\s'-]+$");

        if (!regex.IsMatch(playerName))
        {
            //Show error message if input is not a valid name
            errorText.text = "Please enter a valid name (letters, spaces, hyphens, apostrophes only).";
        }
        else
        {
            //Clear error message
            errorText.text = "";

            //Save playerName to PlayerPrefs or GameManager for later use
            PlayerPrefs.SetString("PlayerName", playerName);
           
            //Load the next scene (replace "GameScene" with your actual scene name)
            SceneManager.LoadSceneAsync(1);
        }
    }

    /// <summary>
    /// This method exits the game.
    /// </summary>
    public void ExitGame(){
        Application.Quit();
    }
}
