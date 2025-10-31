using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //Singleton instance
    public static AudioManager Instance { get; private set; }
    
    //Array for storing audio clips
    public AudioClip[] audioClipsArray;

    //AudioSource component to play the audio
    private AudioSource audioSource;

    /// <summary>
    /// This method runs when the game is first awake and
    /// it gets the audio source component and check if audio
    /// has been added in the inspector in unity.
    /// </summary>
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    /// <summary>
    /// This method plays an audio clip from the array
    /// </summary>
    /// <param name="index"></param>
    public void PlayAudioFromArray(int index)
    {
        //Check if audio manager instance has audios in it and isn't null
        if (Instance == null)
        {
            Debug.LogError("AudioManager Instance is null!");
            return;
        }

        //Check the index of the audio clips array
        if (index >= 0 && index < audioClipsArray.Length)
        {
            //Play audio according to its index in the array
            AudioClip clip = audioClipsArray[index];
            if (clip != null)
            {
                audioSource.clip = clip;
                audioSource.Play();
            }
            else
            {
                Debug.LogWarning("Audio clip at index " + index + " is null.");
            }
        }
        else
        {
            Debug.LogWarning("Audio clip index " + index + " is out of range!");
        }
    }
}
