using UnityEngine;

public class MinigameSceneManager : MonoBehaviour
{
    public void ReturnToMainWorld()
    {
        FindAnyObjectByType<SceneTransitionManager>().ReturnToPreviousScene();
    }
}