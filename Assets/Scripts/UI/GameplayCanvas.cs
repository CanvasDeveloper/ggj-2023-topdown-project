using UnityEngine;
using UnityEngine.UI;

public class GameplayCanvas : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button backgroundButton;

    private void Start()
    {
        pausePanel.SetActive(false);

        if(backgroundButton)
            backgroundButton.onClick.AddListener(HandlePauseUIButton);
    }

    private void OnEnable()
    {
        GameManager.Instance.OnPauseStatusChange += HandlePauseUI;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnPauseStatusChange -= HandlePauseUI;
    }

    private void HandlePauseUIButton()
    {
        GameManager.Instance.PauseResume();
    }

    private void HandlePauseUI(bool value)
    {
        pausePanel.SetActive(value);

        if (value && backgroundButton)
            backgroundButton.Select();
    }
}
