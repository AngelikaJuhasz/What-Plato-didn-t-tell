using UnityEngine;
using Ink.Runtime;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using Ink.UnityIntegration;

public class DebateManager : MonoBehaviour
{
    public static DebateManager Instance { get; private set; }

    public TextAsset inkJSON;  // Ink file for the debate
    private Story End;

    [Header("UI Elements")]
    public TextMeshProUGUI dialogueText;
    public Transform choicesContainer;
    public Button choicePrefab;
    public CanvasGroup dialogueUI;
    public Image npcPortraitImage;

    public List<Sprite> npcPortraits; // Assign in Unity Inspector
    private Dictionary<string, Sprite> portraitDictionary;

    private int correctPoints;
    private int incorrectPoints;
    private int humorousPoints;

    // Add Canvas for the outcomes
    public GameObject correctCanvas;  
    public GameObject incorrectCanvas;  
    public GameObject humorousCanvas;  

    private void Awake()
    {
        // Initialize portrait dictionary
        portraitDictionary = npcPortraits.ToDictionary(sprite => sprite.name, sprite => sprite);
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        LoadPreviousScores();
    }

    void LoadPreviousScores()
    {
        // Get the saved scores from GameProgressTracker (previous scene)
        correctPoints = GameProgressTracker.Instance.GetCorrect();
        incorrectPoints = GameProgressTracker.Instance.GetIncorrect();
        humorousPoints = GameProgressTracker.Instance.GetHumorous();

        Debug.Log($"Loaded Scores - Correct: {correctPoints}, Incorrect: {incorrectPoints}, Humorous: {humorousPoints}");
    }



    public void StartDebate(TextAsset inkFile)
    {
        inkJSON = inkFile;
        End = new Story(inkJSON.text);

        // Initialize variables if not set
        End.variablesState["correct"] = correctPoints;
        End.variablesState["incorrect"] = incorrectPoints;
        End.variablesState["humorous"] = humorousPoints;

        Debug.Log("Starting Debate...");

        ShowDialogue();
        RefreshDialogue();
    }

    void RefreshDialogue()
    {
        if (End == null)
        {
            Debug.LogError("Story object is null! Check if JSON was loaded properly.");
            return;
        }
        
        if (inkJSON == null)
        {
            Debug.LogError("Ink file is not assigned!");
            return;
        }

        // Continue the dialogue if it can
        if (End.canContinue)
        {
            string nextLine = End.Continue().Trim();
            dialogueText.text = nextLine;

        
        }

        // Remove previous choices
        foreach (Transform child in choicesContainer)
        {
            Destroy(child.gameObject);
        }

        // Generate new choices if any
        if (End.currentChoices.Count > 0)
        {
            foreach (Choice choice in End.currentChoices)
            {
                Button choiceButton = Instantiate(choicePrefab, choicesContainer);
                choiceButton.GetComponentInChildren<TextMeshProUGUI>().text = choice.text;
                choiceButton.onClick.AddListener(() => OnChoiceMade(choice));
            }
        }
        else
        {
            // Update scores once debate ends
            correctPoints = (int)End.variablesState["correct"];
            incorrectPoints = (int)End.variablesState["incorrect"];
            humorousPoints = (int)End.variablesState["humorous"];

            // Sync scores back to the GameProgressTracker
            GameProgressTracker.Instance.SetCorrect(correctPoints);
            GameProgressTracker.Instance.SetIncorrect(incorrectPoints);
            GameProgressTracker.Instance.SetHumorous(humorousPoints);

            Debug.Log($"Final Debate Scores - Correct: {correctPoints}, Incorrect: {incorrectPoints}, Humorous: {humorousPoints}");
            HideDialogue();
            ShowOutcomeCanvas();
        }
    }

    void OnChoiceMade(Choice choice)
    {
        Debug.Log("Choice made: " + choice.text);

        // Make the choice and refresh the dialogue
        End.ChooseChoiceIndex(choice.index);
        RefreshDialogue();
    }

    public void SetNPCPortrait(string portraitName)
    {
        if (portraitDictionary.TryGetValue(portraitName, out Sprite portrait))
        {
            npcPortraitImage.sprite = portrait;
            npcPortraitImage.enabled = true; // Show the image
        }
        else
        {
            Debug.LogWarning($"Portrait not found for: {portraitName}");
            npcPortraitImage.enabled = false; // Hide if no valid image
        }
    }

    // Function to display the appropriate outcome canvas based on highest score
    void ShowOutcomeCanvas()
    {
        if (correctPoints >= incorrectPoints && correctPoints >= humorousPoints)
        {
            // Display "Correct" canvas
            correctCanvas.SetActive(true);
            incorrectCanvas.SetActive(false);
            humorousCanvas.SetActive(false);
            Debug.Log("Displaying Correct Outcome");
        }
        else if (incorrectPoints >= correctPoints && incorrectPoints >= humorousPoints)
        {
            // Display "Incorrect" canvas
            correctCanvas.SetActive(false);
            incorrectCanvas.SetActive(true);
            humorousCanvas.SetActive(false);
            Debug.Log("Displaying Incorrect Outcome");
        }
        else
        {
            // Display "Humorous" canvas
            correctCanvas.SetActive(false);
            incorrectCanvas.SetActive(false);
            humorousCanvas.SetActive(true);
            Debug.Log("Displaying Humorous Outcome");
        }
    }

    public void ShowDialogue()
    {
        dialogueUI.alpha = 1;
        dialogueUI.interactable = true;
        dialogueUI.blocksRaycasts = true;
        dialogueUI.gameObject.SetActive(true);
    }

    public void HideDialogue()
    {
        dialogueUI.alpha = 0;
        dialogueUI.interactable = false;
        dialogueUI.blocksRaycasts = false;
        dialogueUI.gameObject.SetActive(false);
    }

    // Call StartDebate here if needed at the beginning of the game (e.g., in Start())
    void Start()
    {
        Debug.Log("Start method called!");

        // Load the Ink file from Resources/Ink folder
        TextAsset inkFile = Resources.Load<TextAsset>("Ink/Debate");  // Make sure the name matches the file name without the extension
        if (inkFile != null)
        {
            StartDebate(inkFile);
        }
        else
        {
            Debug.LogError("Ink file not found!");
        }
    }
}