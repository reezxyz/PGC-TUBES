using UnityEngine;

public class FlashlightLag : MonoBehaviour
{
    public Transform cameraTransform;
    public float followSpeed = 6f;

    Quaternion currentRotation;

    void Start()
    {
        currentRotation = cameraTransform.rotation;
    }

    void Update()
    {
        Quaternion targetRotation = cameraTransform.rotation;

        currentRotation = Quaternion.Slerp(
            currentRotation,
            targetRotation,
            Time.deltaTime * followSpeed
        );

        transform.rotation = currentRotation;
    }
}
