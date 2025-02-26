using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class DraggableIngredient : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Transform originalParent;

    [SerializeField] private GameObject ingredientPrefab; // Prefab of the ingredient
    [SerializeField] private int maxUnits = 4; // Total allowed uses

    private static Dictionary<string, int> ingredientTracker = new Dictionary<string, int>();

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalParent = transform.parent;

        string ingredientName = gameObject.name.Replace("(Clone)", "").Trim();

        if (!ingredientTracker.ContainsKey(ingredientName))
        {
            ingredientTracker[ingredientName] = maxUnits;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        string ingredientName = gameObject.name.Replace("(Clone)", "").Trim();

        if (ingredientTracker[ingredientName] <= 0)
        {
            Debug.Log($"No more {ingredientName} left to drag!");
            eventData.pointerDrag = null; // Prevents dragging
            return;
        }

        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.7f;

        //  **Instantly create a new ingredient BEFORE moving**
        if (ingredientTracker[ingredientName] > 1)
        {
            SpawnNewIngredient();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / transform.lossyScale.x;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

        string ingredientName = gameObject.name.Replace("(Clone)", "").Trim();

        if (transform.parent == originalParent)
        {
            rectTransform.anchoredPosition = Vector2.zero; // Reset position if not placed
        }
        else
        {
            ingredientTracker[ingredientName]--;
            Debug.Log($"{ingredientName} remaining: {ingredientTracker[ingredientName]}");

            if (ingredientTracker[ingredientName] == 0)
            {
                Debug.Log($"{ingredientName} is now fully used up!");
            }
        }
    }

    private void SpawnNewIngredient()
    {
        GameObject newIngredient = Instantiate(ingredientPrefab, originalParent);
        newIngredient.transform.SetSiblingIndex(transform.GetSiblingIndex()); // Keep correct order

        // Reset alpha & enable raycasts to prevent the issue
        CanvasGroup newCanvasGroup = newIngredient.GetComponent<CanvasGroup>();
        if (newCanvasGroup != null)
        {
            newCanvasGroup.alpha = 1f;
            newCanvasGroup.blocksRaycasts = true; //  This is the real fix!
        }
    }


}
