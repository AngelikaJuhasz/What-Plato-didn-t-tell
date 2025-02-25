using UnityEngine;

public class Interactable : MonoBehaviour
{
    // Override this method to specify custom interaction behavior
    public virtual void Interact()
    {
        Debug.Log("Interacted with " + gameObject.name);
    }
}
