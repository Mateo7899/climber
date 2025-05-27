using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class Nuevocontroldelpersonaje : MonoBehaviour
{

    [Header("Movimiento")]

    public float Walkspeed = 4f;
    public float SprintSpeed = 6f;
    public float jumpHeight = 2f;
    public float rotationSpeed = 10f;
    public float mouseSensitivity = 1f;
    public float gravity = -20f;

    [Header("Referenciacion")]
    public Transform cameraTransform;
    public Animator animator;
    private CharacterController characterController;
    private Vector3 Velocity;
    private float currentSpeed;
    private float yaw;
    private Vector3 externalVelocity = Vector3.zero;

    public bool IsMoving { get; private set; }

    public Vector2 CurrentInput { get; private set; }

    public bool IsGrounded { get; private set; }

    public float CurrentYaw => yaw;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleRotation();
        updateAnimator();
    }

    void HandleMovement()
    {
        IsGrounded = characterController.isGrounded;

        if (IsGrounded && Velocity.y < 0)
        {
            if (externalVelocity.y > -0.05f && externalVelocity.y < 0.05f)
                Velocity.y = 0;
            else
                Velocity.y = -2f;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical).normalized;
        IsMoving = inputDirection.magnitude > 0.1f;

        Vector3 moveDirection = Vector3.zero;

        if (IsMoving)
        {
            moveDirection = Quaternion.Euler(0f, cameraTransform.eulerAngles.y, 0f) * inputDirection;
            bool isSprinting = Input.GetKey(KeyCode.LeftShift);
            currentSpeed = isSprinting ? SprintSpeed : Walkspeed;
        }

        if (Input.GetButtonDown("Jump") && IsGrounded)
        {
            Velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator?.SetBool("isJumping", true);
        }

        Velocity.y += gravity * Time.deltaTime;

        Vector3 finalMovement = (moveDirection * currentSpeed + externalVelocity) * Time.deltaTime;
        finalMovement.y += Velocity.y * Time.deltaTime;

        characterController.Move(finalMovement);

        if (IsGrounded && Velocity.y < 0f)
        {
            animator?.SetBool("isJumping", false);
        }
    }

    void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        yaw += mouseX;

        if (IsMoving)
        {
            cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, Quaternion.Euler(0f, yaw, 0f), rotationSpeed * Time.deltaTime);

        }
    }

    void updateAnimator()
    {
        float SpeedPercent = IsMoving ? (currentSpeed == SprintSpeed ? 1f : 0.5f) : 0f;
        animator?.SetFloat("Speed", SpeedPercent, 0.1f, Time.deltaTime);
        animator?.SetBool("IsGrounded", IsGrounded);
        animator?.SetFloat("VerticalSpeed", Velocity.y);
    }

    
}
