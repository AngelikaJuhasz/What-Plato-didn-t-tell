using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private bool isPlayerNear = false;
    public List<string> dialogueLines;
    public bool hasChoices = false;

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
            if (hasChoices)
            {
                Dictionary<string, System.Action> choices = new Dictionary<string, System.Action>
                {
                    { "Accept Quest", () => Debug.Log("Quest Accepted!") },
                    { "Decline", () => Debug.Log("Maybe next time.") }
                };
                Object.FindAnyObjectByType<DialogueManager>().StartDialogue(dialogueLines, choices);
            }
            else
            {
                Object.FindAnyObjectByType<DialogueManager>().StartDialogue(dialogueLines);
            }
        }
    }
}
