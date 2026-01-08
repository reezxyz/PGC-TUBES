using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool hasFlashlight { get; private set; }
    public bool hasPipe = false;

    public void PickupFlashlight()
    {
        hasFlashlight = true;
        Debug.Log("Flashlight picked up");
    }

    public void ConsumePipe()
{
    hasPipe = false;
}
}
