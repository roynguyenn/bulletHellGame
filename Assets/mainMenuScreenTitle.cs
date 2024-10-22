using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuScreenTitle : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject mainMenuUI;

    
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void quitApp() {
        Application.Quit();
    }

    public void startGame() {
        mainMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }
}
