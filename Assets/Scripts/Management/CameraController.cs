using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : Singleton<CameraController>
{
    //Set up Cinemachine Camera system for the game
    private CinemachineVirtualCamera cinemachineVirtualCamera;

    /// <summary>
    /// This method starts the game and references
    /// the method that set the camera to follow the
    /// player right from the start.
    /// </summary>
    private void Start(){
        SetPlayerCameraFollow();
    }

    /// <summary>
    /// This method finds the player object and sets
    /// the camera to follow the player.
    /// </summary>
    public void SetPlayerCameraFollow(){
        cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        cinemachineVirtualCamera.Follow = PlayerController.Instance.transform;
    }
}
