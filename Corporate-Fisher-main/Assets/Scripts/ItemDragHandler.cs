using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform originalParent;
    CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;

        GameObject dragLayer = GameObject.Find("DragLayer");
        transform.SetParent(dragLayer.transform); // Frees it from layout/mask

        transform.SetAsLastSibling(); // Make sure it's drawn on top

        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
    }


    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

        Slots dropSlot = eventData.pointerEnter?.GetComponent<Slots>();
        if(dropSlot == null)
        {
            GameObject dropItem = eventData.pointerEnter;
            if(dropItem != null)
            {
                dropSlot = dropItem.GetComponentInParent<Slots>();
            }
        }
        Slots originalSlot = originalParent.GetComponent<Slots>();

        if(dropSlot != null) {
        if(dropSlot.currentItem != null)
        {
            dropSlot.currentItem.transform.SetParent(originalSlot.transform);
            originalSlot.currentItem = dropSlot.currentItem;
            dropSlot.currentItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
        else
        {
            originalSlot.currentItem = null;
        }
        transform.SetParent(dropSlot.transform);
        dropSlot.currentItem = gameObject;
    }
    else
    {
        transform.SetParent(originalParent);
    }
    GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }
}
