using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
	public static event Action<int> OnScoreChanged;
    public static event Action<int> OnBestScoreChanged;

    [SerializeField] private int currentScore = 0;
    [SerializeField] private int currentBestScore = 0;
    [SerializeField] private int earnAsteroidsCount = 0;
    [SerializeField] private float timeInGame = 0;

    private bool doublePoints;
    private float scoreTimer;

	private void Start()
	{
        doublePoints = false;
        ResetScores();
	}
	private void Update()
	{
        SetPointsToScore();
	}
	private void SetPointsToScore()
    {
        scoreTimer += 1 * Time.deltaTime;
        if (!(scoreTimer >= 1)) return;
      
        if (doublePoints == false)
        {
            AddPointsToScore(1);
            scoreTimer = 0;
        }
        else if (doublePoints)
        {
            AddPointsToScore(2);
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
        PlayerPrefs.SetInt("BestScore", currentBestScore);
        OnBestScoreChanged?.Invoke(currentScore);
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
