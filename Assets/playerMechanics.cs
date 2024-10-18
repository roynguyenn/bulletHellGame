using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMechanics : MonoBehaviour
{
    public healthBarHearts health;
    public float onHitInvulDuration;
    
    public double score = 0;
    public double combo = 1;
    public double increaseRate = 1;
    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate(){
        increaseScore();
    }
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= onHitInvulDuration)
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }
    
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision");
        
        if (collision.gameObject.tag == "Bullet")
        {
            gameObject.layer = LayerMask.NameToLayer("Invulnerable");
            combo = 1;
            increaseRate = 1;
            timer = 0f;
        }
    }

    public void increaseScore(){
        score += increaseRate;
        increaseRate += combo;
        combo *= 1.01;
    }
}
