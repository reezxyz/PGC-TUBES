using UnityEngine;
using UnityEngine.UI;

public class ValveProgressUI : MonoBehaviour
{
    public Image fillImage;
    public CanvasGroup canvasGroup;

    void Start()
    {
        SetVisible(false);
    }

    public void SetProgress(float value)
    {
        fillImage.fillAmount = value;

        // Opacity mengikuti progress
        canvasGroup.alpha = value;
    }

    public void SetVisible(bool visible)
    {
        gameObject.SetActive(true);

        if (!visible)
            canvasGroup.alpha = 0f;
    }

    public void FadeOutImmediate()
    {
        canvasGroup.alpha = 0f;
    }
}
