using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public UnityEvent<Vector3> OnMove = new UnityEvent<Vector3>();
    public UnityEvent<int> OnRotate = new UnityEvent<int>();
    public UnityEvent<bool> OnRun = new UnityEvent<bool>();
    public UnityEvent OnPauseMenu = new();
    public UnityEvent OnLevelStart = new();
    bool isRunning = false;

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
            input += Vector3.forward;
        if (Input.GetKey(KeyCode.S))
            input += Vector3.back;
        if (Input.GetKey(KeyCode.LeftArrow))
            input += Vector3.left;
        if (Input.GetKey(KeyCode.RightArrow))
            input += Vector3.right;
        if (Input.GetKey(KeyCode.A))
            rotate = 1;
        if (Input.GetKey(KeyCode.D))
            rotate = -1;
        if(Input.GetKey(KeyCode.Space))
        {
            isRunning = true;
            OnLevelStart?.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OnPauseMenu?.Invoke();
        }

        OnRun?.Invoke(isRunning);
        OnMove?.Invoke(input);
        OnRotate?.Invoke(rotate);
    }
}
