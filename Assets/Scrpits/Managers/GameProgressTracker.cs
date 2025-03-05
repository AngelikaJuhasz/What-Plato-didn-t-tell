using UnityEngine;

public class GameProgressTracker : MonoBehaviour
{
    public static GameProgressTracker Instance { get; private set; }


    // Tracks Correct, Incorrect, and Humorous choices
    private int correctCount = 0;
    private int incorrectCount = 0;
    private int humorousCount = 0;

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
    }

    public int GetIncorrect() => incorrectCount;
    public void IncreaseIncorrect()
    {
        incorrectCount++;
    }

    public int GetHumorous() => humorousCount;
    public void IncreaseHumorous()
    {
        humorousCount++;
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
}
