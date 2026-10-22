
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 9f;
    public float jumpForce = 6f;

    public float mouseSensitivity = 0.15f;
    public float cameraDistance = 5f;
    public float cameraHeight = 1.5f;
    public float minCameraAngle = -20f;
    public float maxCameraAngle = 60f;
    public float rotationSpeed = 10f;

    private Rigidbody rb;
    private Camera playerCamera;

    private Vector2 movementInput;
    private float cameraYaw;
    private float cameraPitch = 15f;
    private bool isGrounded;
    private bool rotateWithCamera = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCamera = Camera.main;

        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        if (playerCamera != null)
            cameraYaw = playerCamera.transform.eulerAngles.y;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Keyboard.current == null)
            return;

        if (Keyboard.current.leftCtrlKey.wasPressedThisFrame ||
            Keyboard.current.rightCtrlKey.wasPressedThisFrame)
        {
            rotateWithCamera = !rotateWithCamera;
        }

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Mouse.current != null &&
            Mouse.current.leftButton.wasPressedThisFrame &&
            Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (Cursor.lockState == CursorLockMode.Locked &&
            Mouse.current != null)
        {
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();

            cameraYaw += mouseDelta.x * mouseSensitivity;
            cameraPitch -= mouseDelta.y * mouseSensitivity;

            cameraPitch = Mathf.Clamp(
                cameraPitch, minCameraAngle, maxCameraAngle
            );
        }

        movementInput = Vector2.zero;

        if (Keyboard.current.wKey.isPressed ||
            Keyboard.current.upArrowKey.isPressed)
            movementInput.y += 1;

        if (Keyboard.current.sKey.isPressed ||
            Keyboard.current.downArrowKey.isPressed)
            movementInput.y -= 1;

        if (Keyboard.current.aKey.isPressed ||
            Keyboard.current.leftArrowKey.isPressed)
            movementInput.x -= 1;

        if (Keyboard.current.dKey.isPressed ||
            Keyboard.current.rightArrowKey.isPressed)
            movementInput.x += 1;

        movementInput = Vector2.ClampMagnitude(movementInput, 1f);

        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        Quaternion cameraRotation =
            Quaternion.Euler(0f, cameraYaw, 0f);

        Vector3 forward = cameraRotation * Vector3.forward;
        Vector3 right = cameraRotation * Vector3.right;

        Vector3 direction =
            forward * movementInput.y +
            right * movementInput.x;

        float currentSpeed = walkSpeed;

        if (Keyboard.current != null &&
            (Keyboard.current.leftShiftKey.isPressed ||
             Keyboard.current.rightShiftKey.isPressed))
        {
            currentSpeed = runSpeed;
        }

        Vector3 velocity = direction * currentSpeed;

        rb.linearVelocity = new Vector3(
            velocity.x,
            rb.linearVelocity.y,
            velocity.z
        );

        if (rotateWithCamera && direction.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(direction, Vector3.up);

            rb.MoveRotation(Quaternion.Slerp(
                rb.rotation,
                targetRotation,
                rotationSpeed * Time.fixedDeltaTime
            ));
        }

        Vector3 rayOrigin =
            transform.position + Vector3.down * 0.45f;

        isGrounded = Physics.Raycast(
            rayOrigin,
            Vector3.down,
            out RaycastHit hit,
            0.2f
        ) && hit.collider.gameObject != gameObject;
    }

    void LateUpdate()
    {
        if (playerCamera == null)
            return;

        Vector3 target = transform.position +
                         Vector3.up * cameraHeight;

        Quaternion rotation =
            Quaternion.Euler(cameraPitch, cameraYaw, 0f);

        Vector3 offset =
            rotation * new Vector3(0f, 0f, -cameraDistance);

        playerCamera.transform.position = target + offset;
        playerCamera.transform.LookAt(target);
    }
}
