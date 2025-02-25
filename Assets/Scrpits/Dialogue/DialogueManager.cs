using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public Button nextButton;

    public GameObject choicesContainer;
    public Button choiceButtonPrefab;

    private Queue<string> sentences;
    private Dictionary<string, System.Action> choices; // Stores choices & their actions

    void Start()
    {
        sentences = new Queue<string>();
        choices = new Dictionary<string, System.Action>();

        dialoguePanel.SetActive(false);
        nextButton.onClick.AddListener(DisplayNextSentence);
    }

    public void StartDialogue(List<string> dialogueLines, Dictionary<string, System.Action> dialogueChoices = null)
    {
        dialoguePanel.SetActive(true);
        sentences.Clear();
        choices.Clear();
        choicesContainer.SetActive(false);

        foreach (string sentence in dialogueLines)
        {
            sentences.Enqueue(sentence);
        }

        if (dialogueChoices != null)
        {
            foreach (var choice in dialogueChoices)
            {
                choices[choice.Key] = choice.Value;
            }
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            if (choices.Count > 0)
            {
                ShowChoices();
                return;
            }
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    void ShowChoices()
    {
        nextButton.gameObject.SetActive(false);
        choicesContainer.SetActive(true);

        foreach (var choice in choices)
        {
            Button choiceButton = Instantiate(choiceButtonPrefab, choicesContainer.transform);
            choiceButton.gameObject.SetActive(true);
            choiceButton.GetComponentInChildren<TextMeshProUGUI>().text = choice.Key;

            choiceButton.onClick.AddListener(() =>
            {
                choice.Value.Invoke(); // Execute the function tied to the choice
                ClearChoices();
                EndDialogue();
            });
        }
    }

    void ClearChoices()
    {
        foreach (Transform child in choicesContainer.transform)
        {
            Destroy(child.gameObject);
        }
        choices.Clear();
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        nextButton.gameObject.SetActive(true);
    }
}
