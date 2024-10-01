using UnityEngine;
using System;
using CrazyGames;

public class GameManager : Singleton<GameManager>
{
    public event Action<bool> OnPauseStatusChange;
    public event Action OnDead;
    public event Action OnPowerUP;
    public event Action Next;
    public event Action OnGameWin;
    public bool Paused { get; private set; }

    private bool isPowerUp;

    public Lean.Localization.LeanLocalization leanLocalization;

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
        SetCrazyGamePlaying(false);
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

    public void ResumeGame()
    {
        Paused = false;
        Time.timeScale = 1;
        OnPauseStatusChange?.Invoke(Paused);
    }

    public void GameOver()
    {
        Paused = true;
        Time.timeScale = 0;

        if(CrazySDK.IsInitialized)
        {
            CrazySDK.Ad.RequestAd(CrazyAdType.Midgame, () => { }, (error) => { }, null);
        }

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
        {
            SetCrazyGamePlaying(true);
            ResumeGame();
        }
        else
        {
            SetCrazyGamePlaying(false);
            PauseGame();
        }
    }

    public void GameWin()
    {
        Time.timeScale = 0;
        OnGameWin?.Invoke();

        if (CrazySDK.IsInitialized)
        {
            CrazySDK.Game.HappyTime();
            CrazySDK.Ad.RequestAd(CrazyAdType.Midgame, () => { }, (error) => { }, null);
        }

        SetCrazyGamePlaying(false);
    }

    public void SetCrazyGamePlaying(bool playing)
    {
        if (CrazySDK.IsInitialized)
            return;

        if(playing)
        {
            CrazySDK.Game.GameplayStart();
            Debug.Log("Game Started");
        }
        else
        {
            CrazySDK.Game.GameplayStop();
            Debug.Log("Game Stopped");
        }
    }
}
