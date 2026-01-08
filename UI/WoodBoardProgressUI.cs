using UnityEngine;
using UnityEngine.UI;

public class WoodBoardProgressUI : MonoBehaviour
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
