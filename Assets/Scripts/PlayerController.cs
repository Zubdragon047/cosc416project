using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float forwardAcceleration = 20f;
    [SerializeField] private float sidewaysAcceleration = 7f;
    [SerializeField] private float maxSpeed = 15f;
    [SerializeField] private float rotationTorque = 3f;

    // for the speed boost & mud pit
    private float originalForwardAcceleration;
    private float originalMaxSpeed;
    private bool isSpeedBoosted = false;
    private bool isSlowed = false;

    // for the immunity power Update
    private bool immunityActivated = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalForwardAcceleration = forwardAcceleration;
        originalMaxSpeed = maxSpeed;

        rb.centerOfMass = new Vector3(0, -0.4f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MovePlayer(Vector3 input)
    {
        Vector3 inputforward = new(input.x, 0, 0);
        Vector3 inputsideways = new(0, 0, input.z);
        if (rb.linearVelocity.x < maxSpeed && rb.linearVelocity.x > -maxSpeed)
        {
            rb.AddForce(inputforward * forwardAcceleration);
        }
        rb.AddForce(inputsideways * sidewaysAcceleration);
    }

    public void RotatePlayer(int rotate)
    {
        rb.AddTorque(new Vector3(0, 0, rotate * rotationTorque));
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered with: " + other.gameObject.name); // Debugging

        if(other.CompareTag("ImmunityPower"))
        {
            immunityActivated = true;
            Destroy(other.gameObject);
            ApplyImmunityBoost();
        }
        
        if (other.CompareTag("SpeedBoost"))
        {
            ApplySpeedBoost();
        }
        else if (other.CompareTag("MudPit"))
        {
            ApplyMudSlowdown();
        }
    }

    private void ApplyImmunityBoost()
    {
        forwardAcceleration = originalForwardAcceleration * 1.2f; // Increase acceleration
        maxSpeed = originalMaxSpeed * 1.2f; // Increase max speed

        Vector3 boostDirection = new(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        rb.AddForce(boostDirection.normalized * 10f, ForceMode.Impulse);

        StartCoroutine(ResetSpeedAfterDelayFromImmunity(4f));
        
    }

    private void ApplySpeedBoost()
    {
        if (!isSpeedBoosted)
        {
            isSpeedBoosted = true;
            forwardAcceleration = originalForwardAcceleration * 1.5f; // Increase acceleration
            maxSpeed = originalMaxSpeed * 1.5f; // Increase max speed

            Vector3 boostDirection = new(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            rb.AddForce(boostDirection.normalized * 10f, ForceMode.Impulse);

            StartCoroutine(ResetSpeedAfterDelay(2f));
        }
    }

    private void ApplyMudSlowdown()
    {
        if(immunityActivated)
        {
            return;
        }
        else if (!isSlowed)
        {
            isSlowed = true;
            forwardAcceleration = originalForwardAcceleration * 0.75f; // Reduce acceleration
            maxSpeed = originalMaxSpeed * 0.5f; // Reduce max speed

            StartCoroutine(ResetSpeedAfterDelay(2f));
        }
    }

    private IEnumerator ResetSpeedAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        forwardAcceleration = originalForwardAcceleration;
        maxSpeed = originalMaxSpeed;
        isSpeedBoosted = false;
        isSlowed = false;
    }

    private IEnumerator ResetSpeedAfterDelayFromImmunity(float delay)
    {
        yield return new WaitForSeconds(delay);
        forwardAcceleration = originalForwardAcceleration;
        maxSpeed = originalMaxSpeed;
        immunityActivated = false;
    }

}
