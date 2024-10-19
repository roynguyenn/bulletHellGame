using System.Collections;
using System.Collections.Generic;
using System.Threading;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class fullScreenTest : MonoBehaviour
{   
    private float hurtDisplayTime = 1.5f;
    private float hurtDisplayFadeOut = 0.5f;
    public HungerBar healthBar;
    [SerializeField] private Material myMaterial;
    private int screenIntensityProperty = Shader.PropertyToID("_fullscreenIntensity");
    public float fadeCD = 3f;
    private float timer = 0f;
    // Start is called before the first frame update
    
    void Start()
    {
       myMaterial.SetFloat(screenIntensityProperty, 1);
    }

    // Update is called once per frame
   
    void Update()
    {
        StartCoroutine(FadeIn());
        
        
    }
    public IEnumerator FadeIn()
    {
        while(healthBar.CurrentHunger <= 20f) {
            float elapsedTime = 0f;
            while (elapsedTime < hurtDisplayTime)
            {
                elapsedTime += Time.deltaTime;
                float lerpScreenIntensity = Mathf.Lerp(0.8f, 1f, elapsedTime / hurtDisplayTime);
                myMaterial.SetFloat(screenIntensityProperty, lerpScreenIntensity);
                yield return null;  
            }
        }
    
        
    }
}
