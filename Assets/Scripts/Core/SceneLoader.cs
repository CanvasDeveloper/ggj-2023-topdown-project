using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    private readonly int FadeInParam = Animator.StringToHash("FadeIn");
    private readonly int FadeOutParam = Animator.StringToHash("FadeOut");

    [SerializeField] private Animator animator;
    [SerializeField] private float animationFadeTime = 0.3f;

    private bool isLoadingScene;

    public void ReloadScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void LoadScene(int sceneIndex)
    {
        if (isLoadingScene)
            return;

        StartCoroutine(LoadSceneRoutine(sceneIndex));
    }

    private IEnumerator LoadSceneRoutine(int sceneIndex)
    {
        isLoadingScene = true;

        animator.SetTrigger(FadeInParam);

        yield return new WaitForSeconds(animationFadeTime);

        var operation = SceneManager.LoadSceneAsync(sceneIndex);

        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => operation.isDone);
        yield return new WaitForEndOfFrame();

        animator.SetTrigger(FadeOutParam);

        isLoadingScene = false;
    }
}
