using System;
using TMPro;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    private bool timeStarted = false;
    private float startTime;
    [SerializeField] private TextMeshProUGUI currentTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) {
            timeStarted = true;
            startTime = Time.time;
        }

        if (timeStarted) {
            var guiTime = Time.time - startTime;

            int minutes = (int)(guiTime / 60);
            int seconds = (int)(guiTime % 60);
            int fraction = (int)((guiTime * 100) % 100);

            String time = String.Format ("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction);
            currentTime.SetText(time);
        }   
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            timeStarted = false;

            var guiTime = Time.time - startTime;

            int minutes = (int)(guiTime / 60);
            int seconds = (int)(guiTime % 60);
            int fraction = (int)((guiTime * 100) % 100);

            String time = String.Format ("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction);
            currentTime.SetText(time);
        }
    }
}
