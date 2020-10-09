using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SoundConfigUI : MonoBehaviour
{
    [SerializeField] GameObject fxSoundsToggle;
    [SerializeField] GameObject fxVolumeSlider;
    [SerializeField] GameObject musicToggle;
    [SerializeField] GameObject musicVolumeSlider;
    [SerializeField] GameObject closeButton;

    void OnEnable()
    {
        mCanUseToggle = false;
        fxSoundsToggle.GetComponent<Toggle>().isOn = !SoundControl.Instance.IsFXMuted;
        fxVolumeSlider.GetComponent<Slider>().value = SoundControl.Instance.FXVolume;
        musicToggle.GetComponent<Toggle>().isOn = !SoundControl.Instance.IsMusicMuted;
        musicVolumeSlider.GetComponent<Slider>().value = SoundControl.Instance.MusicVolume;
        mCanUseToggle = true;
    }


    /*=======================================================================*/
    /*                                 UI Events                             */
    /*=======================================================================*/
    public void FXSoundChanged()
    {
        PlayFxFromToggle(fxSoundsToggle);
        if(mCanUseToggle)
            SoundControl.Instance.FXMute();
    }

    public void FXVolumeChanged()
    {
        float value = fxVolumeSlider.GetComponent<Slider>().value;
        SoundControl.Instance.SetFXVolume(value);
    }

    public void MusicChanged()
    {
        PlayFxFromToggle(musicToggle);
        if(mCanUseToggle)
            SoundControl.Instance.MusicMute();
    }

    public void MusicVolumeChanged()
    {
        float value = musicVolumeSlider.GetComponent<Slider>().value;
        SoundControl.Instance.SetMusicVolume(value);
    }

    public void CloseClick()
    {
        StartCoroutine(DoCloseDialog());
    }


    /*=======================================================================*/
    /*                             Private Section                           */
    /*=======================================================================*/
    void PlayFxFromToggle(GameObject toggle)
    {
        AudioSource audioSource = toggle.GetComponent<AudioSource>();
        audioSource.mute = SoundControl.Instance.IsFXMuted;
        audioSource.volume = SoundControl.Instance.FXVolume;
        audioSource.Play();
    }

    IEnumerator DoCloseDialog()
    {
        AudioSource audioSource =closeButton.GetComponent<AudioSource>();
        audioSource.mute = SoundControl.Instance.IsFXMuted;
        audioSource.volume = SoundControl.Instance.FXVolume;
        audioSource.Play();
        yield return new WaitWhile(()=>audioSource.isPlaying);
        GameControl.Instance.PauseResumeGame();
        gameObject.SetActive(false);
    }

    bool mCanUseToggle;
}
