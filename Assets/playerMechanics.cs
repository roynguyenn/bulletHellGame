using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class playerMechanics : MonoBehaviour
{
    
    public GameObject player;
    public GameObject health;
    public HungerBar hunger;
    public float onHitInvulDuration;    
    private float invulTimer = 0f;
    public int combo = 0;
    public int hungerIncrease = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        invulTimer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.E)){
            clearEnemies();
        }
        if (invulTimer >= onHitInvulDuration)
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
        }

    }
    
    public void OnCollisionEnter2D(Collision2D collision)
    {   
        
        if (collision.gameObject.tag == "Bullet")
        {
            combo = 0;
            hunger.DecreaseHunger(10);
            gameObject.layer = LayerMask.NameToLayer("Invulnerable");
            invulTimer = 0f;
        }

        if(collision.gameObject.tag == "Item"){
            combo += 1;
            if (combo > 3){
                hungerIncrease += 1;
            }
            hunger.IncreaseHunger(hungerIncrease);
        }

        if(collision.gameObject.tag == "Enemy"){
            combo = 0;
            hunger.DecreaseHunger(10);
            gameObject.layer = LayerMask.NameToLayer("Invulnerable");
            invulTimer = 0f;
        }

    }

    public void clearEnemies(){
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies){
            Destroy(enemy);
        }
    }

}
