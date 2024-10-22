using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenu : MonoBehaviour
{
    // Start is called before the first frame update

    
    public GameObject timer;
    public GameObject titleSCreen;

    public GameObject gamerunning;
    void Start()
    {
        
        Time.timeScale = 0;
        if (StateManager.Instance != null && StateManager.Instance.hasLaunchedMainMenu)
        {
            // Skip the main menu if it has already been shown once
            titleSCreen.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            // Show the main menu on first launch
            titleSCreen.SetActive(true);
            Time.timeScale = 0f;

            if (StateManager.Instance != null)
            {
                StateManager.Instance.hasLaunchedMainMenu = true; // Update state to indicate the main menu has been displayed
            }
        }
        if (titleSCreen.activeSelf) {
            gamerunning.SetActive(false);
            timer.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void startGame() {
        Time.timeScale = 1;
        timer.SetActive(true);
        gamerunning.SetActive(true);
        titleSCreen.SetActive(false);

    }
    public void quitGame() {
        Application.Quit();
    }
    
}
