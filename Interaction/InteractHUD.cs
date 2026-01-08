using UnityEngine;
using TMPro;

public class InteractHUD : MonoBehaviour
{
    public float interactDistance = 3f;
    public LayerMask interactLayer;

    public Transform worldUI;              // Canvas WorldUI
    public TextMeshProUGUI interactText;   // TMP di dalamnya

    Camera cam;
    Transform currentTarget;
    string lastText;
    bool isVisible;

    void Start()
    {
        cam = Camera.main;
        worldUI.gameObject.SetActive(false);
    }

    void Update()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactLayer))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                if (hit.collider.transform != currentTarget)
                {
                    Show(interactable.GetInteractText(), hit.collider.transform);
                }
                return;
            }
        }

        Hide();
    }


    void Show(string text, Transform target)
    {
        currentTarget = target;

        if (!isVisible)
        {
            worldUI.gameObject.SetActive(true);
            isVisible = true;
        }

        if (lastText != text)
        {
            interactText.text = text;
            lastText = text;
        }

    }


    void Hide()
    {
        if (!isVisible) return;

        worldUI.gameObject.SetActive(false);
        isVisible = false;
        currentTarget = null;
        lastText = null;
    }

}
