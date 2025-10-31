using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    //Define singleton T instance pattern for the game
    private static T instance;
    public static T Instance { get { return instance; } }

    /// <summary>
    /// This method is called as the game is first Awake and
    /// it implements the singleton pattern into the MonoBehaviour
    /// scripts.
    /// </summary>
    protected virtual void Awake(){
        if (instance != null && this.gameObject != null){
            Destroy(this.gameObject);
        }
        else {
            instance = (T)this;
        }

        if (!gameObject.transform.parent){
            DontDestroyOnLoad(gameObject);
        }
    }
}
