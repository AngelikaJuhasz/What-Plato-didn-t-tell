using UnityEngine;
using Ink.Runtime;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class InkDialogueManager : MonoBehaviour
{
    public static InkDialogueManager Instance { get; private set; }

    public TextAsset inkJSON;
    private Story story;

    public TextMeshProUGUI dialogueText;
    public Transform choicesContainer;
    public Button choicePrefab;
    public CanvasGroup dialogueUI; // Reference to the UI CanvasGroup
    public Image npcPortraitImage; // Assign in the Unity Inspector

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        HideDialogue(); // Ensure it's hidden on start
    }

    public void StartDialogue(TextAsset inkFile)
    {
        inkJSON = inkFile;
        story = new Story(inkJSON.text);

        // Sync credibility from Unity to Ink
        if (story.variablesState.Contains("correct"))
        {
            int currentCorrect = GameProgressTracker.Instance.GetCorrect();
            story.variablesState["correct"] = currentCorrect;
            Debug.Log("Starting Dialogue - Correct: " + currentCorrect);
        }
        if (story.variablesState.Contains("incorrect"))
        {
            int currentIncorrect = GameProgressTracker.Instance.GetIncorrect();
            story.variablesState["incorrect"] = currentIncorrect;
            Debug.Log("Starting Dialogue - Incorrect: " + currentIncorrect);
        }
        if (story.variablesState.Contains("humorous"))
        {
            int currentHumorous = GameProgressTracker.Instance.GetHumorous();
            story.variablesState["humorous"] = currentHumorous;
            Debug.Log("Starting Dialogue - Humorous: " + currentHumorous);
        }

        ShowDialogue();
        RefreshDialogue();
    }

    void RefreshDialogue()
    {
        if (story == null)
        {
            Debug.LogError("Story object is null! Check if JSON was loaded properly.");
            return;
        }

        // Skip echoed choices
        if (story.canContinue)
        {
            string nextLine;
            do
            {
                nextLine = story.Continue().Trim();
            } while (string.IsNullOrEmpty(nextLine) && story.canContinue);

            dialogueText.text = nextLine;
        }

        // Remove previous choices
        foreach (Transform child in choicesContainer) Destroy(child.gameObject);

        // Generate new choices
        if (story.currentChoices.Count > 0)
        {
            foreach (Choice choice in story.currentChoices)
            {
                Button choiceButton = Instantiate(choicePrefab, choicesContainer);
                choiceButton.GetComponentInChildren<TextMeshProUGUI>().text = choice.text;
                choiceButton.onClick.AddListener(() => Choose(choice.index));
            }
        }
        else
        {
            // Log the current point values before updating Unity's progress tracker
            LogCurrentPoints();

            // Update Unity credibility once choices are gone
            if (story.variablesState.Contains("correct"))
            {
                int inkCorrect = (int)story.variablesState["correct"];
                GameProgressTracker.Instance.SetCorrect(inkCorrect);
                Debug.Log("Updated Correct count in GameProgressTracker: " + inkCorrect);
            }

            if (story.variablesState.Contains("incorrect"))
            {
                int inkIncorrect = (int)story.variablesState["incorrect"];
                GameProgressTracker.Instance.SetIncorrect(inkIncorrect);
                Debug.Log("Updated Incorrect count in GameProgressTracker: " + inkIncorrect);
            }

            if (story.variablesState.Contains("humorous"))
            {
                int inkHumorous = (int)story.variablesState["humorous"];
                GameProgressTracker.Instance.SetHumorous(inkHumorous);
                Debug.Log("Updated Humorous count in GameProgressTracker: " + inkHumorous);
            }

            HideDialogue(); // Hide when dialogue ends
        }
    }

    // Method to log current point values
    void LogCurrentPoints()
    {
        int currentCorrect = GameProgressTracker.Instance.GetCorrect();
        int currentIncorrect = GameProgressTracker.Instance.GetIncorrect();
        int currentHumorous = GameProgressTracker.Instance.GetHumorous();

        Debug.Log("Current Points - Correct: " + currentCorrect);
        Debug.Log("Current Points - Incorrect: " + currentIncorrect);
        Debug.Log("Current Points - Humorous: " + currentHumorous);
    }

    public void SetNPCPortrait(Sprite portrait)
    {
        if (npcPortraitImage != null)
        {
            npcPortraitImage.sprite = portrait;
            npcPortraitImage.enabled = (portrait != null); // Hide the image if no portrait is given
        }
    }

    void Choose(int choiceIndex)
    {
        story.ChooseChoiceIndex(choiceIndex);
        RefreshDialogue();
    }

    public void ShowDialogue()
    {
        if (dialogueUI == null)
        {
            Debug.LogError("Dialogue UI is not assigned!");
            return;
        }

        dialogueUI.alpha = 1;
        dialogueUI.interactable = true;
        dialogueUI.blocksRaycasts = true;
        dialogueUI.gameObject.SetActive(true); // Ensure the UI GameObject is active
    }

    public void HideDialogue()
    {
        if (dialogueUI == null)
        {
            Debug.LogError("Dialogue UI is not assigned!");
            return;
        }

        dialogueUI.alpha = 0;
        dialogueUI.interactable = false;
        dialogueUI.blocksRaycasts = false;
        dialogueUI.gameObject.SetActive(false); // Deactivate the GameObject to hide
    }
}
