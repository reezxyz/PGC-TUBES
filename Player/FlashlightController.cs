using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public Light flashlightLight;
    public KeyCode toggleKey = KeyCode.F;

    private PlayerInventory inventory;
    private bool isOn;

    void Start()
    {
        inventory = GetComponentInParent<PlayerInventory>();
        flashlightLight.enabled = false;
    }

    void Update()
    {
        if (!inventory.hasFlashlight)
            return;

        if (Input.GetKeyDown(toggleKey))
        {
            isOn = !isOn;
            flashlightLight.enabled = isOn;
        }
    }
}
