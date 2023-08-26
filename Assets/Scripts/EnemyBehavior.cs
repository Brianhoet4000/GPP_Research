using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private GunFunctionality m_Gun;
    [SerializeField] private GameObject m_PlayerRef;
    [SerializeField] public float m_MaxDetectionRange = 0.0f;
    
    private bool m_HasDetectedPlayer = false;

    private void Awake()
    {
        m_Gun = GetComponentInChildren<GunFunctionality>();
    }

    private void FixedUpdate()
    {
        DetectPlayer(); // Detect before updating the gun's state

        m_Gun.SetCanShoot(m_HasDetectedPlayer);
        
        if (m_HasDetectedPlayer)
        {
            transform.LookAt(m_PlayerRef.transform);
        }
    }

    private void DetectPlayer()
    {
        Vector3 rayDirection = m_PlayerRef.transform.position - transform.position;
        Ray ray = new Ray(transform.position, rayDirection);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, m_MaxDetectionRange))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Level"))
            {
                m_HasDetectedPlayer = false;
            }
            else if (hit.collider.gameObject == m_PlayerRef)
            {
                m_HasDetectedPlayer = true;
            }
        }
        else
        {
            m_HasDetectedPlayer = false;
        }
    }
}