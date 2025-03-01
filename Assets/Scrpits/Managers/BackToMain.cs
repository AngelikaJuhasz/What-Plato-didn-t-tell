using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMainScene : MonoBehaviour
{
    [SerializeField] private string mainSceneName = "MainScene"; // Set your main scene name in the Inspector

    public void ReturnToMain()
    {
        SceneManager.LoadScene(mainSceneName, LoadSceneMode.Single);
    }
}