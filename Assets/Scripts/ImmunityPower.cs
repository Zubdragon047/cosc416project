using UnityEngine;

public class ImmunityPower : MonoBehaviour
{
    [SerializeField] private int rotateSpeed;

    void Start()
    {
        rotateSpeed = 1;
    }

    void Update()
    {
        transform.Rotate(0, rotateSpeed, 0, Space.World);
    }

    //can add OnTriggerEnter for when player hits it? would use instance of GameManager

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
