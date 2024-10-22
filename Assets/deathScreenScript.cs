    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseDeathScreen : MonoBehaviour
{
    public HungerBar hunger;
    public GameObject deathPauseScreenUI;

    public GameObject restartScreenButtonsNext;

    public GameObject restartScreenButtonsBack;
    public GameObject mainShooter;

    public GameObject pauseButton;

    public GameObject continueButton;

    public GameObject deathScreens;
    public GameObject timer;
    public GameObject winSCreen;

    public GameObject mainMenuButton;

    public GameObject tutorialButton;
    public GameObject restartButton;

    public GameObject newMainMenu;
    public bool callOnce = false;
    public bool isPaused = false;

    private bool hasWon = false;
    // Start is called before the first frame update
    void Start()
    {
        deathPauseScreenUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
       GameOver();
       callOnceButton();
        if (!hasWon && hunger.GetComponent<HungerBar>().CurrentHunger == hunger.GetComponent<HungerBar>().stages[4])
        {
            restartButton.SetActive(true);
            newMainMenu.SetActive(true);
            TriggerWinningScreen();
        }
    }
    public void GameOver() {
        if (hunger.GetComponent<HungerBar>().CurrentHunger <= 0) {
            Debug.Log("dead");
            Time.timeScale = 0;
            mainShooter.SetActive(false);
            deathPauseScreenUI.SetActive(true);
            continueButton.SetActive(false);
            deathScreens.SetActive(true);
            pauseButton.SetActive(false); 
            timer.SetActive(false);
            mainMenuButton.SetActive(false);
            tutorialButton.SetActive(false);

        }

    }
    public void PauseScreen() {
        
        deathPauseScreenUI.SetActive(true);
        deathScreens.SetActive(false);
        pauseButton.SetActive(false);
        continueButton.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;    
        timer.SetActive(false);
        mainMenuButton.SetActive(true);
        tutorialButton.SetActive(true);
    }
    public void callOnceButton() {
        if (!callOnce) {
            restartScreenButtonsBack.SetActive(false);
            restartScreenButtonsNext.SetActive(false);
            callOnce = true;
        }
    }
    public void continueGame() {
        deathPauseScreenUI.SetActive(false);
        pauseButton.SetActive(true);
        continueButton.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
    }
    public void TriggerWinningScreen()
    {
        
        if (!winSCreen.activeSelf)
        {
            restartButton.SetActive(true);
            newMainMenu.SetActive(true);
            winSCreen.SetActive(true);
            Time.timeScale = 0f; 
            hasWon = true; 
            
        }
    }
    
}
