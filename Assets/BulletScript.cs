using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    enum BulletTypes { Normal, Bounce, Wave }

    // Variables for all bullet types
    [SerializeField] private BulletTypes bulletType;
    public float movespeed = 5f;
    public float bulletLife = 8f;
    public Vector3 bulletDirection = new Vector3(0,0,0);
    private float timer = 0f;

    // Variables for Bounce type
    public int bounceCap;
    private int bounceCount = 0;

    // Variables for Wave type
    public float amplitude;
    public float frequency;

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
        } else if(bulletType == BulletTypes.Bounce && bounceCount >= bounceCap)
        {
            Destroy(gameObject);
            bounceCount = 0;
        } else if(bulletType == BulletTypes.Wave)
        {
            Vector3 perpendicular = new Vector3(bulletDirection.y, -bulletDirection.x, 0);
            Vector3 moveY = perpendicular * Mathf.Sin(Time.time * frequency) * amplitude;

            transform.position += (moveY + bulletDirection) * movespeed * Time.deltaTime;
            
        }

        transform.position += movespeed * Time.deltaTime * bulletDirection;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision");
        if (bulletType != BulletTypes.Bounce && collision.gameObject.tag != "Bullet")
        {
            Destroy(gameObject);
        }
        if (bulletType == BulletTypes.Bounce && collision.gameObject.tag != "Bullet")
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
