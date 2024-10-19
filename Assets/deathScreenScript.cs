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

    public bool callOnce = false;
    public bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        deathPauseScreenUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       GameOver();
       PauseScreen();
       callOnceButton();
    }
    public void GameOver() {
        if (hunger.GetComponent<HungerBar>().CurrentHunger <= 0) {
            Debug.Log("dead");
            Time.timeScale = 0;
            deathPauseScreenUI.SetActive(true);
        
        }

    }
    public void PauseScreen() {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused) {
            deathPauseScreenUI.SetActive(true);
            Time.timeScale = 0;
            isPaused = true;
        } else if (Input.GetKeyDown(KeyCode.Escape) && isPaused){
            deathPauseScreenUI.SetActive(false);
            isPaused = false;
            Time.timeScale = 1;

        }

    }
    public void callOnceButton() {
        if (!callOnce) {
            restartScreenButtonsBack.SetActive(false);
            restartScreenButtonsNext.SetActive(false);
            callOnce = true;
        }
    }
    
    
    
}
