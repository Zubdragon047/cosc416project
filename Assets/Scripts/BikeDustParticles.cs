using UnityEngine;

public class BikeDustParticles : MonoBehaviour
{
    [SerializeField] private GameObject hitParticles;
    private GameObject hitParticlesInstance;
    [SerializeField] private InputManager inputManager;
    private bool isLevelStarted = false;

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
        }
    
}
