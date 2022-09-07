using System;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public event Action OnGameOver;
    public event Action OnGameStart;

    public void GameOver()
    {
        OnGameOver?.Invoke();
    }

    public void StartGame()
    {
        OnGameStart?.Invoke();
    }
}
