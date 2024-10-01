using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using CrazyGames;

public class SceneLoader : Singleton<SceneLoader>
{
    public event Action OnSceneStartLoading;

    private readonly int FadeInParam = Animator.StringToHash("FadeIn");
    private readonly int FadeOutParam = Animator.StringToHash("FadeOut");

    [SerializeField] private GameObject fadeCanvas;
    [SerializeField] private Animator animator;
    [SerializeField] private float animationFadeTime = 0.3f;

    private bool isLoadingScene;

    private void Start()
    {
        fadeCanvas.SetActive(true);

        CrazySDK.Init(() =>
        {
            LoadTitle();
        });
    }

    public void ReloadScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadTitle()
    {
        LoadScene(1);
    }

    private void LoadScene(int sceneIndex)
    {
        if (isLoadingScene)
            return;

        GameManager.Instance.SetCrazyGamePlaying(false);

        StartCoroutine(LoadSceneRoutine(sceneIndex));
    }

    private IEnumerator LoadSceneRoutine(int sceneIndex)
    {
        isLoadingScene = true;

        OnSceneStartLoading?.Invoke();

        animator.SetTrigger(FadeInParam);

        yield return new WaitForSecondsRealtime(animationFadeTime);

        GameManager.Instance.ResumeGame();
        var operation = SceneManager.LoadSceneAsync(sceneIndex);

        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => operation.isDone);
        yield return new WaitForEndOfFrame();

 
        animator.SetTrigger(FadeOutParam);

        isLoadingScene = false;
    }
}
