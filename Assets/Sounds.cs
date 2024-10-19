using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sounds : MonoBehaviour
{   
    public AudioClip fightingMusic;
    public AudioSource clip;
    // Start is called before the first frame update
    void Start()
    {
        clip.PlayOneShot(fightingMusic);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
