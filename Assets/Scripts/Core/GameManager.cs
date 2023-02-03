using UnityEngine;
using System;

public class GameManager : Singleton<GameManager>
{
    public event Action<bool> OnPauseStatusChange;
    public event Action OnDead;
    public event Action OnPowerUP;
    public event Action Next;
    public bool Paused { get; private set; }

    private bool isPowerUp;

    public Lean.Localization.LeanLocalization leanLocalization;

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    /// <summary>
    /// Use pra evitar que o player pause em horas que nao deve.
    /// </summary>
    public bool CanPause = true;

    private void PauseGame()
    {
        Paused = true;
        Time.timeScale = 0;
        OnPauseStatusChange?.Invoke(Paused);
    }

    private void ResumeGame()
    {
        Paused = false;
        Time.timeScale = 1;
        OnPauseStatusChange?.Invoke(Paused);
    }

    public void GameOver()
    {
        Paused = true;
        Time.timeScale = 0;
        OnPauseStatusChange?.Invoke(Paused);
        OnDead?.Invoke();
    }

    public void PowerUP()
    {
        if (!isPowerUp)
        {
            isPowerUp = true;
            OnPowerUP?.Invoke();
        }
   
    }

    public void Wave()
    {
        isPowerUp = false;
        Next?.Invoke();
    }

    public void PauseResume()
    {
        if (!CanPause)
            return;

        if (Paused)
            ResumeGame();
        else
            PauseGame();
    }
}
