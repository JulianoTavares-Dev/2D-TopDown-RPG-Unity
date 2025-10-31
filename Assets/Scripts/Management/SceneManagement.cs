using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagement : Singleton<SceneManagement>
{
    //Get the Scene Transition Name string used in other scripts
    public string SceneTransitionName { get; private set; }

    /// <summary>
    /// This method Sets the transition name.
    /// </summary>
    /// <param name="sceneTransitionName"></param>
    public void SetTransitionName(string sceneTransitionName) {
        this.SceneTransitionName = sceneTransitionName;
    }
}
