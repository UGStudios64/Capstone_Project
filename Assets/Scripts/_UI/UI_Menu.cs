using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Menu : MonoBehaviour
{
    [SerializeField] private Image fade;
    [SerializeField] private float fadeTime;
    [SerializeField] private bool startWith0Alpha;
    [Space (5)]
    [SerializeField] private GameObject firstButton;


    // GAME //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    private void Awake()
    {
        if (startWith0Alpha) fade.canvasRenderer.SetAlpha(0f);
    }

    void Update()
    {
        if (!EventSystem.current.currentSelectedGameObject)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            if (h != 0 || v != 0) EventSystem.current.SetSelectedGameObject(firstButton);
        }
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

    // EXIT ---------------------------
    public void Exit()
    { Application.Quit(); }
}