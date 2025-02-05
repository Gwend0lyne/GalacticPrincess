using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public Rigidbody rg;
    public float forwardMoveSpeed;
    public float backwardMoveSpeed;
    public float steerSpeed;

    private float inputX;
    private float inputY;
    void Update() // Get keyboard inputs
    {
        inputY = Input.GetAxis("Vertical");
        inputX = Input.GetAxis("Horizontal");
    }
   
    void FixedUpdate() // Apply physics here
    {
        float speed = inputY > 0 ? forwardMoveSpeed : backwardMoveSpeed;
        if (inputY == 0) speed = 0;
        rg.AddForce(this.transform.right * speed, ForceMode.Acceleration);
        // Steer
        float rotation = inputX * steerSpeed * Time.fixedDeltaTime;
        transform.Rotate(0, rotation, 0, Space.World);
    }
}