using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAccuracyController : MonoBehaviour
{
    private float m_TimeToNextHit = 0.0f;
    private const float m_BaseHitDelay = 2.5f;
    private float m_Timer;

    [SerializeField]
    private GameObject m_PlayerRef;
    private Rigidbody m_PlayerRigidBody;
    private float m_ExtraOffsetMinX = -3.5f;
    private float m_ExtraOffsetMaxX = 3.5f;
    private float m_ExtraOffsetMinY = 0.6f;
    private float m_ExtraOffsetMaxY = 1.3f;
    private float m_PlayerHeightOffset;
    private float m_PlayerWidthOffset;
    
    private void Awake()
    {
        m_PlayerRigidBody = m_PlayerRef.GetComponent<Rigidbody>();
    }

    void Update()
    {
        m_Timer += Time.deltaTime;
        CapsuleCollider playerCollider = m_PlayerRef.GetComponent<CapsuleCollider>();
        m_PlayerWidthOffset = playerCollider.radius;
        m_PlayerHeightOffset = playerCollider.height / 2.0f;
    }

    public Vector3 FindBulletDirection(Vector3 bulletPos)
    {
        Vector3 playerPos = m_PlayerRef.transform.position;
        Vector3 direction;

        if (m_Timer >= m_TimeToNextHit)
        {
            m_Timer = 0.0f;
            CalculateTimeToNextHit();
            direction = playerPos - bulletPos;  // Direction to hit the player
        }
        else
        {
            // Calculate miss offsets
            float missOffsetX = Random.Range(-m_ExtraOffsetMaxX, m_ExtraOffsetMaxX);  // Spread in X-axis
            float missOffsetY = Random.Range(m_ExtraOffsetMinY, m_ExtraOffsetMaxY);    // Spread in Y-axis
            float missOffsetZ = Random.Range(m_PlayerWidthOffset + m_ExtraOffsetMinX, m_PlayerWidthOffset + m_ExtraOffsetMaxX);

            // Apply miss offsets to create a fake position
            Vector3 fakePos = new Vector3(playerPos.x + missOffsetX, playerPos.y + missOffsetY, playerPos.z + missOffsetZ);
            direction = fakePos - bulletPos;  // Direction to miss the player
        }

        return direction;
    }

    private void CalculateTimeToNextHit()
    {
        float newDelay = m_BaseHitDelay * CalculateDistance() * CalculateVelocity();
        m_TimeToNextHit = newDelay;
    }

    private float CalculateDistance()
    {
        float distanceToTarget = Vector3.Distance(m_PlayerRef.transform.position, gameObject.transform.position);
        const float maxRange = 20.0f;
        const float midRange = 10.0f;

        // Default modifier if player is close to the shooter
        float modifier = 0.5f;

        if (distanceToTarget > midRange)
        {
            if (distanceToTarget > maxRange)
            {
                // Player is far away and is safer
                modifier = 1.0f;
            }
            else
            {
                // Player is slightly more in danger
                modifier = 0.8f;
            }
        }

        return modifier;
    }

    private float CalculateVelocity()
    {
        Vector3 vectorPlayerToEnemy = gameObject.transform.position - m_PlayerRef.transform.position;
        Vector3 velocityVect = m_PlayerRigidBody.velocity;

        float angle = Vector3.Angle(vectorPlayerToEnemy, velocityVect);

        const float closerAngle = 45.0f;
        const float furtherAngle = 160.0f;

        if (angle > furtherAngle)
        {
            return 1.0f;  // Player is running away from the enemy
        }

        if (angle < closerAngle)
        {
            return 0.5f;  // Player is running towards the enemy
        }

        return 0.7f;  // Standard amount
    }

}
