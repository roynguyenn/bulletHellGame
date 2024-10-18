using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class healthBarHearts : MonoBehaviour
{
    public float health;
    public Image[] hearts;

    public float healthBarIndicator = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        hearts[0].fillAmount = healthBarIndicator;
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    public void TookDamage(float damage) {
        health -= damage;

        health = Mathf.Clamp(health, 0, 100);

        hearts[0].fillAmount = 0.5f * health/100f; 


    }
}
