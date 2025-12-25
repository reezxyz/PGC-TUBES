using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool hasFlashlight { get; private set; }

    public void PickupFlashlight()
    {
        hasFlashlight = true;
        Debug.Log("Flashlight picked up");
    }
}
