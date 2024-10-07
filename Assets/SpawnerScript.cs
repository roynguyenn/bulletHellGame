using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    enum SpawnerTypes { Rotate, Aim }

    public GameObject bullet;
    public GameObject player;
    public float bulletLife = 8f;
    public float bulletSpeed = 5f;
    private float firingRate = 1f;
    private float timer = 0f;

    private GameObject spawnedBullet;
    [SerializeField] private SpawnerTypes spawnerType;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(spawnerType == SpawnerTypes.Rotate)
        {
            transform.Rotate(0f, 0f, 1f);
        } else if(spawnerType == SpawnerTypes.Aim)
        {
            Vector3 direction = transform.position - player.transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, angle + 180f);
        }
        

        if (timer >= firingRate)
        {
            Fire();
            timer = 0f;
        }
    }

    public void Fire()
    {
        spawnedBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        spawnedBullet.GetComponent<BulletScript>().movespeed = bulletSpeed;
        spawnedBullet.GetComponent<BulletScript>().bulletLife = bulletLife;
        spawnedBullet.transform.rotation = transform.rotation;
    }

    
}
