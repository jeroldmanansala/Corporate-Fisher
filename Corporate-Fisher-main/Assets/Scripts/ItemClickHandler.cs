using UnityEngine;
using UnityEngine.EventSystems;

public class ItemClickHandler : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        InvController inv = FindFirstObjectByType<InvController>();
        if (inv != null)
        {
            inv.SelectItem(gameObject); // Send item to InvController
        }
    }
}
