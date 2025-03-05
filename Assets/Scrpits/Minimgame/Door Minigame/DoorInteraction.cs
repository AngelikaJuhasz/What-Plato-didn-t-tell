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

    private void OnMouseDown()
    {
        if (doorChosen) return; // Prevent interaction if a door was already picked

        doorChosen = true; // Lock further choices

        if (doorType == DoorType.Middle)
        {
            resultText.text = "You chose wisely! Socrates gains wisdom.";
            resultText.color = Color.green;
            GameProgressTracker.Instance.IncreaseCorrect();
        }
        else
        {
            resultText.text = "Wrong choice! Socrates loses credibility.";
            resultText.color = Color.red;
            GameProgressTracker.Instance.IncreaseIncorrect();

            resultText.gameObject.SetActive(true); // Show result message
        }
    }
}
