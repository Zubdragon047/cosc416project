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
    // for the speed boost
    [SerializeField] private float increasedAcceleration = 1.2f;
    [SerializeField] private float increasedSpeed = 1.2f;
    [SerializeField] private float SpeedBoostImpulse = 10f;
    // for the mud pit
    [SerializeField] private float reducedAcceleration = 0.75f;
    [SerializeField] private float reducedMaxSpeed = 0.75f;
    [SerializeField] private float rotationTorque = 3f;
    // for the immunity boost
    [SerializeField] private float immunityAcceleration = 1.2f;
    [SerializeField] private float immunitySpeed = 1.2f;
    [SerializeField] private float immunityBoostImpulse = 10f;

    // for the speed boost & mud pit
    private float originalForwardAcceleration;
    private float originalMaxSpeed;
    private bool isSpeedBoosted = false;
    private bool isSlowed = false;
    public bool running = false;

    // for the immunity power Update
    private bool immunityActivated = false;
    [SerializeField] private GameObject immunityParticles;

    // for dust particles
    [SerializeField] private GameObject dustParticles1;
    [SerializeField] private GameObject dustParticles2;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalForwardAcceleration = forwardAcceleration;
        originalMaxSpeed = maxSpeed;

        rb.centerOfMass = new Vector3(0, -0.2f, 0);
        dustParticles1.SetActive(false);
        dustParticles2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (running)
        {
            if (rb.linearVelocity.x < maxSpeed && rb.linearVelocity.x > -maxSpeed)
            {
                rb.AddForce(Vector3.right * forwardAcceleration);
            }
        }

        // for immunity particle effects
        if(immunityActivated)
        {
            immunityParticles.SetActive(true);
        }else{
            immunityParticles.SetActive(false);
        }
        
    }
/*
    private void OnCollisionStay(Collision collision)
    {
        if(running){
            dustParticles1.SetActive(true);
            dustParticles2.SetActive(true);
        }else{
            dustParticles1.SetActive(false);
            dustParticles2.SetActive(false);
        }
    }
    
    private void OnCollisionExit(Collision collision)
    {
            dustParticles1.SetActive(false);
            dustParticles2.SetActive(false);
    }
    */

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

    public void OnRun(bool isRunning)
    {
        running = isRunning;
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
        forwardAcceleration = originalForwardAcceleration * immunityAcceleration; // Increase acceleration
        maxSpeed = originalMaxSpeed * immunitySpeed; // Increase max speed

        Vector3 boostDirection = new(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        rb.AddForce(boostDirection.normalized * immunityBoostImpulse, ForceMode.Impulse);

        StartCoroutine(ResetSpeedAfterDelayFromImmunity(4f));
        
    }

    private void ApplySpeedBoost()
    {
        if (!isSpeedBoosted)
        {
            isSpeedBoosted = true;
            forwardAcceleration = originalForwardAcceleration * increasedAcceleration; // Increase acceleration
            maxSpeed = originalMaxSpeed * increasedSpeed; // Increase max speed

            Vector3 boostDirection = new(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            rb.AddForce(boostDirection.normalized * SpeedBoostImpulse, ForceMode.Impulse);

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
            forwardAcceleration = originalForwardAcceleration * reducedAcceleration; // Reduce acceleration
            maxSpeed = originalMaxSpeed * reducedMaxSpeed; // Reduce max speed

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
