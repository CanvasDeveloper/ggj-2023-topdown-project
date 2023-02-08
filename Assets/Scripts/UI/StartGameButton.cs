using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameButton : MonoBehaviour
{
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(LaunchGame);
    }

    private void LaunchGame()
    {
        SceneLoader.Instance.NextScene();
    }
}
