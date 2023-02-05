using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayCanvas : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private GameObject hudIngame;
    [SerializeField] private GameObject hudPowerUP;
    [SerializeField] private Button backgroundButton;
    [SerializeField] private Button tryAgainButton;
    [SerializeField] private Button quitToMenuButton;

    public GameObject painelChoiceCards;


    /// <summary>
    /// Interface do sistema de vida
    /// </summary>
    private IDamageable playerHealthSystem;

    private void Start()
    {
        DisableAll();

        backgroundButton.onClick.AddListener(HandlePauseUIButton);
        tryAgainButton.onClick.AddListener(TryAgain);
        quitToMenuButton.onClick.AddListener(QuitToMenu);

        GameManager.Instance.OnPauseStatusChange += HandlePauseUI;
        GameManager.Instance.OnDead += OpenDeathScreen;
        GameManager.Instance.OnPowerUP += PowerUpUI;
        GameManager.Instance.Next += NextWave;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnPauseStatusChange -= HandlePauseUI;
        GameManager.Instance.OnDead -= OpenDeathScreen;
        GameManager.Instance.OnPowerUP -= PowerUpUI;
        GameManager.Instance.Next -= NextWave;
    }

    private void HandlePauseUIButton()
    {
        GameManager.Instance.PauseResume();
    }

    public void TryAgain()
    {
        SceneLoader.Instance.ReloadScene();
    }

    public void QuitToMenu()
    {
        SceneLoader.Instance.LoadTitle();
    }

    /// <summary>
    /// Desabilita ou habilita o painel de pause de acordo com o valor
    /// </summary>
    /// <param name="value">Estado de pausa</param>
    private void HandlePauseUI(bool value)
    {
        pausePanel.SetActive(value);

        if (value && backgroundButton)
            backgroundButton.Select();
    }

    /// <summary>
    /// Atualiza a barra de vida do player na UI
    /// </summary>
    /// <param name="current">Valor atual</param>
    /// <param name="max">Valor maximo</param>
    private void UpdateHealthBar(float current, float max)
    {
        var percent = current / max;
    }

    private void OpenDeathScreen()
    {
        OpenDeathScreen(null);
    }

    /// <summary>
    /// Abre o painel de morte
    /// </summary>
    private void OpenDeathScreen(IDamageable instance)
    {
        DisableAll();
        hudIngame.SetActive(false);
        deathPanel.SetActive(true);
    }

    public void PowerUpUI()
    {
        Time.timeScale = 0;
        PowerUpController.Instance.m_player.playerLevel += 1;

        hudPowerUP.SetActive(true);
        List<GameObject> newListHud = new List<GameObject>();
        List<int> usedIndices = new List<int>();
       
        while (newListHud.Count < 3)
        {
            int randomIndex = Random.Range(0, PowerUpController.Instance.cardList.Count);
            if (!usedIndices.Contains(randomIndex))
            {
                newListHud.Add(PowerUpController.Instance.cardList[randomIndex]);
                usedIndices.Add(randomIndex);

            }

        }

        foreach (GameObject go in newListHud)
        {
            Instantiate(go, painelChoiceCards.transform);
        }



    }
    public void NextWave()
    {
        Time.timeScale = 1;
        hudPowerUP.SetActive(false);
        foreach (Transform child in painelChoiceCards.transform)
        {
            Destroy(child.gameObject);
        }
        WaveSpawner.Instance.isEndWave = false;
        WaveSpawner.Instance.currWave++;
        TreeController.Instance.xpBarCurrent = 0;
        if(PowerUpController.Instance.m_player.playerLevel <= 20)
        {
            TreeController.Instance.xpBarMax += 10;
        }
        else if(PowerUpController.Instance.m_player.playerLevel <= 40)
        {
            TreeController.Instance.xpBarMax += 13;
        }
        else if (PowerUpController.Instance.m_player.playerLevel > 41)
        {
            TreeController.Instance.xpBarMax += 16;
        }
       
        WaveSpawner.Instance.GenerateWave();
      
    }

    /// <summary>
    /// Desativa todos os paineis
    /// </summary>
    private void DisableAll()
    {
        pausePanel.SetActive(false);
        deathPanel.SetActive(false);
      
    }
}
