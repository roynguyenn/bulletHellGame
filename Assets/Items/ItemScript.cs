using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    
    public enum ItemTypes{Nuggets, Ranch, Ramen, Tiramisu, Shrimp, Donut, Burger}
    public ItemTypes itemtype;
    public GameObject player;
    public SpriteRenderer sprite;
    public Color blinkColor;
    public Color originalColor;
    private float flashTime = 5f;
    public float lifeTime;
    private float timer;

    public int hungerVal;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        originalColor = sprite.color;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifeTime - flashTime){
            StartCoroutine(Blink());
        }
        if(timer >= lifeTime){

            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision){
        
        if (collision.gameObject.tag == "Player"){
            Destroy(gameObject);
        }
    }

    public IEnumerator Blink()
    {
        float elapsedTime = 0f;
        float blinkTime = 0.5f;
        while (elapsedTime < flashTime){
            sprite.color = blinkColor;
            yield return new WaitForSeconds(blinkTime);

            sprite.color = originalColor;
            yield return new WaitForSeconds(blinkTime);

            elapsedTime += 2 * blinkTime;
        }

        sprite.color = originalColor;
    }
}
