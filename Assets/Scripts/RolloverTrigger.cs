using UnityEngine;

public class RolloverTrigget : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float flipUprightForce = 1f;
    private void OnTriggerStay(Collider other)
    {
        rb.linearVelocity = Vector3.zero;
        rb.AddTorque(new Vector3(0, 0, -flipUprightForce), ForceMode.Impulse);
    }
}
