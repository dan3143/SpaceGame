using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] GameObject fire;
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float maxMovementSpeed = 10f;
    [SerializeField] float deceleration = 0.5f;
    Rigidbody2D rb2;
    BluetoothService bluetoothService;
    bool rotatingRight = false;
    bool rotatingLeft = false;
    bool advancing = false;
    
    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        bluetoothService = BluetoothService.Instance;
    }
    
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android) {
            rotatingLeft = bluetoothService.IsButtonPressed("Left");
            rotatingRight = bluetoothService.IsButtonPressed("Right");
            advancing = bluetoothService.IsButtonPressed("Space");
        } else {
            rotatingLeft = Input.GetButton("Left");
            rotatingRight = Input.GetButton("Right");
            advancing = Input.GetButton("Jump");
        }
    }

    void FixedUpdate()
    {
        if (rotatingLeft)
        {
            transform.Rotate(0, 0, rotationSpeed);
        }
        if (rotatingRight)
        {
            transform.Rotate(0, 0, -rotationSpeed);
        }
        if (advancing)
        {
            Vector3 movement = transform.up * movementSpeed;
            rb2.AddForce(movement);
            if (rb2.velocity.magnitude > maxMovementSpeed)
                rb2.velocity = rb2.velocity.normalized * maxMovementSpeed;
            fire.SetActive(true);
        } else {
            fire.SetActive(false);
            rb2.velocity -= rb2.velocity * deceleration;
        }
    }

    void Message(string message)
    {
        rotatingLeft = message == "left_pressed";
        rotatingRight = message == "right_pressed";
        advancing = message == "x_pressed";
    }
}
