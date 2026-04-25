using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private Image fade;
    [SerializeField] private float fadeTime;
    [SerializeField] private bool startWith0Alpha;


    // GAME //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    private void Awake()
    {
        if (startWith0Alpha) fade.canvasRenderer.SetAlpha(0f);
    }


    // FUNCTIONS //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-

    // FADE TRANSITION TO OTHER SCENE
    private void Fade()
    { fade.CrossFadeAlpha(1, fadeTime, true); }

    public void LoadScene(string sceneName)
    {
        Fade();
        StartCoroutine(LoadSceneAfterDelay(sceneName));
    }

    IEnumerator LoadSceneAfterDelay(string sceneName)
    {
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(sceneName);
    }


    // RETRY --------------------
    public void RetryLevel()
    { LoadScene(GameState.currentScene); }


    // EXIT --------------------
    public void Exit()
    { Application.Quit(); }
}