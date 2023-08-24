using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_MoveSpeed = 12.0f;
    [SerializeField] private float m_InputDeadZone = 0.01f;

    private Rigidbody m_RigidBody;

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
    }
}
