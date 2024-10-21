using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TutorialPages : MonoBehaviour
{
    public GameObject[] tutorialPages;
    public GameObject pauseScreen;
    public GameObject backButton;
    public GameObject nextButton;
    public GameObject exitButton;
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
        pauseScreen.SetActive(true);
        exitButton.SetActive(false);
        backButton.SetActive(false);
        nextButton.SetActive(false);
        Time.timeScale = 1;
    }
}
