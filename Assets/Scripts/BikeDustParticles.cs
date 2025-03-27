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
        //hitParticles.SetActive(false);
    }

    private void ActivateDustParticles()
    {
        isLevelStarted = true;
       // Debug.Log("activated dust");
    }

    
        private void OnTriggerStay(Collider other)
        {
            //Debug.Log("triggered");
            if(isLevelStarted)
            {
                hitParticlesInstance = Instantiate(hitParticles, transform.position, Quaternion.identity);
                Destroy(hitParticlesInstance.gameObject, 1);
               // hitParticles.SetActive(true);
            }
        }
    
}
