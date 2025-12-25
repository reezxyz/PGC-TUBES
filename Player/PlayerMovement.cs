using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 4f;
    public float crouchSpeed = 2f;
    public float gravity = -9.8f;

    public float standHeight = 1.8f;
    public float crouchHeight = 1.0f;

    private CharacterController controller;
    private Vector3 velocity;
    public bool IsCrouching { get; private set; }
    private bool insideVent;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        controller.height = standHeight;
        controller.center = new Vector3(0, standHeight / 2f, 0);
    }

    void Update()
    {
        HandleCrouch();
        HandleMovement();
        HandleGravity();
    }

    void HandleMovement()
    {
        float speed = IsCrouching ? crouchSpeed : walkSpeed;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
    }

    void HandleCrouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            // Jika player sedang di dalam vent dan MAU berdiri → TOLAK
            if (insideVent && IsCrouching)
                return;

            // Toggle crouch
            IsCrouching = !IsCrouching;

            if (IsCrouching)
            {
                controller.height = crouchHeight;
                controller.center = new Vector3(0, crouchHeight / 2f, 0);
            }
            else
            {
                controller.height = standHeight;
                controller.center = new Vector3(0, standHeight / 2f, 0);
            }
        }
    }

    void HandleGravity()
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Vent"))
            insideVent = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Vent"))
            insideVent = false;
    }
}
