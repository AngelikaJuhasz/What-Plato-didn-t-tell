using UnityEngine;

public class NPC : Interactable
{
    public Dialogue dialogue; // Assign via inspector

    public override void Interact()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }
}
