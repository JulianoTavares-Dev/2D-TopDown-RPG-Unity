using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : Singleton<UIFade>
{
    //Set serialized Image UI for the screen that will fade
    [SerializeField] private Image fadeScreen;
    
    //Set serialized speed that the screen will fade
    [SerializeField] private float fadeSpeed = 1f;

    //Defines a privte fade routine for the fade effect in the game
    private IEnumerator fadeRoutine;

    /// <summary>
    /// This method fades the screen in.
    /// </summary>
    public void FadeToBlack(){
        if (fadeRoutine != null){
            StopCoroutine(fadeRoutine);
        }

        fadeRoutine = FadeRoutine(1);
        StartCoroutine(fadeRoutine);
    }

    /// <summary>
    /// This method fades the screen away going back to the game.
    /// </summary>
    public void FadeToClear(){
        if (fadeRoutine != null){
            StopCoroutine(fadeRoutine);
        }

        fadeRoutine = FadeRoutine(0);
        StartCoroutine(fadeRoutine);
    }

    /// <summary>
    /// This routine smoothly changes the alpha of fadescreen
    /// to the targetd alpha.
    /// </summary>
    /// <param name="targetAlpha">target alpha of the fadescreen</param>
    /// <returns></returns>
    private IEnumerator FadeRoutine(float targetAlpha){

        //While loop to show fadescreen and target alpha
        while (!Mathf.Approximately(fadeScreen.color.a, targetAlpha)){
            float alpha = Mathf.MoveTowards(fadeScreen.color.a, targetAlpha, fadeSpeed * Time.deltaTime);
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, alpha);
            yield return null;
        }
    }
}
