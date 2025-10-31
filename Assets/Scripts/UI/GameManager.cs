using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    //Defines instance for the game manager
    public static GameManager Instance { get; private set; }

    //Defines UI objects
    public GameObject gameOverUI;
    public GameObject winScreenUI;
    public Timer timer;

    //Defines save manager and player object
    public SaveManager saveManager;
    public GameObject player;

    //Define constants
    private const float gameOverTime = 500f;
    private const int winCoinCount = 100;
    const string TOWN_TEXT = "Core Nexus";

    //Define other variables
    public int coinsCollected = 0;

    /// <summary>
    /// This method runs when the game is first awake and it
    /// basically finds the player object and sets the camera
    /// to follow the player.
    /// </summary>
    private void Awake()
    {
        //Don't destory the player object if instance is null
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //Destory the player object if instance is not null
        else
        {
            Destroy(gameObject);
        }

        //Set camera to follow the player
        CameraController.Instance.SetPlayerCameraFollow();
    }

    /// <summary>
    /// This method runs when the game first starts and
    /// it set the UI menus to false so they don't show yet.
    /// </summary>
    private void Start(){
        gameOverUI.SetActive(false);
        winScreenUI.SetActive(false);
    }

    /// <summary>
    /// This method constantly updates throughout the game and 
    /// it calls on different method within this script.
    /// </summary>
    private void Update()
    {
        //Check if the timer has reached the game over time
        if (timer.timeRemaining >= gameOverTime)
        {
            gameOver();
        }

        //Check for key presses to save and load the game
        if (Input.GetKeyDown(KeyCode.K))
        {
            SaveGame();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadGame();
        }
        //Check for key press to quit the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            mainMenu();
            
        }
    }

    /// <summary>
    /// This method is for when the player collects the coins.
    /// </summary>
    public void CollectCoin()
    {
        //Add collected coins to coin count of the player
        coinsCollected++;
        Debug.Log("Coins Collected: " + coinsCollected);

        //Player wins if coins collected are more than the win coin count
        if (coinsCollected >= winCoinCount)
        {
            Win();
        }
    }

    /// <summary>
    /// This method is for when the player loses and gets
    /// the game over screen meaning player needs to start
    /// the game again.
    /// </summary>
    public void gameOver(){
        //Show game over screen
        gameOverUI.SetActive(true);
        //Don't show timer/stop timer
        timer.timeIsRunning = false;
    }

    /// <summary>
    /// This method is for when the player wins and the 
    /// win screen shows to the player.
    /// </summary>
    public void Win()
    {   
        //Show win screen
        winScreenUI.SetActive(true);
        //Stop timer
        timer.timeIsRunning = false; 
    }

    /// <summary>
    /// This method restarts the game back to the start.
    /// </summary>
    public void restart(){
        
        //Deactivate UI Screens
        gameOverUI.SetActive(false);
        winScreenUI.SetActive(false);

        //Reset timer, coin count
        timer.timeRemaining = 0f;
        timer.timeIsRunning = true; 
        coinsCollected = 0; 
        
        //Load Scene 1/Level 1 of the game
        SceneManager.LoadScene("Core Nexus");
        Debug.Log("Restart!!!");
    }

    /// <summary>
    /// This method takes the player back to the main menu
    /// also known as Command Centre.
    /// </summary>
    public void mainMenu(){
        //Set game over and win screen to false
        gameOverUI.SetActive(false);
        winScreenUI.SetActive(false);

        //Load Main Menu scene
        SceneManager.LoadScene("Command Centre");
        Debug.Log("Command Centre!!!");
    } 

    /// <summary>
    /// This method Saves the game.
    /// </summary>
    public void SaveGame()
    {
        // ave the game state before going to the main menu
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            //Save these game components/variables
            int playerCoins = coinsCollected; 
            int playerDeathCount = playerHealth.deathCount;
            int playerCurrentHealth = (int)playerHealth.currentHealth;
            int playerStaminaLevel = 0; 

            //Use Save Game method of the save manager to save game
            saveManager.SaveGame(playerCoins, playerDeathCount, playerCurrentHealth, playerStaminaLevel, player.transform.position);
        }
    }

    /// <summary>
    /// This method Loads the game.
    /// </summary>
    public void LoadGame()
    {
        //Load the game data
        SaveData saveData = saveManager.LoadGame();
        if (saveData != null)
        {
            //Update GameManager or PlayerHealth with loaded data
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.deathCount = saveData.deathCount;
                playerHealth.currentHealth = saveData.currentHealth;
                playerHealth.SetPlayerPosition(saveData.playerPosition.ToVector3());
            }
            //Load the coins collected by the player
            coinsCollected = saveData.coins;
            Debug.Log("Game Loaded");
        }
        else
        {
            //Show error if the game could not be loaded because save data wasn't found
            Debug.LogWarning("No save data found.");
        }

        // Assuming you have a CameraController instance to set the camera follow
        //CameraController.Instance.SetPlayerCameraFollow(player.transform);
    }

    /*public void quit(){
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Quit!!!");
        }
    }
    */
}
