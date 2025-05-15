using UnityEngine;
using UnityEngine.UI;


public class PlayerStats : MonoBehaviour
{
    public float hunger = 100f;
    public float maxHunger = 100f;
    public float hungerDrainRate = 100f; // hunger per second

    public bool hungerZero = false;

    private float hungerLogTimer = 0f; // used to throttle debug messages
    private float logInterval = 1f; // how often to log hunger
    public Slider hungerBar;
    private PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        if (hungerBar != null)
        {
            hungerBar.maxValue = maxHunger;
            hungerBar.value = hunger;
        }
    }


    void Update()
    {

        hunger -= hungerDrainRate * Time.deltaTime;
        hunger = Mathf.Clamp(hunger, 0, maxHunger);

        // Log hunger every second
        hungerLogTimer += Time.deltaTime;
        if (hungerLogTimer >= logInterval)
        {
            Debug.Log("Current Hunger: " + hunger.ToString("F1"));
            hungerLogTimer = 0f;
        }
        if (hungerBar != null)
        {
            hungerBar.value = hunger;
        }


        if (hunger <= 0 && !hungerZero)
        {
            HungerDebuff();
        }
        else if (hunger > 0 && hungerZero)
        {
            RemoveDebuff();
        }
    }

    public void EatFood(float amount)
    {
        hunger += amount;
        hunger = Mathf.Clamp(hunger, 0, maxHunger);
        Debug.Log("Ate food. Hunger is now: " + hunger);
    }

    void HungerDebuff()
    {
        hungerZero = true;
        Debug.Log("You're hungry! Movement slowed.");
        if (playerMovement != null)
        {
            playerMovement.SetSpeedMultiplier(0.3f); // Example: 30% speed
        }
    }

    void RemoveDebuff()
    {
        hungerZero = false;
        Debug.Log("Hunger recovered. Speed restored.");
        if (playerMovement != null)
        {
            playerMovement.SetSpeedMultiplier(1f); // Normal speed
        }
    }
}
