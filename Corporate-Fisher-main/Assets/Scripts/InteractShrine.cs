using UnityEngine;

public class InteractShrine : MonoBehaviour, IInteractable
{
    public GameObject shrineUIPanel;
    private bool isUIOpen = false;

    public void Interact()
    {
        if (!isUIOpen)
        {
            OpenShrineUI();
        }
        else
        {
            CloseShrineUI();
        }
    }

    private void OpenShrineUI()
    {
        shrineUIPanel.SetActive(true);
        isUIOpen = true;
    }

    private void CloseShrineUI()
    {
        shrineUIPanel.SetActive(false);
        isUIOpen = false;
    }

    public string GetDescription()
    {
        return isUIOpen ? "Close Shrine" : "Offer to Shrine";
    }

    public void ForceCloseUI()
{
    if (isUIOpen)
    {
        shrineUIPanel.SetActive(false);
        isUIOpen = false;
    }
}

}


