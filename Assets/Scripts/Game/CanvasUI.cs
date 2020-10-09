using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CanvasUI : MonoBehaviour
{
    [SerializeField] GameObject soundButton;
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject upButton;
    [SerializeField] GameObject downButton;
    [SerializeField] GameObject pauseText;
    [SerializeField] Text highScoreText;
    [SerializeField] Text scoreText;
    [SerializeField] GameObject soundConfigUI;
    [SerializeField] GameObject gameOverUI;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        pauseText.SetActive(false);
        soundConfigUI.SetActive(false);
        gameOverUI.SetActive(false);

        //Según el dispositivo aparecerán o no los botones.
        #if MOBILE_INPUT
        upButton.SetActive(true);
        downButton.SetActive(true);
        #else
        upButton.SetActive(false);
        downButton.SetActive(false);
        #endif

        //Esperamos a que SoundControl se inicialice.
        yield return new WaitWhile(()=>SoundControl.Instance==null);
        //Esperamos a que GameControl se inicialice.
        yield return new WaitWhile(()=>GameControl.Instance==null);
    }

    void Update()
    {
        //Puntuaciones.
        highScoreText.text = string.Format("High Score: {0}", GameControl.Instance.HighScore);
        scoreText.text = string.Format("Score: {0}", GameControl.Instance.Score);

        if(GameControl.Instance.IsGameOver)
        {
            pauseButton.GetComponent<Button>().interactable = false;
            soundButton.GetComponent<Button>().interactable = false;
            gameOverUI.SetActive(true);
        }
    }


    /*=======================================================================*/
    /*                                 UI Events                             */
    /*=======================================================================*/
    public void SoundClick()
    {
        PlayFxFromButton(soundButton);

        soundConfigUI.SetActive(true);
        GameControl.Instance.PauseResumeGame();
    }

    public void PauseClick()
    {
        PlayFxFromButton(pauseButton);

        GameControl.Instance.PauseResumeGame();

        soundButton.GetComponent<Button>().interactable = !GameControl.gameStopped;

        string iconName = GameControl.gameStopped ? "Sprites/Icon_03" : "Sprites/Icon_02";
        pauseButton.GetComponentsInChildren<Image>()[1].sprite = Resources.Load<Sprite>(iconName);
        pauseText.SetActive(GameControl.gameStopped);
    }


    /*=======================================================================*/
    /*                             Private Section                           */
    /*=======================================================================*/
    void PlayFxFromButton(GameObject button)
    {
        AudioSource audioSource = button.GetComponent<AudioSource>();
        audioSource.mute = SoundControl.Instance.IsFXMuted;
        audioSource.volume = SoundControl.Instance.FXVolume;
        audioSource.Play();
    }
}
