using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int m_Health;

    public int GetHealth()
    {
        return m_Health;
    }

    public void SetHealth(int health)
    {
        m_Health = health;
    }

    public void TakeDamage(int damage)
    {
        m_Health -= damage;
    }
}
