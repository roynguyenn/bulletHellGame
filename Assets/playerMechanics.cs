using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMechanics : MonoBehaviour
{
    public GameObject health;
    public healthBarHearts healthScript;
    public float onHitInvulDuration;
    private float timer = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        healthScript = health.GetComponent<healthBarHearts>();
    }

    // Update is called once per frame
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
        
        if (collision.gameObject.tag == "Bullet")
        {
            gameObject.layer = LayerMask.NameToLayer("Invulnerable");
            timer = 0f;
            healthScript.TookDamage(20f);

        }
    }
}
