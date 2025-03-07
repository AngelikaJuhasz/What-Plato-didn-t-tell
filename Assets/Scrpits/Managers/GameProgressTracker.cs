using UnityEngine;
using UnityEngine.SceneManagement;

public class GameProgressTracker : MonoBehaviour
{
    public static GameProgressTracker Instance { get; private set; }

    // Tracks Correct, Incorrect, and Humorous choices
    private int correctCount = 0;
    private int incorrectCount = 0;
    private int humorousCount = 0;

    // Set the threshold score at which the scene will change
    private int scoreThreshold = 3;
    private bool hasTriggeredEndScene = false;

    private void Update()
    {
        if (hasTriggeredEndScene == false)
        CheckTotalScoreAndTriggerSceneChange();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keeps it active across scenes
        }
        else
        {
            Destroy(gameObject); // Ensures only one instance exists
        }
    }

    // Methods for Correct, Incorrect, and Humorous choices
    public int GetCorrect() => correctCount;
    public void IncreaseCorrect()
    {
        correctCount++;
        Debug.Log($"Correct Increased: {correctCount}"); // Debug log

        // Check if the scene should be triggered
        CheckTotalScoreAndTriggerSceneChange();
    }

    public int GetIncorrect() => incorrectCount;
    public void IncreaseIncorrect()
    {
        incorrectCount++;
        Debug.Log($"Incorrect Increased: {incorrectCount}"); // Debug log

        // Check if the scene should be triggered
        CheckTotalScoreAndTriggerSceneChange();
    }

    public int GetHumorous() => humorousCount;
    public void IncreaseHumorous()
    {
        humorousCount++;
        Debug.Log($"Humorous Increased: {humorousCount}"); // Debug log

        // Check if the scene should be triggered
        CheckTotalScoreAndTriggerSceneChange();
    }

    // Optionally, reset all values (useful for restarting or starting new game)
    public void ResetAllValues()
    {
        correctCount = 0;
        incorrectCount = 0;
        humorousCount = 0;
    }

    // You can also provide setters if you want to set specific values externally
    public void SetCorrect(int value) => correctCount = value;
    public void SetIncorrect(int value) => incorrectCount = value;
    public void SetHumorous(int value) => humorousCount = value;

    // Method to check if total score reaches the threshold
    public void CheckTotalScoreAndTriggerSceneChange()
    {
        // Only check if the scene has not been triggered
        if (hasTriggeredEndScene)
            return;

        int totalScore = correctCount + incorrectCount + humorousCount;
        Debug.Log($"Current Score: Correct: {correctCount}, Incorrect: {incorrectCount}, Humorous: {humorousCount}, Total: {totalScore}");

        if (totalScore >= scoreThreshold)
        {
            hasTriggeredEndScene = true;  // Set the flag to prevent triggering again
            TriggerEndScene();           // Trigger the scene change
        }
    }

    // Trigger the scene change
    private void TriggerEndScene()
    {
        Debug.Log("Score threshold reached! Loading new scene...");

        // Replace with the actual scene name you'd like to load
        SceneManager.LoadScene("End");  // Ensure "End" is the correct scene name in Build Settings
    }
}
