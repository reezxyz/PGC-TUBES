using UnityEngine;
using UnityEngine.UI;

public class HoldProgressUI : MonoBehaviour
{
    [Header("References")]
    public Image fillImage;
    public CanvasGroup canvasGroup;

    [Header("Fade")]
    public float fadeSpeed = 8f;

    float targetAlpha = 0f;

    void Awake()
    {
        SetProgress(0f);
        canvasGroup.alpha = 0f;
        gameObject.SetActive(true);
    }

    void Update()
    {
        canvasGroup.alpha = Mathf.Lerp(
            canvasGroup.alpha,
            targetAlpha,
            Time.deltaTime * fadeSpeed
        );
    }

    public void SetProgress(float value)
    {
        fillImage.fillAmount = Mathf.Clamp01(value);
    }

    public void Show()
    {
        targetAlpha = 1f;
    }

    public void Hide()
    {
        targetAlpha = 0f;
    }

    public void ResetUI()
    {
        SetProgress(0f);
        canvasGroup.alpha = 0f;
        targetAlpha = 0f;
    }
}
