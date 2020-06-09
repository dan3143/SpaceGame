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
    
    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetButton("Left"))
        {
            transform.Rotate(0, 0, rotationSpeed);
        }
        if (Input.GetButton("Right"))
        {
            transform.Rotate(0, 0, -rotationSpeed);
        }
        if (Input.GetButton("Jump"))
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
}
