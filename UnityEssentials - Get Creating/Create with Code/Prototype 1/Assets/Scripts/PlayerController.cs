using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
     [SerializeField] private float rpm;
    [SerializeField] private float horsePower =0;
    private float turnSpeed = 45.0f;
    private float horizontalInput;
    private float verticalInput;
    private Rigidbody playerRb;
    [SerializeField] GameObject centerOfMass;
    [SerializeField] TextMeshProUGUI speedometerText;
     [SerializeField] TextMeshProUGUI rpmText;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.centerOfMass = centerOfMass.transform.position;
    }

    void FixedUpdate()
    {
        // player inputs
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // we move the vehicle forward
        // transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        playerRb.AddRelativeForce(Vector3.forward * horsePower * verticalInput);
        speed = Mathf.RoundToInt(playerRb.velocity.magnitude * 2.237f); // for kph use 3.6
        speedometerText.SetText("Speed: " + speed + "MPH");
        rpm = (speed % 30) * 40;
        rpmText.SetText("RPM: " + rpm);

        // we turn the vehicle
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);
    }
}
