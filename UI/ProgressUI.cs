using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgressUI : MonoBehaviour
{
    [Header("References")]
    public Image fillImage;
    public CanvasGroup canvasGroup;

    [Header("Fade")]
    public float fadeOutDuration = 0.4f;

    Coroutine fadeRoutine;

    void Awake()
    {
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();

        canvasGroup.alpha = 0f;
    }

    public void SetProgress(float value)
    {
        value = Mathf.Clamp01(value);

        if (fillImage != null)
            fillImage.fillAmount = value;

        // SELAMA ADA PROGRESS → UI TETAP TERLIHAT
        if (value > 0f)
        {
            ShowImmediate();
        }
        else
        {
            // BARU FADE OUT SETELAH 0
            FadeOut();
        }
    }

    public void ShowImmediate()
    {
        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        canvasGroup.alpha = 1f;
    }

    public void FadeOut()
    {
        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        fadeRoutine = StartCoroutine(FadeOutRoutine());
    }

    IEnumerator FadeOutRoutine()
    {
        float startAlpha = canvasGroup.alpha;
        float t = 0f;

        while (t < fadeOutDuration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, t / fadeOutDuration);
            yield return null;
        }

        canvasGroup.alpha = 0f;
        fadeRoutine = null;
    }

    public void SetVisible(bool visible)
    {
        if (visible)
            ShowImmediate();
        else
            FadeOut();
    }
}
