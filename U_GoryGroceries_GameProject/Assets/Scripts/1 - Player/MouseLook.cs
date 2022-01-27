using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float ySensitivity;
    [SerializeField] private float xSensitivity;

    [SerializeField] Transform cam;
    [SerializeField] Transform orientation;

    float mouseX;
    float mouseY;

    float multiplier = 0.01f;

    float xRotation;
    float yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
    }

    private void Update()
    {
        PlayerInput();

        cam.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    void PlayerInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * xSensitivity * multiplier;
        xRotation += mouseY * ySensitivity * multiplier;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
    }
}
