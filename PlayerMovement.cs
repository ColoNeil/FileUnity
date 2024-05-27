using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float sprintSpeed = 5.0f;
    public float gravityValue = -9.81f;
    public CharacterController controller;
    public Transform orientation;
    public Animator animator;

    private Vector3 playerVelocity;
    private bool isGrounded;
    private float currentSpeed;

    void Update()
    {
        // Check if the player is grounded
        isGrounded = controller.isGrounded;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Calculate movement direction
        Vector3 forward = orientation.forward;
        Vector3 right = orientation.right;

        forward.y = 0f;  // Keep only the horizontal component
        right.y = 0f;    // Keep only the horizontal component

        forward.Normalize();
        right.Normalize();

        Vector3 move = (right * Input.GetAxis("Horizontal") + forward * Input.GetAxis("Vertical")).normalized;

        // Determine sprint
        float targetSpeed = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) ? sprintSpeed : moveSpeed;
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed * move.magnitude, Time.deltaTime * 5f);

        // Move the player
        controller.Move(move * Time.deltaTime * currentSpeed);

        // Rotate the player to face the movement direction if moving
        if (move != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 360 * Time.deltaTime);
        }

        // Apply gravity
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        // Update the animator's speed parameter
        animator.SetFloat("Speed", currentSpeed);
    }
}
