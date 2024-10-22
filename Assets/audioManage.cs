using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManage : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioMainMenu audioManager;
    public AudioSource mainTitlePlayer;
    public AudioSource src;
    public AudioClip mouseButton, tutorialPages, titleTheme;

    public GameObject tutorialManage;
    
    void Start()
    {
        mainTitlePlayer = audioManager.titleScreen;

        src = audioManager.pagesTurnMouse;
    }

    // Update is called once per frame
    void Update()
    {
        titleScreenPlay();
        mouseButtonTurn();
    }
    public void tutorialPageTurnButton() {
        src.clip = tutorialPages;
        src.Play();
    }
    public void mouseButtonTurn() {
        if (Input.GetKey(KeyCode.Mouse1)) {
            src.clip = mouseButton;
            src.Play();
        }
        
    }
    public void titleScreenPlay() {
        
        
    }
}
