using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsView : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button returnButton;
    [SerializeField] private Button mainMenuButton;
    [Header("Sound")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider effectSlider;
    [SerializeField] private Toggle muteToggle;

    private void OnEnable()
    {
        returnButton.onClick.AddListener(ReturnToGame);
        mainMenuButton.onClick.AddListener(LoadMainMenu);

        musicSlider.onValueChanged.AddListener(MusicVolumeChange);
        effectSlider.onValueChanged.AddListener(EffectVolumeChange);
        muteToggle.onValueChanged.AddListener(MuteAllSounds);
    }

    private void Awake()
    {
        musicSlider.value = AudioManager.Instance.GetMusicVolume() * musicSlider.maxValue;
        effectSlider.value = AudioManager.Instance.GetEffectsVolume() * effectSlider.maxValue;
    }

    private void LoadMainMenu()
    {
        var sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (sceneIndex != 0)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            ReturnToGame();
        }
    }
    private void ReturnToGame()
    {
        gameObject.SetActive(false);
        if (Time.timeScale != 0) return;
        Time.timeScale = 1;
    }
    private void MusicVolumeChange(float value)
    {
        AudioManager.Instance.SetMusicVolume(value / musicSlider.maxValue);
    }

    private void EffectVolumeChange(float value)
    {
        AudioManager.Instance.SetEffectsVolume(value / effectSlider.maxValue);
    }
    private void MuteAllSounds(bool volumeState)
    {
        AudioListener.pause = volumeState;
    }
}
