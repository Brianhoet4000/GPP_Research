using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    [SerializeField] private float m_Speed = 30.0f;
    [SerializeField] private float m_LifeTime = 6.0f;
    [SerializeField] private int m_Damage = 2;

    private void Awake()
    {
        Invoke("Kill", m_LifeTime);
    }

    private void FixedUpdate()
    {
        if (!WallDetection())
        {
            transform.position += transform.forward * Time.deltaTime * m_Speed;
        }
    }
    
    static string[] RAYCAST_MASK = new string[] { "Level" };
    bool WallDetection()
    {
        Ray collisionRay = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(collisionRay, out RaycastHit hit, Time.deltaTime * m_Speed, LayerMask.GetMask(RAYCAST_MASK)))
        {
            Kill();
            return true;
        }
        return false;
    }

    void Kill()
    {
        Destroy(gameObject);
    }

    public void SetDirection(Vector3 dir)
    {
        transform.forward = dir;
    }

    private void OnTriggerEnter(Collider other)
    {
        //make sure not to hit own team
        if (other.tag == tag)
        {
            return;
        }

        HealthComponent playerHealth = other.GetComponent<HealthComponent>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(m_Damage);
            Kill();
        }

    }
}