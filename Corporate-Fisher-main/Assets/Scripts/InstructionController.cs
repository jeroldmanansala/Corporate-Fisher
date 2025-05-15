using UnityEngine;
using System.Collections;

public class InstructionController : MonoBehaviour
{
    [Header("UI CanvasGroups")]
    [SerializeField] private CanvasGroup moveUI;
    [SerializeField] private CanvasGroup fishUI;
    [SerializeField] private CanvasGroup inventoryUI;

    [Header("Player & Water")]
    [SerializeField] private Transform player;
    [SerializeField] private float waterX = 8f;  // Adjust to your water’s X-position

    [SerializeField] private CanvasGroup selectFishUI;
private bool selectFishShown = false;


    private void Update()
    {
        // Hide inventory hint when Tab is pressed
        if (inventoryUI.alpha > 0f && Input.GetKeyDown(KeyCode.Tab))
{
    StartCoroutine(Fade(inventoryUI, 1f, 0f, 0.5f));
    if (!selectFishShown)
    {
        selectFishShown = true;
        StartCoroutine(Fade(selectFishUI, 0f, 1f, 0.5f));
    }
}

    }

    private IEnumerator Start()
    {
        // 1. Wait for any horizontal input
        yield return new WaitUntil(() => Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f);
        // 2. Fade out move instructions
        yield return Fade(moveUI, 1f, 0f, 1f);

        // 3. Wait until player reaches water
        yield return new WaitUntil(() => player.position.x >= waterX);
        // 4. Fade in fish instructions
        yield return Fade(fishUI, 0f, 1f, 1f);

        // 5. Wait for spacebar to cast
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        // 6. Fade out fish instructions
        yield return Fade(fishUI, 1f, 0f, 1f);
    }

    private IEnumerator Fade(CanvasGroup cg, float from, float to, float duration)
    {
        float elapsed = 0f;
        cg.alpha = from;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            cg.alpha = Mathf.Lerp(from, to, elapsed / duration);
            yield return null;
        }
        cg.alpha = to;
    }

    public void ShowInventoryHint()
    {
        StartCoroutine(Fade(inventoryUI, 0f, 1f, 1f));
    }

    public void OnEatPressed()
{
    StartCoroutine(Fade(selectFishUI, 1f, 0f, 0.5f));
}

}
