using UnityEngine;

public class FlashlightPickup : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        PlayerInventory inventory = other.GetComponent<PlayerInventory>();

        if (inventory != null)
        {
            inventory.PickupFlashlight();
            Destroy(gameObject);
        }
    }
}
