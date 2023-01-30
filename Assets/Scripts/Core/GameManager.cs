using UnityEngine;
using System;

public class GameManager : Singleton<GameManager>
{
    public event Action<bool> OnPauseStatusChange;
    public bool Paused { get; private set; }

    public void PauseGame()
    {
        Time.timeScale = 0;
        OnPauseStatusChange?.Invoke(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        OnPauseStatusChange?.Invoke(false);
    }
}
