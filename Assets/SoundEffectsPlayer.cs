using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SoundEffectsPlayer : MonoBehaviour
{
    // Start is called before the first frame update

    public Audio AudioManager;
    public AudioSource mainThemePlayer;
    public AudioSource src;
    public AudioClip mouseButton, tutorialPages, gameTheme;

    public GameObject deathScreen;

    public GameObject tutorialManage;
    
    void Start()
    {
        mainThemePlayer = AudioManager.mainThemeAudio;

        src = AudioManager.tutorialMouseAudio;
    }

    // Update is called once per frame
    void Update()
    {
        gameThemePlay();
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
    public void gameThemePlay() {
        
        for (int i = 0; i < tutorialManage.GetComponent<TutorialPages>().tutorialPages.Length; i++  ) {
            if (!tutorialManage.GetComponent<TutorialPages>().tutorialPages[i].activeSelf
                && !deathScreen.activeSelf) {
                if (!mainThemePlayer.isPlaying) {
                mainThemePlayer.clip = gameTheme;
                mainThemePlayer.Play();
                }
            }
        } 
        for (int i = 0; i < tutorialManage.GetComponent<TutorialPages>().tutorialPages.Length; i++  ) {
            if (tutorialManage.GetComponent<TutorialPages>().tutorialPages[i].activeSelf
                || deathScreen.activeSelf) {
                    mainThemePlayer.Stop();
                }
        }
    }
}
