using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    enum BulletTypes { Normal, Bounce }
    [SerializeField] private BulletTypes bulletType;
    public float movespeed = 5f;
    public float bulletLife = 8f;
    public Vector3 bulletDirection = new Vector3(0,0,0);
    private float timer = 0f;

    public int bounceCap;
    private int bounceCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(bulletType == BulletTypes.Normal && timer >= bulletLife)
        {
            Destroy(gameObject);
            timer = 0f;
        } else if(bulletType == BulletTypes.Bounce && bounceCount >= bounceCap)
        {
            Destroy(gameObject);
            bounceCount = 0;
        }

        transform.position += movespeed * Time.deltaTime * bulletDirection;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (bulletType == BulletTypes.Normal)
        {
            Destroy(gameObject);
        }
        if (bulletType == BulletTypes.Bounce)
        {
            if (collision.gameObject.name == "player")
            {
                Destroy(gameObject);
            }
            bulletDirection = Vector3.Reflect(bulletDirection, collision.contacts[0].normal);
            bounceCount += 1;
        }
    }
}
