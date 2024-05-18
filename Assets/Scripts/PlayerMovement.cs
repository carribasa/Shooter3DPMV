using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed;
    public float jumpHeight = 3.0f; // Variable para la fuerza de salto

    private Vector3 velocity;
    public float gravity = -9.81f;
    private Animator animator;

    public bool isGrounded;

    public float groundDistance;
    public LayerMask groundMask;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check ground
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Horizontal movement
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(speed * Time.deltaTime * move);

        // Local velocity for animation
        Vector3 localVelocity = transform.InverseTransformDirection(controller.velocity);
        float velocityMagnitude = new Vector2(localVelocity.x, localVelocity.z).magnitude;
        animator.SetFloat("Velocity", velocityMagnitude);

        // Set the grounded parameter
        animator.SetBool("isGrounded", isGrounded);

        // Set the vertical velocity parameter
        animator.SetFloat("VerticalVelocity", velocity.y);

        // Jump
        Jump();

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // Calcular la velocidad de salto
            animator.SetTrigger("Jump");
        }
    }
}
