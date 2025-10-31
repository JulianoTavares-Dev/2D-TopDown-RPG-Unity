using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

//Define serialized variables in the save manager C# Script 
//that is attached to other objects in the game.
[System.Serializable]
public class SaveData
{
    public int coins;
    public int deathCount;
    public int currentHealth;
    public int staminaLevel;
    public SerializableVector3 playerPosition;
}

public class SaveManager : MonoBehaviour
{
    //Define variables for the game
    public static SaveManager instance;
    private string saveFilePath;

    /// <summary>
    /// This method uses the persistent data path feature in unity
    /// to save a json file of the current game data.
    /// </summary>
    void Awake()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "savefile.json");
    }

    /// <summary>
    /// This method Saves the variables of the game like
    /// coins the player has, the health, etc...
    /// </summary>
    /// <param name="coins"></param>
    /// <param name="deathCount"></param>
    /// <param name="currentHealth"></param>
    /// <param name="staminaLevel"></param>
    /// <param name="playerPosition"></param>
    public void SaveGame(int coins, int deathCount, int currentHealth, int staminaLevel, Vector3 playerPosition)
    {
        //Save the variables below
        SaveData saveData = new SaveData();
        saveData.coins = coins;
        saveData.deathCount = deathCount;
        saveData.currentHealth = currentHealth;
        saveData.staminaLevel = staminaLevel;
        saveData.playerPosition = new SerializableVector3(playerPosition);

        //Save it to the json file using persistent data path
        string json = JsonUtility.ToJson(saveData);
        //Write lines to the json file
        File.WriteAllText(saveFilePath, json);
        //Show on the console game saved to show player it has been saved
        Debug.Log("Game Saved");
    }

    /// <summary>
    /// This method loads the game from the json file.
    /// </summary>
    /// <returns></returns>
    public SaveData LoadGame()
    {
        //If file exists, load it
        if (File.Exists(saveFilePath))
        {
            //Read all lines from the file
            string json = File.ReadAllText(saveFilePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);
            //Show player message on the console to show it has been loaded
            Debug.Log("Game Loaded");
            return saveData;
        }
        else
        {
            //If file doesn't exist, show there was an issue with the game
            Debug.LogWarning("Save file not found");
            return null;
        }
    }
}