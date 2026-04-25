using UnityEngine;
using UnityEngine.UI;

public class UI_LevelButton : MonoBehaviour
{
    [SerializeField] private SceneTransition sceneTransition;
    [SerializeField] private Button button;

    [Header("// LEVEL INFO -----------------------------------------------------------------------------------------")]
    [SerializeField] private int levelIndex;
    [SerializeField] private string sceneName;


    // GAME //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    void Start()
    {
        SaveData data = SaveSystem.Load();
        // MAKE THE BUTTON INTERACTABLE IF THE PREVIUOS IS COMPLETE
        button.interactable = levelIndex <= data.unlockedLevel;
    }


    // FUNCTIONS //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    public void LoadLevel()
    {
        GameState.currentLevel = levelIndex;
        GameState.currentScene = sceneName;

        sceneTransition.LoadScene(sceneName);
    }
}