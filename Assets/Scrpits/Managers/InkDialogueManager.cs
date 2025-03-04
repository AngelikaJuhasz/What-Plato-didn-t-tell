using UnityEngine;
using Ink.Runtime;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class InkDialogueManager : MonoBehaviour
{
    public TextAsset inkJSON;
    private Story story;

    public TextMeshProUGUI dialogueText;
    public Transform choicesContainer;
    public Button choicePrefab;

    void Start()
    {
        if (inkJSON == null)
        {
            Debug.LogError("Ink JSON file is missing! Assign it in the Inspector.");
            return;
        }

        story = new Story(inkJSON.text);
        RefreshDialogue();
    }

    void RefreshDialogue()
    {
        if (story == null)
        {
            Debug.LogError("Story object is null! Check if JSON was loaded properly.");
            return;
        }

        // Skip the choice echo by advancing past the first line after a choice
        if (story.canContinue)
        {
            string nextLine;
            do
            {
                nextLine = story.Continue().Trim();
            } while (string.IsNullOrEmpty(nextLine) && story.canContinue); // Skip empty lines

            dialogueText.text = nextLine; // Display the next valid dialogue line
        }

        // Remove any previous choice buttons
        foreach (Transform child in choicesContainer)
        {
            Destroy(child.gameObject);
        }

        // Generate new choice buttons if there are choices available
        if (story.currentChoices.Count > 0)
        {
            foreach (Choice choice in story.currentChoices)
            {
                Button choiceButton = Instantiate(choicePrefab, choicesContainer);
                choiceButton.GetComponentInChildren<TextMeshProUGUI>().text = choice.text;
                choiceButton.onClick.AddListener(() => Choose(choice.index));
            }
        }
    }


    void Choose(int choiceIndex)
    {
        story.ChooseChoiceIndex(choiceIndex);
        RefreshDialogue();
    }
}
