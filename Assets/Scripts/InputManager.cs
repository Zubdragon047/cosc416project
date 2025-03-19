using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public UnityEvent<Vector3> OnMove = new UnityEvent<Vector3>();
    public UnityEvent<int> OnRotate = new UnityEvent<int>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 input = Vector3.zero;
        int rotate = 0;
        if (Input.GetKey(KeyCode.W))
            input += transform.forward;
        if (Input.GetKey(KeyCode.S))
            input -= transform.forward;
        if (Input.GetKey(KeyCode.LeftArrow))
            input -= transform.right;
        if (Input.GetKey(KeyCode.RightArrow))
            input += transform.right;
        if (Input.GetKey(KeyCode.A))
            rotate = 1;
        if (Input.GetKey(KeyCode.D))
            rotate = -1;

        OnMove?.Invoke(input);
        OnRotate?.Invoke(rotate);
    }
}
