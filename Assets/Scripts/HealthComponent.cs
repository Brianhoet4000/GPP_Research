using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int m_Health;
    private int m_MaxHealth = 100;
    public UIHealth m_UIHealth;
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
        //make sure health can't go below 0
        m_Health = Mathf.Clamp(m_Health, 0, m_MaxHealth);
        m_UIHealth.SetHealth(m_Health);

        if (m_Health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
