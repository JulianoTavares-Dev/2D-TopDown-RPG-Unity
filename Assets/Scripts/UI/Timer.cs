using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    //Define variables and text
    public float timeRemaining = 0;
    public bool timeIsRunning = true;
    public TMP_Text timeText;

    /// <summary>
    /// Thsi method is called before the first frame updates
    /// and it sets the time is running variable to true and resets
    /// the timer.
    /// </summary>
    private void Start()
    {
        timeIsRunning = true;
        timeRemaining = 0;
    }

    /// <summary>
    /// This method is called once per frame and it
    /// checks if the time is still running and if it is,
    /// it displays the time to the player.
    /// </summary>
    private void Update()
    {
        if (timeIsRunning)
        {
            timeRemaining += Time.deltaTime;
            DisplayTime(timeRemaining);
        }
    }

    /// <summary>
    /// This method displays the time to the player.
    /// </summary>
    /// <param name="timeToDisplay"></param>
    private void DisplayTime(float timeToDisplay){
        //Count minutes and seconds in the timer
        float minutes = Mathf.FloorToInt (timeToDisplay / 60);
        float seconds = Mathf.FloorToInt (timeToDisplay % 60);
        //Display the time on the time text in this specific structure
        timeText.text = string.Format ("{0:00}:{1:00}", minutes, seconds);
    }
}
