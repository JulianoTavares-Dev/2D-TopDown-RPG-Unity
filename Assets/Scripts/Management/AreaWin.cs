using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaWin : MonoBehaviour
{
    //Set up serialized Win Screen Game object
    [SerializeField] public GameObject winScreenUI;

    //Set up waiting time before showing the Win Screen
    private float waitToShowWinScreen = 1f;

    /// <summary>
    /// This method checks if the player has collided with the collider 2d
    /// that is a trigger to start the Win screen routine and therefore show
    /// to the player the win screen.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Check if player object has initialized
        if (other.gameObject.GetComponent<PlayerController>())
        {
            //If condition is met, fade in the win screen
            UIFade.Instance.FadeToBlack();
            //reference the Win Screen Routine
            StartCoroutine(ShowWinScreenRoutine());
        }
    }

    /// <summary>
    /// This routine wait for some time and cals on the 
    /// show win screen method.
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShowWinScreenRoutine()
    {
        //Wait sometime before showing the win screen
        while (waitToShowWinScreen >= 0)
        {
            waitToShowWinScreen -= Time.deltaTime;
            yield return null;
        }

        //Call on the show win screen method to show the win screen to the player
        ShowWinScreen();
    }

    /// <summary>
    /// This method shows the win screen to the player.
    /// </summary>
    private void ShowWinScreen()
    {
        //Check to see if there is a win screen UI available
        if (winScreenUI != null)
        {
            //If there is, show win screen and show message to player
            winScreenUI.SetActive(true);
            Time.timeScale = 0f; // Pause the game
            Debug.Log("You Win!");
        }
        else
        {
            //If there isn't show error
            Debug.LogWarning("Win screen UI is not assigned.");
        }
    }

    
}
