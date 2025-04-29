using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InvController : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public int slotCount;
    public GameObject[] itemPrefabs;

    public TextMeshProUGUI itemNameText;
    public GameObject eatButton;
    public Image selectedHighlight;

    private GameObject selectedItem;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i=0;i<slotCount;i++) 
        {
            Slots slot = Instantiate(slotPrefab, inventoryPanel.transform).GetComponent<Slots>();
        }
    }

    public bool AddItem(GameObject itemPrefab)
    {
        foreach(Transform slotTransform in inventoryPanel.transform)
        {
            Slots slot = slotTransform.GetComponent<Slots>();
            if (slot != null && slot.currentItem == null)
            {
                GameObject newItem = Instantiate(itemPrefab, slotTransform);
                newItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot.currentItem = newItem;
                return true;
            }
        }
        Debug.Log("Invnetory full!");
        return false;
    }

    public GameObject GetRandomFish()
    {
        if (itemPrefabs.Length == 0) return null;

        int index = Random.Range(0, itemPrefabs.Length);
        return itemPrefabs[index];
    }

    public void EatSelected()
{
    if (selectedItem != null)
    {
        FishItem fish = selectedItem.GetComponent<FishItem>();
        if (fish != null)
        {
            PlayerStats player = FindFirstObjectByType<PlayerStats>();
            player.EatFood(fish.hungerRestore); // Update hunger

            Slots slot = selectedItem.transform.parent.GetComponent<Slots>();
            if (slot != null) slot.currentItem = null;

            Destroy(selectedItem);
            Deselect();
        }
    }

}

public void Deselect()
{
    selectedItem = null;

    if (selectedHighlight != null)
        selectedHighlight.gameObject.SetActive(false);

    if (itemNameText != null)
        itemNameText.text = "";

    if (eatButton != null)
        eatButton.SetActive(false);
}

public void SelectItem(GameObject item)
{
    selectedItem = item;

    FishItem fish = item.GetComponent<FishItem>();
    if (fish != null)
    {
        itemNameText.text = fish.fishName;

        eatButton.SetActive(true);

        // Highlight the slot the item is in
        Transform slotTransform = item.transform.parent;
        selectedHighlight.gameObject.SetActive(true);
        selectedHighlight.transform.SetParent(slotTransform);
        selectedHighlight.rectTransform.anchoredPosition = Vector2.zero;
    }
}





}
