using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private bool isPlayerNear = false;
    public List<string> dialogueLines;
    public bool hasChoices = false;
    public Sprite npcPortraitSprite; // Unique portrait for each NPC


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            Debug.Log("Press E to interact");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            DialogueManager dm = Object.FindAnyObjectByType<DialogueManager>();
            if (dm != null)
            {
                if (hasChoices)
                {
                    Dictionary<string, System.Action> choices = new Dictionary<string, System.Action>
                    {
                        { "Accept Quest", () => Debug.Log("Quest Accepted!") },
                        { "Decline", () => Debug.Log("Maybe next time.") }
                    };
                    dm.StartDialogue(dialogueLines, npcPortraitSprite, choices);
                }
                else
                {
                    dm.StartDialogue(dialogueLines, npcPortraitSprite);
                }
            }
            else
            {
                Debug.LogError("DialogueManager not found!");
            }
        }
    }
}