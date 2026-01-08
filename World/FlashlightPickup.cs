using UnityEngine;

public class FlashlightPickup : MonoBehaviour, IInteractable
{
    [Header("Interaction")]
    public KeyCode pickupKey = KeyCode.E;

    InteractionTextSpawner textSpawner;
    bool pickedUp = false;

    void Awake()
    {
        textSpawner = GetComponent<InteractionTextSpawner>();
    }

    // DIPANGGIL SETIAP FRAME SAAT DI-HOVER
    public void Interact()
    {
        if (pickedUp) return;

        if (!Input.GetKeyDown(pickupKey)) return;

        PlayerInventory inventory = FindObjectOfType<PlayerInventory>();
        if (inventory == null) return;

        inventory.PickupFlashlight();
        pickedUp = true;

        // bersihkan UI & object
        if (textSpawner != null)
            Destroy(textSpawner.gameObject);

        Destroy(gameObject);
    }

    public void OnHoverEnter()
    {
        if (pickedUp) return;

        if (textSpawner != null)
            textSpawner.Show();
    }

    public void OnHoverExit()
    {
        if (pickedUp) return;

        if (textSpawner != null)
            textSpawner.Hide();
    }
}
