using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PriestessDialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel; // The UI panel holding the text
    [SerializeField] private TextMeshProUGUI dialogueText; // The text UI element
    [SerializeField] private Button dialogueButton; // Button to progress dialogue

    private string[] priestessStatements =
    {
        "You must choose wisely, traveler. One of us speaks the truth, the others deceive.\n",  // First message at scene start
        "Priest A: 'Priest B always speaks the truth.'\n",
        "Priest B: 'The correct door is the one on the left.'\n",
        "Priest C: 'Priest A is lying.'"
    };

    private int currentStatementIndex = 0;
    private bool dialogueComplete = false; // Prevents extra clicks after last dialogue

    void Start()
    {
        dialoguePanel.SetActive(true);
        dialogueText.text = priestessStatements[currentStatementIndex]; // Show first line

        dialogueButton.onClick.AddListener(ShowNextDialogue);
    }

    void ShowNextDialogue()
    {
        if (dialogueComplete) return;

        currentStatementIndex++;

        if (currentStatementIndex < priestessStatements.Length)
        {
            // Instead of replacing, append the new statement
            dialogueText.text += "\n" + priestessStatements[currentStatementIndex];
        }
        else
        {
            dialogueComplete = true;
            dialogueButton.interactable = false; // Disable further clicks
        }
    }

    public void HideDialogue()
    {
        dialoguePanel.SetActive(false);
    }

    public void EndScene()
    {
        HideDialogue();
        // Add scene transition here if needed
        Debug.Log("Scene is ending...");
    }
}
