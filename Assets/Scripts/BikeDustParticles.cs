using UnityEngine;

public class BikeDustParticles : MonoBehaviour
{
    [SerializeField] private GameObject hitParticles;
    private GameObject hitParticlesInstance;
    [SerializeField] private InputManager inputManager;
    private bool isLevelStarted = false;

    [SerializeField] private GameObject mudParticles;
    private GameObject mudParticlesInstance;

    private void Awake()
    {
        inputManager.OnStartDust.AddListener(ActivateDustParticles);
    }

    private void ActivateDustParticles()
    {
        isLevelStarted = true;
    }

    
    private void OnTriggerStay(Collider other)
    {
        if(isLevelStarted)
        {
            hitParticlesInstance = Instantiate(hitParticles, transform.position, Quaternion.identity);
            Destroy(hitParticlesInstance.gameObject, 0.2f);
        }
        if(other.CompareTag("MudPit"))
        {
            mudParticlesInstance = Instantiate(mudParticles, transform.position, Quaternion.identity);
            Destroy(mudParticlesInstance.gameObject, 0.5f);
        }
        }
/*
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("MudPit"))
        {
            mudParticlesInstance = Instantiate(mudParticles, transform.position, Quaternion.identity);
            Destroy(mudParticlesInstance.gameObject, 1);
        }
    }

*/


}
