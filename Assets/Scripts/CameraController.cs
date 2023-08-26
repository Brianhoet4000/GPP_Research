using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour
{
    [SerializeField]private float m_SensX;
    [SerializeField]private float m_SensY;

    public Transform orientation;

    private float xRotation;
    private float yRotation;
  
    void Start()
    {
        //hide cursor while in game
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
  
    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * m_SensX;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * m_SensY;

        yRotation += mouseX;
        
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0 , yRotation, 0);
    }
}