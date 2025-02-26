using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance; // optional singleton if desired

    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public Button nextButton;

    public GameObject choicesContainer;
    public Button choiceButtonPrefab;

    private Queue<string> sentences;
    private Dictionary<string, System.Action> choices; // Stores choices & their actions

    public Image playerPortrait;
    public Image npcPortrait;
    public Sprite defaultPlayerSprite;  // Default player's portrait

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        sentences = new Queue<string>();
        choices = new Dictionary<string, System.Action>();

        dialoguePanel.SetActive(false);
        nextButton.onClick.AddListener(DisplayNextSentence);
    }

    // Overload: Uses default player's portrait automatically.
    public void StartDialogue(List<string> dialogueLines, Sprite npcPortraitSprite, Dictionary<string, System.Action> dialogueChoices = null)
    {
        StartDialogue(dialogueLines, npcPortraitSprite, defaultPlayerSprite, dialogueChoices);
    }

    // Main StartDialogue: requires both NPC and player portrait sprites.
    public void StartDialogue(List<string> dialogueLines, Sprite npcPortraitSprite, Sprite playerPortraitSprite, Dictionary<string, System.Action> dialogueChoices = null)
    {
        dialoguePanel.SetActive(true);
        sentences.Clear();
        choices.Clear();
        ClearChoices();

        // Set the portraits
        if (playerPortrait != null)
        {
            playerPortrait.sprite = playerPortraitSprite;
            playerPortrait.enabled = false; // Hide by default (shown when player speaks)
        }
        if (npcPortrait != null)
        {
            npcPortrait.sprite = npcPortraitSprite;
            npcPortrait.enabled = true; // NPC portrait is visible by default
        }

        // Enqueue each dialogue line.
        foreach (string sentence in dialogueLines)
        {
            sentences.Enqueue(sentence);
        }

        // If there are choices, store them.
        if (dialogueChoices != null)
        {
            choices = new Dictionary<string, System.Action>(dialogueChoices);
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

        // Decide which portrait to show based on markers in the sentence.
        // Example: sentences starting with "[NPC]" show NPC portrait; "[Player]" shows player portrait.
        if (sentence.StartsWith("[NPC]"))
        {
            npcPortrait.enabled = true;
            playerPortrait.enabled = false;
        }
        else if (sentence.StartsWith("[Player]"))
        {
            playerPortrait.enabled = true;
            npcPortrait.enabled = false;
        }
    }

    private void ShowChoices()
    {
        nextButton.gameObject.SetActive(false);
        choicesContainer.SetActive(true);

        foreach (var choice in choices)
        {
            Button choiceButton = Instantiate(choiceButtonPrefab, choicesContainer.transform);
            choiceButton.gameObject.SetActive(true);

            TextMeshProUGUI choiceText = choiceButton.GetComponentInChildren<TextMeshProUGUI>();
            if (choiceText != null)
            {
                choiceText.text = choice.Key;
            }
            else
            {
                Debug.LogError("Choice button is missing a TextMeshProUGUI component.");
            }

            choiceButton.onClick.RemoveAllListeners();
            choiceButton.onClick.AddListener(() =>
            {
                choice.Value.Invoke(); // Execute the action for this choice.
                ClearChoices();
                EndDialogue();
            });
        }
    }

    private void ClearChoices()
    {
        foreach (Transform child in choicesContainer.transform)
        {
            Destroy(child.gameObject);
        }
        choices.Clear();
        choicesContainer.SetActive(false);
    }

    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        nextButton.gameObject.SetActive(true);
    }
}
