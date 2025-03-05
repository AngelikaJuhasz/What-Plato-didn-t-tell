using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private bool isPlayerNear = false;

    [Header("Dialogue Settings")]
    public TextAsset inkDialogueFile; // Unique dialogue per NPC
    public Sprite npcPortraitSprite; // NPC portrait

    [Header("Scene Transition")]
    [SerializeField] private string sceneToLoad; // Scene to load

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    void Interact()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            FindAnyObjectByType<SceneTransitionManager>().LoadScene(sceneToLoad);
        }
        else if (inkDialogueFile != null)
        {
            InkDialogueManager.Instance.StartDialogue(inkDialogueFile);
            InkDialogueManager.Instance.SetNPCPortrait(npcPortraitSprite); // Handles portrait separately
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
}
