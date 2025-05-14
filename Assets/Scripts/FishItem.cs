using UnityEngine;

public enum Rarity { Common, Uncommon, Rare, Legendary }

public class FishItem : MonoBehaviour
{
    public string fishName = "Fish1";
    public int hungerRestore = 25;
    public Rarity rarity = Rarity.Common;
    public bool edible = true;
    public bool canBeOffered = true;
    public Sprite fishSprite;
    public int xpValue = 25;
}

