using System.Collections;
using System.Collections.Generic;
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

	private void OnEnable()
	{
        LevelController.OnScoreChanged += ChangeScore;
        LevelController.OnBestScoreChanged += ChangeBestScore;

        SpaceshipController.OnBoost += ChangeBoostSlider;
        boostSlider.maxValue = boostReadyValue;
    }
	private void OnDisable()
	{
        LevelController.OnScoreChanged -= ChangeScore;
        LevelController.OnBestScoreChanged -= ChangeBestScore;
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
}
