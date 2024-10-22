using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class TutorialPages : MonoBehaviour
{
    public GameObject[] tutorialPages;
    public GameObject pauseScreen;
    public GameObject backButton;
    public GameObject nextButton;
    public GameObject exitButton;
    public GameObject deathScreen;
    public GameObject mainMenu;

    public GameObject inTheGame;
    public GameObject mainMenuButton;

    public GameObject pauseButton;

    public int pageIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        exitButton.SetActive(false);
        backButton.SetActive(false);
        nextButton.SetActive(false);
        for (int i = 0; i < tutorialPages.Length; i++) {
            tutorialPages[i].SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showFirstPage() {
        Time.timeScale = 0;
        pauseScreen.SetActive(false);
        backButton.SetActive(true);
        nextButton.SetActive(true);
        pageIndex = 0;
        for (int i = 0; i < tutorialPages.Length; i++) {
            if (i ==0) {
                tutorialPages[i].SetActive(true);

            }
            else {
                tutorialPages[i].SetActive(false);
            }
           
        }
    }

    public void nextPage() {
        tutorialPages[pageIndex].SetActive(false);
        pageIndex+=1;
        pageIndex = Mathf.Clamp(pageIndex,0,6);
        if (pageIndex == tutorialPages.Length-1) {
            nextButton.SetActive(false);
            exitButton.SetActive(true);
        }
        tutorialPages[pageIndex].SetActive(true);
    }
    public void backPage() {
        if(pageIndex >= 1) {
            tutorialPages[pageIndex].SetActive(false);
            pageIndex -= 1;
            pageIndex = Mathf.Clamp(pageIndex, 0, 6);
            tutorialPages[pageIndex].SetActive(true);
            nextButton.SetActive(true);
            exitButton.SetActive(false);
        }
            
        
    }
    public void exitTutorial() {
        for (int i = 0; i < tutorialPages.Length; i++) {
            tutorialPages[i].SetActive(false);
        }
        if (mainMenu.activeSelf) {
            mainMenu.SetActive(true);
        } else {
            mainMenu.SetActive(false);
        }
        pauseScreen.SetActive(true);
        deathScreen.SetActive(false);
        exitButton.SetActive(false);
        backButton.SetActive(false);
        nextButton.SetActive(false);
        if (mainMenu.activeSelf) {
            pauseScreen.SetActive(false);
        }
        Time.timeScale = 1;
    }
    public void mainMenuScreen() {
        mainMenu.SetActive(true);
        pauseScreen.SetActive(false);
        pauseButton.SetActive(false);
        deathScreen.SetActive(false);
        
    }
}
