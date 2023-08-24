using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]private float sensX;
    [SerializeField]private float sensY;

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
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0 , yRotation, 0);
    }
}