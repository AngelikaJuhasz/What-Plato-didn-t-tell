using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameTrigger : MonoBehaviour
{
    public string minigameSceneName; // Name of the minigame scene to load
    private bool playerInRange = false;
    public bool useSceneTransitionManager = true; // Toggle to use our SceneTransitionManager if available

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (useSceneTransitionManager)
            {
                SceneTransitionManager stm = FindAnyObjectByType<SceneTransitionManager>();
                if (stm != null)
                {
                    stm.LoadScene(minigameSceneName);
                }
                else
                {
                    Debug.LogWarning("SceneTransitionManager not found! Loading scene directly.");
                    SceneManager.LoadScene(minigameSceneName, LoadSceneMode.Single);
                }
            }
            else
            {
                SceneManager.LoadScene(minigameSceneName, LoadSceneMode.Single);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Press E to start the minigame.");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
