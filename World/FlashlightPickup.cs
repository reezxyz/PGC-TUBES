using UnityEngine;

public class FlashlightPickup : MonoBehaviour, IInteractable
{
    public string interactText = "Press E to take flashlight";

    public void Interact()
    {
        PlayerInventory inventory = FindObjectOfType<PlayerInventory>();

        if (inventory != null)
        {
            inventory.PickupFlashlight();
            Destroy(gameObject);
        }
    }

    public string GetInteractText()
    {
        return interactText;
    }
}
