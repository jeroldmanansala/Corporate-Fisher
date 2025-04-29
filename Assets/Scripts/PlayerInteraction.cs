using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    private IInteractable currentInteractable;

    public GameObject interactUI;
    public TextMeshProUGUI interactText;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentInteractable != null)
            {
                currentInteractable.Interact();
                interactUI.SetActive(false); // hide prompt after interaction
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IInteractable interactable = collision.GetComponent<IInteractable>();

        if (interactable != null)
        {
            currentInteractable = interactable;
            interactText.text = currentInteractable.GetDescription();
            interactUI.SetActive(true);
        }
    }

private void OnTriggerExit2D(Collider2D collision)
{
    var exitingInteractable = collision.GetComponent<IInteractable>();

    if (exitingInteractable != null && exitingInteractable == currentInteractable)
    {
        // If it's a shrine and it's open, close it
        InteractShrine shrine = exitingInteractable as InteractShrine;
        if (shrine != null)
        {
            shrine.ForceCloseUI();
        }

        currentInteractable = null;
        interactUI.SetActive(false);
    }
}


}
