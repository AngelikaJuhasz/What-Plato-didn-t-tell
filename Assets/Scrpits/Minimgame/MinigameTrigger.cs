using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameTrigger : MonoBehaviour
{
    public string minigameSceneName;

    public void StartMinigame()
    {
        SceneManager.LoadScene(minigameSceneName, LoadSceneMode.Additive);
    }
}

