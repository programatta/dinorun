using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonFX : MonoBehaviour
{
    public enum ButtonFXType {Click,Over};

    [SerializeField] AudioSource AudioButtonClick;
    [SerializeField] AudioSource AudioButtonOver;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*=======================================================================*/
    /*                                 UI Events                             */
    /*=======================================================================*/
    public void PointerEnter()
    {   
        PlayFX(UIButtonFX.ButtonFXType.Over);
    }
 
    public void PointerExit()
    {
    }

    public void PlayFX(ButtonFXType fXType)
    {
        switch(fXType)
        {
            case ButtonFXType.Click:
                AudioButtonClick.mute = SoundControl.Instance.IsFXMuted;
                AudioButtonClick.volume = SoundControl.Instance.FXVolume;
                AudioButtonClick.Play();
                break;

            case ButtonFXType.Over:
                AudioButtonOver.mute = SoundControl.Instance.IsFXMuted;
                AudioButtonOver.volume = SoundControl.Instance.FXVolume;
                AudioButtonOver.Play();
                break;
        }
    }

    public bool IsFXPlaying()
    {
        return AudioButtonClick.isPlaying || AudioButtonOver.isPlaying;
    }
}
