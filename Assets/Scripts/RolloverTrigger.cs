using UnityEngine;

public class RolloverTrigget : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float flipUprightForce = 1f;
    [SerializeField] private GameObject explosionParticles;
    private GameObject explosionParticlesInstance;
    private void OnTriggerStay(Collider other)
    {
        //explosionParticlesInstance = Instantiate(explosionParticles, other.transform.position, Quaternion.identity);
       // Destroy(explosionParticlesInstance.gameObject, 1);
        rb.linearVelocity = Vector3.zero;
        rb.AddTorque(new Vector3(0, 0, -flipUprightForce), ForceMode.Impulse);
        //explosionParticlesInstance = Instantiate(explosionParticles, other.transform.position, Quaternion.identity);
        //Destroy(explosionParticlesInstance.gameObject, 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        explosionParticlesInstance = Instantiate(explosionParticles, gameObject.transform.position, Quaternion.identity);
        Destroy(explosionParticlesInstance.gameObject, 1);
    }
}
