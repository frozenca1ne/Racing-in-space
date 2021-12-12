using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
	public static event Action<int> OnScoreChanged;
    public static event Action<int> OnBestScoreChanged;
    public static event Action<float> OnTimeInGameChanged;
    public static event Action<int> OnAsteroidsCountChanged;

    [SerializeField] private int currentScore = 0;
    [SerializeField] private int currentBestScore = 0;
    [SerializeField] private int earnAsteroidsCount = 0;
    [SerializeField] private float timeInGame = 0;
    [SerializeField] private int normalPoints = 1;
    [SerializeField] private int boostedPoints = 2;

	public  int CurrentScore => currentScore;

	public  int EarnAsteroidsCount => earnAsteroidsCount;
    public  float TimeInGame => timeInGame;
    public int CurrentBestScore => currentBestScore;

    private bool doublePoints;
    private float scoreTimer;

    private void OnEnable()
    {
        AsteroidScoreHelper.OnAsteroidsAdd += AddAsteroidsCount;
        AsteroidScoreHelper.OnAsteroidsPointsAdd += AddPointsToScore;
    }

    private void OnDisable()
    {
        AsteroidScoreHelper.OnAsteroidsAdd -= AddAsteroidsCount;
        AsteroidScoreHelper.OnAsteroidsPointsAdd -= AddPointsToScore;
    }

    private void Start()
	{
        doublePoints = false;
        ResetScores();
	}
	private void Update()
	{
        SetPointsToScore();
        SetTimeInGame();
	}
	private void SetPointsToScore()
    {
        scoreTimer += 1 * Time.deltaTime;
        if (!(scoreTimer >= 1)) return;
      
        if (doublePoints == false)
        {
            AddPointsToScore(normalPoints);
            scoreTimer = 0;
        }
        else if (doublePoints)
        {
            AddPointsToScore(boostedPoints);
            scoreTimer = 0;
        }
    }
    public void AddPointsToScore(int value)
    {
        currentScore += value;
        OnScoreChanged?.Invoke(currentScore);
        CheckNewRecord();
    }
    private void CheckNewRecord()
    {
        var lastBestScore = PlayerPrefs.GetInt("BestScore", 0);
        if (currentScore <= lastBestScore) return;
        currentBestScore = currentScore;
        PlayerPrefs.SetInt("BestScore", currentBestScore);
        OnBestScoreChanged?.Invoke(currentScore);
    }
    private void SetTimeInGame()
    {
        //adds 1 point every second
        timeInGame += 1 * Time.deltaTime;
        OnTimeInGameChanged?.Invoke(timeInGame);
    }
    public void AddAsteroidsCount(int value)
    {
        earnAsteroidsCount += value;
        OnAsteroidsCountChanged?.Invoke(earnAsteroidsCount);
    }
    private void ResetScores()
    {
        StartCoroutine(ResetScoreWithDelay());
    }

    private IEnumerator ResetScoreWithDelay()
    {
        yield return new WaitForSeconds(0.3f);
        currentScore = 0;
        currentBestScore = 0;
        earnAsteroidsCount = 0;
        timeInGame = 0;
    }

}
