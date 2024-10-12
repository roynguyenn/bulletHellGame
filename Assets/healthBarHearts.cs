using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class healthBarHearts : MonoBehaviour
{
    public int health = 5;
    public int numberOfHearts = 5;
    public Image[] hearts;
    public Image fullHeart;
    // Start is called before the first frame update
    void Start()
    {
        health = 5;
        numberOfHearts = 5;
        if (health > numberOfHearts) {
            health = numberOfHearts;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < hearts.Length; i++) {
            if (i < health) {
                hearts[i].fillAmount = 1;
            } else {
                hearts[i].fillAmount = 0;
            }
        }
    }
}
