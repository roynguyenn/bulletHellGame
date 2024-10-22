using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class playerMechanics : MonoBehaviour
{
    public SpriteRenderer sprite;
    public GameObject player;
    public GameObject health;
    public HungerBar hunger;
    private float onHitInvulDuration = 2f;    
    private float itemInvulDuration = 6f;
    public int combo = 0;
    private int hungerIncrease = 0;
    public float gameTimer = 0f;
    public Color blinkColor;
    public Color originalColor;
    private Coroutine blinking;

    // For speed item
    public movementScript movement;
    float originalSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        originalSpeed = movement.movespeed;
        originalColor = sprite.color;
    }

    // Update is called once per frame
    void Update()
    {
        // invulTimer += Time.deltaTime;
        gameTimer += Time.deltaTime;
        // if (invulTimer >= onHitInvulDuration)
        // {
        //     gameObject.layer = LayerMask.NameToLayer("Default");
        // }

    }
    
    public void OnCollisionEnter2D(Collision2D collision)
    {   
        
        if (collision.gameObject.tag == "Bullet")
        {
            combo = 0;
            hungerIncrease = 0;
            hunger.DecreaseHunger(10);
            blinking = StartCoroutine(Blink());
            StartCoroutine(stopBlink());
            gameObject.layer = LayerMask.NameToLayer("Invulnerable");
            Invoke("cancelInvulnerability", onHitInvulDuration);
        }

        if (collision.gameObject.tag == "Item"){
            combo += 1;
            ItemScript item = collision.gameObject.GetComponent<ItemScript>();

            if (combo > 3){
                hungerIncrease += 1;
            }

            if (item.itemtype == ItemScript.ItemTypes.Ramen){
                speedBoost(1);
                Invoke("cancelSpeedBoost", 5f);
            }

            if (item.itemtype == ItemScript.ItemTypes.Tiramisu){
                gameObject.layer = LayerMask.NameToLayer("ItemInvulnerable");
                Invoke("cancelInvulnerability", itemInvulDuration);
            }

            if(item.itemtype == ItemScript.ItemTypes.Shrimp){
                clearEnemies();
            }

            hunger.IncreaseHunger(item.hungerVal + hungerIncrease);
            
        }

        if(collision.gameObject.tag == "Enemy"){
            combo = 0;
            hungerIncrease = 0;
            hunger.DecreaseHunger(10);
            blinking = StartCoroutine(Blink());
            StartCoroutine(stopBlink());
            gameObject.layer = LayerMask.NameToLayer("Invulnerable");
            Invoke("cancelInvulnerability", onHitInvulDuration);
        }

    }

    public void speedBoost(float boostAmount){
        movement.movespeed += boostAmount;
    }

    public void cancelSpeedBoost(){
        movement.movespeed = originalSpeed;
    }

    public void cancelInvulnerability(){
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    public void clearEnemies(){
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var enemy in enemies){
            Destroy(enemy);
        }
    }

    public IEnumerator Blink()
    {
        float elapsedTime = 0f;
        float blinkTime = 0.1f;
        while (elapsedTime < onHitInvulDuration){
            sprite.color = blinkColor;
            yield return new WaitForSeconds(blinkTime);

            sprite.color = originalColor;
            yield return new WaitForSeconds(blinkTime);

            elapsedTime += 2 * blinkTime;
        }

        sprite.color = originalColor;
    }

    public IEnumerator stopBlink(){
        yield return new WaitForSeconds(onHitInvulDuration);
        StopCoroutine(blinking);
        sprite.color = originalColor;
    }
}
