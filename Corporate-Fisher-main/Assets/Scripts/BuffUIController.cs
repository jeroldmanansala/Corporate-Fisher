using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class BuffUIController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CanvasGroup notificationGroup;
    [SerializeField] private TMP_Text notificationText;
    [SerializeField] private CanvasGroup iconGroup;
    [SerializeField] private Image iconImage;

    public void ShowBuff(string message, Sprite iconSprite, float fadeTime = 0.5f, float displayTime = 2f)
    {
        StopAllCoroutines();
        StartCoroutine(BuffSequence(message, iconSprite, fadeTime, displayTime));
    }

    private IEnumerator BuffSequence(string message, Sprite iconSprite, float fadeTime, float displayTime)
    {
        // Set text and icon
        notificationText.text = message;
        iconImage.sprite = iconSprite;

        // Bring UI in front
        notificationGroup.transform.SetAsLastSibling();
        iconGroup.transform.SetAsLastSibling();

        // Fade in both
        yield return StartCoroutine(Fade(notificationGroup, 0f, 1f, fadeTime));
        yield return StartCoroutine(Fade(iconGroup, 0f, 1f, fadeTime));

        // Wait
        yield return new WaitForSeconds(displayTime);

        // Fade out both
        yield return StartCoroutine(Fade(notificationGroup, 1f, 0f, fadeTime));
        yield return StartCoroutine(Fade(iconGroup, 1f, 0f, fadeTime));
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
}
