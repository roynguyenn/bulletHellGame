using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    enum BulletTypes { Normal, Bounce, Wave, Homing }
    
    // Variables for all bullet types
    [SerializeField] private BulletTypes bulletType;

    public GameObject player;
    public Vector3 bulletDirection = new Vector3(0, 0, 0);
    public float movespeed = 5f;
    public float bulletLife = 8f;
    private float timer = 0f;

    // Variables for Bounce type
    public int bounceCap;
    private int bounceCount = 0;

    // Variables for Wave type
    public float amplitude;
    public float frequency;

    // Variables for Homing Type
    public float turnRate;
    public Rigidbody2D rb;
    public float homingWait;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(bulletType != BulletTypes.Bounce && timer >= bulletLife)
        {
            Destroy(gameObject);
            timer = 0f;
        }
        else if(bulletType == BulletTypes.Bounce && bounceCount >= bounceCap)
        {
            Destroy(gameObject);
            bounceCount = 0;
        }
        else if(bulletType == BulletTypes.Wave)
        {
            Vector3 perpendicular = new Vector3(bulletDirection.y, -bulletDirection.x, 0);
            Vector3 moveY = perpendicular * Mathf.Sin(Time.time * frequency) * amplitude;

            transform.position += (moveY + bulletDirection) * movespeed * Time.deltaTime;
            
        }
        else if (bulletType == BulletTypes.Homing)
        {
            
            Vector3 playerPosition = player.transform.position;
            Vector3 direction = (playerPosition - transform.position).normalized;

            float rotateAmount = -Vector3.Cross(direction, transform.right).z;
            rb.angularVelocity = rotateAmount * turnRate;
            rb.velocity = bulletDirection * movespeed;

            if (timer >= homingWait)
            {
                rb.velocity = transform.right * movespeed;
            }
            
        }

        if (bulletType != BulletTypes.Homing)
        {
            transform.position += movespeed * Time.deltaTime * bulletDirection;
        }
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {

        if (bulletType != BulletTypes.Bounce && collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "Item")
        {
            Destroy(gameObject);
        }
        if (bulletType == BulletTypes.Bounce && collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "Item")
        {
            if (collision.gameObject.name == "player")
            {
                Destroy(gameObject);
            }   
            bulletDirection = Vector3.Reflect(bulletDirection, collision.GetContact(0).normal);
            bounceCount += 1;
            
            
        }
    }
}
