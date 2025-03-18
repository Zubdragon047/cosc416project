using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float forwardAcceleration = 5f;
    [SerializeField] private float sidewaysAcceleration = 2f;
    [SerializeField] private float maxSpeed = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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

}
