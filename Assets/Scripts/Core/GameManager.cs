using UnityEngine;
using System;

public class GameManager : Singleton<GameManager>
{
    public event Action<bool> OnPauseStatusChange;
    public bool Paused { get; private set; }

    /// <summary>
    /// Use pra evitar que o player pause em horas que nao deve.
    /// </summary>
    public bool CanPause = true;

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

    public void PauseResume()
    {
        if (!CanPause)
            return;

        if (Paused)
            PauseGame();
        else
            ResumeGame();
    }
}
