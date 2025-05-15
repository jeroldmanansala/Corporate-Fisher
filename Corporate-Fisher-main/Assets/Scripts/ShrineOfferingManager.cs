using System.Collections.Generic;
using UnityEngine;

public class ShrineOfferingManager : MonoBehaviour
{
    public List<Slots> shrineSlots; 
    private int offeringCount = 0; // Track how many offerings the player has completed

    public PlayerMovement playerMovement;
    public PlayerStats playerStats;

    public int currentXP = 0;
    public int xpToNext = 100;
    public int essence = 0;

    [Header("Buff UI")]
[SerializeField] private BuffUIController buffUI;
    [SerializeField] private Sprite speedBuffIcon;

    public bool OfferItems()
    {
        if (AreSlotsFull())
        {
            offeringCount++;
            ApplyBlessing(offeringCount);
            ClearShrineSlots();
            return true;
        }
        else
        {
            Debug.Log("Offering incomplete! Place 3 items.");
            return false;
        }
    }

    public void TryOffer() {
        OfferItems();
    }

    private bool AreSlotsFull()
    {
        foreach (var slot in shrineSlots)
        {
            if (slot.currentItem == null)
                return false;
        }
        return true;
    }

    private void ClearShrineSlots()
    {
        foreach (var slot in shrineSlots)
        {
            if (slot.currentItem != null)
            {
                Destroy(slot.currentItem);
                slot.currentItem = null;
            }
        }
    }

    public void ApplyBlessing(int count)
    {
        if (count == 1)
        {
            Debug.Log("Blessing 1: Increased move speed!");
            playerMovement.moveSpeed += 3f; 
            buffUI.ShowBuff("You have been granted increased movement speed!", speedBuffIcon);
        }
        else if (count == 2)
        {
            Debug.Log("Blessing 2: Increased max hunger!");
            playerStats.maxHunger += 40f;
        }
        else
        {
            Debug.Log("All blessings received!");
            // TODO: unlock portal, visuals
        }
    }
}
