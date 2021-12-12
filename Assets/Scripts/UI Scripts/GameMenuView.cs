using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuView : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] private Text scoreText;
    [SerializeField] private Text bestScoreText;
    [SerializeField] private Text timeInGame;
    [SerializeField] private Text earnAsteroidsCount;
    [SerializeField] private Text newBestScoreMessage;
    [SerializeField] private float congratsDelay = 2f;
    [Header("BoostBar")]
    [SerializeField] private Slider boostSlider;
    [SerializeField] private Text boostReadyText;
    [SerializeField] private float boostReadyValue = 3f;
    [Header("Settings")]
    [SerializeField] private CanvasGroup settingsPanel;
    [SerializeField] private Button openSettingsButton;
    [Header("LoseGame")]
    [SerializeField] private CanvasGroup loseGamePanel;
    [SerializeField] private float openPanelDelay = 1f;

    private void OnEnable()
	{
        LevelController.OnScoreChanged += ChangeScore;
        LevelController.OnBestScoreChanged += ChangeBestScore;
        LevelController.OnTimeInGameChanged += ChangeTime;
        LevelController.OnAsteroidsCountChanged += ChangeAsteroidsCount;

        openSettingsButton.onClick.AddListener(OpenSettingsPanel);

        SpaceshipController.OnDie += OpenLoseGamePanel;
        SpaceshipController.OnBoost += ChangeBoostSlider;
        boostSlider.maxValue = boostReadyValue;
    }
	private void OnDisable()
	{
        LevelController.OnScoreChanged -= ChangeScore;
        LevelController.OnBestScoreChanged -= ChangeBestScore;
        LevelController.OnTimeInGameChanged -= ChangeTime;
        LevelController.OnAsteroidsCountChanged -= ChangeAsteroidsCount;
        SpaceshipController.OnDie -= OpenLoseGamePanel;
        SpaceshipController.OnBoost -= ChangeBoostSlider;
    }
	private void Awake()
	{
        SetStartBestScore();
	}
	private void ChangeScore(int value)
    {
        scoreText.text = $"SCORE : {value }";
    }

    private void ChangeBestScore(int value)
    {
        bestScoreText.text = $"BEST SCORE : {value}";
        ShowCongratsText();
    }
    private void ShowCongratsText()
    {
        StartCoroutine(ShowCongrats(congratsDelay));
    }
    private IEnumerator ShowCongrats(float delay)
    {
        ActivateBestSCoreMessage(true);
        yield return new WaitForSeconds(delay);
        ActivateBestSCoreMessage(false);
    }
    private void ActivateBestSCoreMessage(bool state)
    {
        newBestScoreMessage.enabled = state;
    }
    private void ChangeBoostSlider()
    {
        boostSlider.value = SpaceshipController.BoostFilling;
        boostReadyText.enabled = boostSlider.value >= boostReadyValue;
    }
    private void ChangeTime(float value)
    {
        timeInGame.text = $"TIME : {value:F2} s.";
    }
    private void ChangeAsteroidsCount(int value)
    {
        earnAsteroidsCount.text = $"ASTEROIDS : {value}";
    }
    private void SetStartBestScore()
    {
        var lastBestScore = PlayerPrefs.GetInt("BestScore", 0);
        bestScoreText.text = $"BEST SCORE : {lastBestScore}";
    }
    private void OpenSettingsPanel()
    {
        settingsPanel.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    private void OpenLoseGamePanel()
    {
        StartCoroutine(OpenPanelWithDelay(openPanelDelay));
    }

    private IEnumerator OpenPanelWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        loseGamePanel.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}
