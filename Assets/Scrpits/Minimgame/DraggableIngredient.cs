using UnityEngine;
using UnityEngine.EventSystems;
using TMPro; // Required for text display

public class DraggableIngredient : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Transform originalParent;

    public int maxUses = 4; // Maximum times this ingredient can be used
    private int remainingUses; // Tracks remaining uses
    public TextMeshProUGUI countText; // UI text to display remaining uses

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalParent = transform.parent;
        remainingUses = maxUses;
        UpdateCountText();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (remainingUses <= 0) return; // Prevent dragging if no uses left

        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.7f;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (remainingUses <= 0) return; // Don't allow movement if out of uses
        rectTransform.anchoredPosition += eventData.delta / transform.lossyScale.x;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

        if (transform.parent == transform.root)
        {
            transform.SetParent(originalParent);
        }
        else
        {
            UseIngredient(); // Reduce count if successfully dropped
        }
    }

    private void UseIngredient()
    {
        remainingUses--;
        UpdateCountText();

        if (remainingUses <= 0)
        {
            gameObject.SetActive(false); // Hide the ingredient if no uses left
        }
    }

    private void UpdateCountText()
    {
        if (countText != null)
        {
            countText.text = remainingUses.ToString();
        }
    }

    public void ResetIngredient()
    {
        remainingUses = maxUses;
        UpdateCountText();
        gameObject.SetActive(true);
    }
}
