using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float distance = 3f;
    public float radius = 0.25f;
    public LayerMask interactMask;

    IInteractable current;
    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Detect();
    }

    void Detect()
    {
        IInteractable next = null;

        if (Physics.SphereCast(
            cam.transform.position,
            radius,
            cam.transform.forward,
            out RaycastHit hit,
            distance,
            interactMask
        ))
        {
            next = hit.collider.GetComponentInParent<IInteractable>();
        }

        if (next != current)
        {
            current?.OnHoverExit();
            next?.OnHoverEnter();
            current = next;
        }

        current?.Interact();
        if (next != null)
        {
            Debug.Log("Hover: " + next);
        }

    }
    
}
