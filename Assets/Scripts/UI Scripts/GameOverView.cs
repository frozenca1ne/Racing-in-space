using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverView : MonoBehaviour
{
    [SerializeField] private LevelController levelController;
    [SerializeField] private Text finalScore;
    [SerializeField] private Text finalAsteroids;
    [SerializeField] private Text finalTotalTime;
    [SerializeField] private Text newRecordText;

    [SerializeField] private Button restartLevel;

    private void Awake()
    {
        SetFinalScore();
    }

    private void OnEnable()
    {
        restartLevel.onClick.AddListener(RestartLevel);
    }

    private void SetFinalScore()
    {
        finalScore.text = $"FINAL SCORE : {levelController.CurrentScore}";
        finalAsteroids.text = $"ASTEROIDS : {levelController.EarnAsteroidsCount}";
        finalTotalTime.text = $"TOTAL TIME : {levelController.TimeInGame: F2}";

        var lastBestScore = PlayerPrefs.GetInt("BestScore", 0);
        if (levelController.CurrentScore <= lastBestScore) return;
        newRecordText.enabled = true;
    }

    private void RestartLevel()
    {
        var levelIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(levelIndex);
        Time.timeScale = 1;
    }
}
