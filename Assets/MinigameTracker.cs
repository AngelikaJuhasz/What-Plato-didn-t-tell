using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameTracker : MonoBehaviour
{
    public static MinigameTracker Instance { get; private set; }

    private int completedMinigames = 0;
    private int totalMinigames = 5; // Adjust based on your game

    [SerializeField] private float transitionDelay = 2.0f; // Delay in seconds

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep tracker between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void MarkMinigameComplete()
    {
        completedMinigames++;
        Debug.Log($"Minigame completed! {completedMinigames}/{totalMinigames}");

        if (completedMinigames >= totalMinigames)
        {
            Debug.Log("All minigames completed! Transitioning to final scene...");
            Invoke("LoadFinalScene", transitionDelay); // Add delay before transition
        }
    }

    private void LoadFinalScene()
    {
        SceneManager.LoadScene("End"); // Change this to your actual final scene name
    }
}
