using UnityEngine;

public class SoundControl: MonoBehaviour
{
    public static SoundControl Instance = null;

    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
            Instance.IsGlobalMuted = PlayerPrefs.GetInt("globalMute")==1;

            Instance.IsFXMuted = !PlayerPrefs.HasKey("fxMute") ? false : PlayerPrefs.GetInt("fxMute")==1;
            Instance.FXVolume = !PlayerPrefs.HasKey("fxVolume") ? 0.5f : PlayerPrefs.GetFloat("fxVolume");
            Instance.IsMusicMuted = !PlayerPrefs.HasKey("musicMute") ? false : PlayerPrefs.GetInt("musicMute")==1;
            Instance.MusicVolume = !PlayerPrefs.HasKey("musicVolume") ? 0.5f : PlayerPrefs.GetFloat("musicVolume");

            Debug.Log(string.Format("SoundControl\nFXMuted:{0}\nFXVol:{1}\nSndMuted:{2}\nSndVol:{3}",Instance.IsFXMuted, Instance.FXVolume, Instance.IsMusicMuted, Instance.MusicVolume));
        }
        else if( Instance != this)
            Destroy(gameObject);
    }

    public void GlobalMute()
    {
        IsGlobalMuted = !IsGlobalMuted;
        PlayerPrefs.SetInt("globalMute",IsGlobalMuted?1:0);
    }

    public bool IsGlobalMuted {get; private set;}

    public void FXMute()
    {
        IsFXMuted = !IsFXMuted;
        PlayerPrefs.SetInt("fxMute",IsFXMuted?1:0);
    }
    public bool IsFXMuted {get; private set;}

    public void SetFXVolume(float vol)
    {
        FXVolume = vol;
        PlayerPrefs.SetFloat("fxVolume",FXVolume);
    }
    public float FXVolume {get; private set;}


    public void MusicMute()
    {
        IsMusicMuted = !IsMusicMuted;
        PlayerPrefs.SetInt("musicMute",IsMusicMuted?1:0);
    }
    public bool IsMusicMuted {get; private set;}

    public void SetMusicVolume(float vol)
    {
        MusicVolume = vol;
        PlayerPrefs.SetFloat("musicVolume",MusicVolume);
    }
    public float MusicVolume {get; private set;}
}
