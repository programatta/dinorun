using System.Collections;
using UnityEngine;

public class CreditsUI : MonoBehaviour
{
    [SerializeField] GameObject backButton;

    /*=======================================================================*/
    /*                                 UI Events                             */
    /*=======================================================================*/
    public void GoMainMenu()
    {
        StartCoroutine(DoGoMainMenu());
    }


    /*=======================================================================*/
    /*                             Private Section                           */
    /*=======================================================================*/
    IEnumerator DoGoMainMenu()
    {
        // AudioSource audioSource = backButton.GetComponent<AudioSource>();
        // audioSource.mute = SoundControl.Instance.IsFXMuted;
        // audioSource.volume = SoundControl.Instance.FXVolume;
        // audioSource.Play();
        UIButtonFX fx = backButton.GetComponent<UIButtonFX>();
        fx.PlayFX(UIButtonFX.ButtonFXType.Click);

        //No esperamos a que finalice ya que ahora llamamos a una corrutina para hacer
        //la transición de la escena a través de SceneLoader.
        //yield return new WaitWhile(()=>audioSource.isPlaying);
        yield return null;
        SceneLoader.Instance.LoadNextLevel("MainMenu");
    }
}
