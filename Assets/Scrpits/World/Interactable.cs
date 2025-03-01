using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private bool isPlayerNear = false;
    public List<string> dialogueLines;
    public bool hasChoices = false;
    public Sprite npcPortraitSprite; // Unique portrait for each NPC

    [SerializeField] private string sceneToLoad; // Set this in Unity for each object

    public void Interact()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            FindAnyObjectByType<SceneTransitionManager>().LoadScene(sceneToLoad);
        }
    }


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
                        { "Choice 1", () => Debug.Log("You chose correctly!") },
                        { "Choice 2", () => Debug.Log("Thats not quite right...") }
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