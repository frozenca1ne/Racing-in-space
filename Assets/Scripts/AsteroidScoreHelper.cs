using System;

[Serializable]
public class AsteroidScoreHelper
{
    public static event Action<int> OnAsteroidsAdd;
    public static event Action<int> OnAsteroidsPointsAdd;

    private int currentAsteroidsEarn;
    private int currentPointsPerAsteroid;

    public int CurrentAsteroidsEarn
    {
        get => currentAsteroidsEarn;
        set
        {
            currentAsteroidsEarn = value;
            OnAsteroidsAdd?.Invoke(currentAsteroidsEarn);
        }
    }

    public int CurrentPointsPerAsteroid
    {
        get => currentPointsPerAsteroid;
        set
        {
            currentPointsPerAsteroid = value;
            OnAsteroidsPointsAdd?.Invoke(currentPointsPerAsteroid);
        }
    }
}
