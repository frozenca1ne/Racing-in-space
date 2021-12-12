using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private CanvasGroup settingsPanel;
    [SerializeField] private Text bestScore;

    private void OnEnable()
    {
        playButton.onClick.AddListener(LoadGameScene);
        exitButton.onClick.AddListener(ExitGame);
        settingsButton.onClick.AddListener(OpenSettingsPanel);
    }

    private void Start()
    {
        SetStartBestScore();
    }

    private void LoadGameScene()
    {
        var sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex + 1);
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    private void OpenSettingsPanel()
    {
        settingsPanel.gameObject.SetActive(true);
    }
    private void SetStartBestScore()
    {
        var currentBestScore = PlayerPrefs.GetInt("BestScore", 0);
        bestScore.text = "BEST SCORE : " + currentBestScore;
    }
}
