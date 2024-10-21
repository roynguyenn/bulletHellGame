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
    private int hungerIncrease = 0;
    public float gameTimer = 0f;

    // For speed item
    public movementScript movement;
    float originalSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        originalSpeed = movement.movespeed;
    }

    // Update is called once per frame
    void Update()
    {
        invulTimer += Time.deltaTime;
        gameTimer += Time.deltaTime;
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
            hungerIncrease = 0;
            hunger.DecreaseHunger(10);
            gameObject.layer = LayerMask.NameToLayer("Invulnerable");
            invulTimer = 0f;
        }

        if (collision.gameObject.tag == "Item"){
            combo += 1;
            ItemScript item = collision.gameObject.GetComponent<ItemScript>();

            if (combo > 3){
                hungerIncrease += 1;
            }

            if (item.itemtype == ItemScript.ItemTypes.Shrimp){
                speedBoost(1);
                Invoke("cancelSpeedBoost", 5f);
            }

            if (item.itemtype == ItemScript.ItemTypes.Tiramisu){
                clearEnemies();
            }
            hunger.IncreaseHunger(item.hungerVal + hungerIncrease);
        }

        if(collision.gameObject.tag == "Enemy"){
            combo = 0;
            hungerIncrease = 0;
            hunger.DecreaseHunger(10);
            gameObject.layer = LayerMask.NameToLayer("Invulnerable");
            invulTimer = 0f;
        }

    }

    public void speedBoost(float boostAmount){
        movement.movespeed += boostAmount;
    }

    public void cancelSpeedBoost(){
        movement.movespeed = originalSpeed;
    }

    public void clearEnemies(){
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var enemy in enemies){
            Destroy(enemy);
        }
    }
}
