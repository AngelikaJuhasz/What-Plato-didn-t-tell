using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    private Dictionary<string, int> ingredientCounts = new Dictionary<string, int>();

    public void OnDrop(PointerEventData eventData)
    {
        DraggableIngredient ingredient = eventData.pointerDrag.GetComponent<DraggableIngredient>();

        if (ingredient != null)
        {
            string ingredientName = ingredient.gameObject.name.Replace("(Clone)", "").Trim();

            // Track ingredient counts
            if (ingredientCounts.ContainsKey(ingredientName))
            {
                ingredientCounts[ingredientName]++;
            }
            else
            {
                ingredientCounts[ingredientName] = 1;
            }

            Debug.Log($"Added {ingredientName}. Total: {ingredientCounts[ingredientName]}");

            // Move ingredient into drop zone
            ingredient.transform.SetParent(transform);
            ingredient.transform.localPosition = Vector3.zero;
        }
    }

    public Dictionary<string, int> GetFinalIngredients()
    {
        return new Dictionary<string, int>(ingredientCounts);
    }

    public void ResetIngredients()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        ingredientCounts.Clear();
    }
}
