using System.Collections;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] GameObject restartButton;
    [SerializeField] GameObject exitButton;


    /*=======================================================================*/
    /*                                 UI Events                             */
    /*=======================================================================*/
    public void RestartClick()
    {
        StartCoroutine(DoRestartGame());
    }

    public void QuitClick()
    {
        StartCoroutine(DoQuitGame());
    }


    /*=======================================================================*/
    /*                             Private Section                           */
    /*=======================================================================*/
    IEnumerator DoRestartGame()
    {
        // AudioSource audioSource = restartButton.GetComponent<AudioSource>();
        // audioSource.mute = SoundControl.Instance.IsFXMuted;
        // audioSource.volume = SoundControl.Instance.FXVolume;
        // audioSource.Play();
        UIButtonFX fx = restartButton.GetComponent<UIButtonFX>();
        fx.PlayFX(UIButtonFX.ButtonFXType.Click);

        //No esperamos a que finalice ya que ahora llamamos a una corrutina para hacer
        //la transición de la escena en el GameControl a través de SceneLoader.
        //yield return new WaitWhile(()=>restartButton.GetComponent<AudioSource>().isPlaying);
        yield return null;
        GameControl.Instance.RestartGame();
    }

    IEnumerator DoQuitGame()
    {
        // AudioSource audioSource = exitButton.GetComponent<AudioSource>();
        // audioSource.mute = SoundControl.Instance.IsFXMuted;
        // audioSource.volume = SoundControl.Instance.FXVolume;
        // audioSource.Play();
        UIButtonFX fx = exitButton.GetComponent<UIButtonFX>();
        fx.PlayFX(UIButtonFX.ButtonFXType.Click);

        //No esperamos a que finalice ya que ahora llamamos a una corrutina para hacer
        //la transición de la escena GameControl a través de SceneLoader.
        //yield return new WaitWhile(()=>exitButton.GetComponent<AudioSource>().isPlaying);
        yield return null;
        GameControl.Instance.GoMainMenu();
    }
}

