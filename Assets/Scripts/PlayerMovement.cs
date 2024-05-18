using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed;
    public float jumpHeight = 3.0f;
    private Vector3 velocity;
    public float gravity = -9.81f;
    private Animator animator;
    public bool isGrounded;

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

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(speed * Time.deltaTime * move);

        Vector3 localVelocity = transform.InverseTransformDirection(controller.velocity);
        float velocityMagnitude = new Vector2(localVelocity.x, localVelocity.z).magnitude;

        // Animations
        animator.SetFloat("Velocity", velocityMagnitude);
        animator.SetBool("isGrounded", isGrounded);
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
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}
