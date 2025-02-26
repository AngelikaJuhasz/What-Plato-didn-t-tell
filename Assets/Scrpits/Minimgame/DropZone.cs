using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class DropZone : MonoBehaviour, IDropHandler
{
    public Dictionary<string, int> ingredientCount = new Dictionary<string, int>();

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedItem = eventData.pointerDrag;
        if (droppedItem != null)
        {
            DraggableIngredient ingredient = droppedItem.GetComponent<DraggableIngredient>();
            if (ingredient != null)
            {
                droppedItem.transform.SetParent(transform); // Move to mixing area
                droppedItem.transform.localPosition = Vector3.zero; // Center it
                AddIngredient(droppedItem.name);
            }
        }
    }

    void AddIngredient(string ingredientName)
    {
        if (!ingredientCount.ContainsKey(ingredientName))
            ingredientCount[ingredientName] = 0;

        ingredientCount[ingredientName]++;
        Debug.Log(ingredientName + " added! Total: " + ingredientCount[ingredientName]);
    }

    public Dictionary<string, int> GetFinalIngredients()
    {
        return ingredientCount;
    }
}
