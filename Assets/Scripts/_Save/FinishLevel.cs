using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    [SerializeField] private SceneTransition sceneTransition;

    public void OnLevelComplete()
    {
        SaveData data = SaveSystem.Load();
        int currentLevel = GameState.currentLevel;

        if (currentLevel >= data.unlockedLevel)
        {
            data.unlockedLevel = currentLevel + 1;
            SaveSystem.Save(data);
        }

        Debug.Log("LEVEL COMPLETE AND SAVED");
        sceneTransition.LoadScene("CompleteLevel");
    }
}