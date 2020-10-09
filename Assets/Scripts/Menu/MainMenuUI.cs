using System.Collections;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] GameObject playButton;
    [SerializeField] GameObject creditsButton;
    [SerializeField] GameObject exitButton;

    /*=======================================================================*/
    /*                                 UI Events                             */
    /*=======================================================================*/
    public void PlayClick()
    {
        StartCoroutine(DoPlayGame());
    }

    public void CreditsClick()
    {
        StartCoroutine(DoShowCredits());
    }

    public void ExitClick()
    {
        StartCoroutine(DoExitGame());
    }


    /*=======================================================================*/
    /*                             Private Section                           */
    /*=======================================================================*/
    IEnumerator DoPlayGame()
    {
        // AudioSource audioSource = playButton.GetComponent<AudioSource>();
        // audioSource.mute = SoundControl.Instance.IsFXMuted;
        // audioSource.volume = SoundControl.Instance.FXVolume;
        // audioSource.Play();
        UIButtonFX fx = playButton.GetComponent<UIButtonFX>();
        fx.PlayFX(UIButtonFX.ButtonFXType.Click);

        //No esperamos a que finalice ya que ahora llamamos a una corrutina para hacer
        //la transición de la escena a través de SceneLoader.
        //yield return new WaitWhile(()=>audioSource.isPlaying);
        yield return null;
        SceneLoader.Instance.LoadNextLevel("GamePlay");
    }

    IEnumerator DoShowCredits()
    {
        // AudioSource audioSource = creditsButton.GetComponent<AudioSource>();
        // audioSource.mute = SoundControl.Instance.IsFXMuted;
        // audioSource.volume = SoundControl.Instance.FXVolume;
        // audioSource.Play();
        UIButtonFX fx = playButton.GetComponent<UIButtonFX>();
        fx.PlayFX(UIButtonFX.ButtonFXType.Click);

        //No esperamos a que finalice ya que ahora llamamos a una corrutina para hacer
        //la transición de la escena a través de SceneLoader.
        //yield return new WaitWhile(()=>audioSource.isPlaying);
        yield return null;
        SceneLoader.Instance.LoadNextLevel("Credits");
    }

    IEnumerator DoExitGame()
    {
        // AudioSource audioSource = exitButton.GetComponent<AudioSource>();
        // audioSource.mute = SoundControl.Instance.IsFXMuted;
        // audioSource.volume = SoundControl.Instance.FXVolume;
        // audioSource.Play();
        UIButtonFX fx = playButton.GetComponent<UIButtonFX>();
        fx.PlayFX(UIButtonFX.ButtonFXType.Click);        
        yield return new WaitWhile(()=>fx.IsFXPlaying());
        Application.Quit();
    }
}
