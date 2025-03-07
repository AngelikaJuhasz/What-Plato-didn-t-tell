using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // For updating UI messages

public class DoorInteraction : MonoBehaviour
{
    public enum DoorType { Left, Middle, Right }
    public DoorType doorType;

    public TextMeshProUGUI resultText;
    private static bool doorChosen = false; // Tracks if a door has been picked

    public GameObject gameEndCanvas; // Assign the UI Canvas in Inspector
    public GameObject feedbackText; // The text that gives feedback (inside the canvas)

    private bool gameEnded = false;
    void Start()
    {
        if (gameEndCanvas != null)
            gameEndCanvas.SetActive(false); // Make sure it's hidden at start
    }

    private void OnMouseDown()
    {
        if (doorChosen) return; // Prevent interaction if a door was already picked

        doorChosen = true; // Lock further choices

        if (doorType == DoorType.Middle)
        {
            resultText.text = "You chose wisely! Socrates gains wisdom.";
            resultText.color = Color.green;
            GameProgressTracker.Instance.IncreaseCorrect();
            EndGame(true);
        }
        else
        {
            resultText.text = "Wrong choice! Socrates loses credibility.";
            resultText.color = Color.red;
            GameProgressTracker.Instance.IncreaseIncorrect();
            EndGame(true);

            resultText.gameObject.SetActive(true); // Show result message
        }
    }

    void EndGame(bool won)
    {
        gameEnded = true;

        if (gameEndCanvas != null)
        {
            gameEndCanvas.SetActive(true); // Show the end game canvas
        }

       

        Debug.Log("Game Over!");
    }
}

