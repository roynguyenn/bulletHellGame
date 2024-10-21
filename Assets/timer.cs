using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class timer : MonoBehaviour
{
    public GameObject text;
    public bool timerRunning = true;
    private float timers = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timerRunning) {
            timers += Time.deltaTime;
            updateTimerText();
        }
    }
    public void updateTimerText() {
        int minutes = Mathf.FloorToInt(timers / 60);
        int seconds = Mathf.FloorToInt(timers % 60);
        text.GetComponent<TextMeshProUGUI>().text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
