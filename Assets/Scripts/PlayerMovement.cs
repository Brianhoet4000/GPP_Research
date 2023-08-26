using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_MoveSpeed = 12.0f;
    [SerializeField] private float m_JumpForce = 10.0f;  // Jump force value
    [SerializeField] private float m_InputDeadZone = 0.01f;

    private Rigidbody m_RigidBody;
    private bool m_IsGrounded = true;  // Flag to track if the player is grounded

    private void Awake()
    {
        m_RigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float movementX = Input.GetAxis("Horizontal");
        float movementZ = Input.GetAxis("Vertical");

        if (Mathf.Abs(movementX) < m_InputDeadZone) movementX = 0f;
        if (Mathf.Abs(movementZ) < m_InputDeadZone) movementZ = 0f;

        Vector3 cameraRight = Camera.main.transform.right;
        Vector3 cameraForward = Camera.main.transform.forward;

        cameraRight.y = 0f;
        cameraForward.y = 0f;
        cameraRight.Normalize();
        cameraForward.Normalize();

        Vector3 movement = (cameraRight * movementX + cameraForward * movementZ) * m_MoveSpeed;

        m_RigidBody.velocity = new Vector3(movement.x, m_RigidBody.velocity.y, movement.z);

        m_IsGrounded = IsGrounded();
        
        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && m_IsGrounded)
        {
            print("Jumped");
            Jump();
        }
    }

    private void Jump()
    {
        m_RigidBody.AddForce(Vector3.up * m_JumpForce, ForceMode.Impulse);
        m_IsGrounded = false;  // Set the grounded state to false after jumping
    }

    private bool IsGrounded()
    {
        float raycastDistance = 0.1f;  // Adjust as needed
        Vector3 raycastOrigin = transform.position + Vector3.up * 0.1f;  // Slightly above the player's position
        return Physics.Raycast(raycastOrigin, Vector3.down, raycastDistance);
    }
}
