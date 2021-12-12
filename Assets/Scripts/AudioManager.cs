using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Singleton
    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            musicPlayer.volume = PlayerPrefs.GetFloat(PrefsMusicVolume, 0.5f);
            effectsPlayer.volume = PlayerPrefs.GetFloat(PrefsMusicVolume, 0.5f);
        }
    }
    #endregion

    [SerializeField] private AudioSource musicPlayer;
    [SerializeField] private AudioSource effectsPlayer;

    private const string PrefsMusicVolume = "Volume";
    private const string PrefsEffectsVolume = "Effects";

    public void PlayEffect(AudioClip effect)
    {
        effectsPlayer.PlayOneShot(effect);
    }
    public void SetMusicVolume(float volume)
    {
        musicPlayer.volume = volume;
        PlayerPrefs.SetFloat(PrefsMusicVolume, volume);
    }

    public void SetEffectsVolume(float volume)
    {
        effectsPlayer.volume = volume;
        PlayerPrefs.SetFloat(PrefsEffectsVolume, volume);
    }
    public float GetMusicVolume()
    {
        return musicPlayer.volume;
    }

    public float GetEffectsVolume()
    {
        return effectsPlayer.volume;
    }
}
