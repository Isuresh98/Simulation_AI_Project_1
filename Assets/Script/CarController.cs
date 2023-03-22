using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 30f;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float deceleration = 10f;
    [SerializeField] private float brakePower = 10f;
    [SerializeField] private float turnSpeed = 100f;

    private Rigidbody rb;
    private float inputHorizontal;
    private float inputVertical;
    private float currentSpeed;
    private CameraFollow cameraFollow;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0f, -0.5f, 0f);
        cameraFollow = Camera.main.GetComponent<CameraFollow>();
        cameraFollow.SetTarget(transform);
    }

    void FixedUpdate()
    {
        inputHorizontal = Input.GetAxis("Horizontal");
        inputVertical = Input.GetAxis("Vertical");

        if (inputVertical > 0)
        {
            currentSpeed = Mathf.Clamp(currentSpeed + (acceleration * Time.fixedDeltaTime), 0f, maxSpeed);
        }
        else if (inputVertical < 0)
        {
            currentSpeed = Mathf.Clamp(currentSpeed - (deceleration * Time.fixedDeltaTime), -maxSpeed / 2f, 0f);
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.fixedDeltaTime);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, brakePower * Time.fixedDeltaTime);
        }

        Vector3 movement = transform.forward * currentSpeed * inputVertical;
        rb.AddForce(movement, ForceMode.Acceleration);

        float turn = inputHorizontal * turnSpeed * Time.fixedDeltaTime * Mathf.Lerp(1f, 0.2f, rb.velocity.magnitude / maxSpeed);
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }
}
