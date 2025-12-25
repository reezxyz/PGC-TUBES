using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public float mouseSensitivity = 150f;
    public Transform playerBody;

    public float standCameraHeight = 1.6f;
    public float crouchCameraHeight = 1.0f;
    public float cameraSmooth = 8f;

    float xRotation = 0f;
    float currentCameraHeight;

    PlayerMovement movement;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        movement = playerBody.GetComponent<PlayerMovement>();
        currentCameraHeight = standCameraHeight;
    }

    void Update()
    {
        HandleMouseLook();
        HandleCameraHeight();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    void HandleCameraHeight()
    {
        if (movement == null) return;

        float targetHeight = movement.IsCrouching 
            ? crouchCameraHeight 
            : standCameraHeight;

        currentCameraHeight = Mathf.Lerp(
            currentCameraHeight, 
            targetHeight, 
            Time.deltaTime * cameraSmooth
        );

        transform.localPosition = new Vector3(0, currentCameraHeight, 0);
    }
}
