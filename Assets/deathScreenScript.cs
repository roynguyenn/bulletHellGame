using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseDeathScreen : MonoBehaviour
{
    public healthBarHearts health;
    public GameObject deathPauseScreenUI;
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
       
    }
    public void GameOver() {
        if (health.health <= 0) {
            Debug.Log("dead");
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
    
    
    
}
