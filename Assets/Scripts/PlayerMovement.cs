using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed;

    private Vector3 velocity;
    public float gravity = -9.81f;

    public bool isGrounded;

    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;

    void Update()
    {
        // Check ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Horizontal movement
        Vector3 move = (transform.right * x) + (transform.forward * z);
        controller.Move(speed * Time.deltaTime * move);

        // Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
