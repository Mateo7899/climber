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

    }

    void HandleMovement()
    {
        IsGrounded = characterController.isGrounded;

        if(externalVelocity.y > -0.05f )
    }
}
