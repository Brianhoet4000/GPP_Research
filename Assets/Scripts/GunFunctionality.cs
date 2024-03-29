using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFunctionality : MonoBehaviour
{
    [SerializeField] private GameObject m_BulletTemplate = null;
    [SerializeField] private int m_ClipSize = 8;
    [SerializeField] private float m_FireRate = 1.0f;
    [SerializeField] private Transform m_FireSocket;

    private int m_CurrentAmmo = 0;
    private float m_FireTimer = 0.0f;
    private bool m_CanShoot = false;
    private bool m_IsReloading = false;
    private EnemyAccuracyController m_AccuracyController;

    //[SerializeField] private AudioSource m_FireSound;

    private void Awake()
    {
        m_CurrentAmmo = m_ClipSize;
        m_AccuracyController = gameObject.GetComponent<EnemyAccuracyController>();
    }

    void Update()
    {
        if(!m_CanShoot)
        {
            return;
        }

        //Handle fire timer countdown
        if (m_FireTimer > 0.0f)
        {
            m_FireTimer -= Time.deltaTime;
        }

        if (m_FireTimer <= 0.0f)
        {
            if(!m_IsReloading)
            {
                FireProjectile();
            }         
        }
    }

    private void FireProjectile()
    {       
        //no ammo, don't fire
        if (m_CurrentAmmo <= 0)
        {
            m_IsReloading = true;
            const float reloadTime = 2.0f;
            Invoke("Reload", reloadTime);
            return;
        }

        //no bullet to fire
        if (m_BulletTemplate == null)
        {
            return;
        }

        //consume a bullet
        --m_CurrentAmmo;
    
       GameObject newBullet = Instantiate(m_BulletTemplate, m_FireSocket.position, m_FireSocket.rotation);
       Bullet bulletComponent = newBullet.GetComponent<Bullet>();
       
       Vector3 direction = m_AccuracyController.FindBulletDirection(newBullet.transform.position);
       bulletComponent.SetDirection(direction);

        //set the time and respect the firerate
        m_FireTimer += 1.0f / m_FireRate;

        /*if (m_FireSound)
        {
            m_FireSound.Play();
        }*/
    }

    public void SetCanShoot(bool canShoot)
    {
        m_CanShoot = canShoot;
    }

    public void Reload()
    {
        m_CurrentAmmo = m_ClipSize;
        m_IsReloading = false;
    }
}
