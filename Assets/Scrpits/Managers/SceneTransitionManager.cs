using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    private string lastScene; // Store the last scene before switching

    public void LoadScene(string sceneName)
    {
        lastScene = SceneManager.GetActiveScene().name; // Store the current scene
        SceneManager.LoadScene(sceneName);
    }

    public void ReturnToPreviousScene()
    {
        if (!string.IsNullOrEmpty(lastScene))
        {
            SceneManager.LoadScene(lastScene);
        }
        else
        {
            Debug.LogWarning("No previous scene found!");
        }
    }
}
