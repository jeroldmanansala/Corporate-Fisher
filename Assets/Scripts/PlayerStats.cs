using UnityEngine;
using UnityEngine.UI;


public class PlayerStats : MonoBehaviour
{
    public float hunger = 100f;
    public float maxHunger = 100f;
    public float hungerDrainRate = 100f; // hunger per second

    public bool isDead = false;

    private float hungerLogTimer = 0f; // used to throttle debug messages
    private float logInterval = 1f; // how often to log hunger
    public Slider hungerBar;

    void Start()
    {
        if (hungerBar != null)
        {
            hungerBar.maxValue = maxHunger;
            hungerBar.value = hunger;
        }
    }


    void Update()
    {
        if (isDead) return;

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


        if (hunger <= 0)
        {
            DieOfHunger();
        }
    }

    public void EatFood(float amount)
    {
        hunger += amount;
        hunger = Mathf.Clamp(hunger, 0, maxHunger);
        Debug.Log("🍽️ Ate food. Hunger is now: " + hunger);
    }

    void DieOfHunger()
    {
        isDead = true;
        Debug.Log("💀 You died of hunger.");
        // Future: Play animation, show game over screen, etc.
    }
}

