using UnityEngine;
using System;

public class BeforeTheStartView : MonoBehaviour
{
    public static event Action<bool> OnGameStart;
    private void Start()
    {
        Time.timeScale = 0;
    }

    private void Update()
    {
        StartGame();
    }

    private void StartGame()
    {
        if (!Input.anyKey) return;
        gameObject.SetActive(false);
        Time.timeScale = 1;
        OnGameStart?.Invoke(true);
    }
}
