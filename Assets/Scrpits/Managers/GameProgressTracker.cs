using UnityEngine;

public class GameProgressTracker : MonoBehaviour
{
    public static GameProgressTracker Instance { get; private set; }
    private int score = 0; // Tracks total points

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

    public void AddScore(int points)
    {
        score += points;
        Debug.Log($"Current Score: {score}");
    }

    public int GetScore()
    {
        return score;
    }
}
