using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform playerCamera;
    public float speed = 5;
    float sensitivity = 2;
    float rotationY;
    float rotationX;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        playerMovement();
        cameraMovement();
    }

    void playerMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
       
        controller.Move(transform.right * x * speed * Time.deltaTime);
        controller.Move(transform.forward * z * speed * Time.deltaTime);
    }

    void cameraMovement()
    {
        
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        rotationX -= mouseX;
        transform.Rotate(Vector3.up * mouseX);


        rotationY -= mouseY;
        rotationY = Mathf.Clamp(rotationY, -90f, 90f);

       

        playerCamera.localRotation = Quaternion.Euler(rotationY, 0f, 0f);
        transform.localRotation = Quaternion.Euler(0f, -rotationX, 0f);
    }
}
