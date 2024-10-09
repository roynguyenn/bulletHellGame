using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    enum SpawnerTypes { Rotate, Aim, Spread }

    // Variables for any spawner type
    [SerializeField] private SpawnerTypes spawnerType;
    public GameObject bullet;
    public GameObject player;
    private GameObject spawnedBullet;
    public float bulletLife = 8f;
    public float bulletSpeed = 5f;
    private float firingRate = 1f;
    private float timer = 0f;

    // Spread type variables
    public int spreadCount;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (spawnerType == SpawnerTypes.Rotate)
        {
            transform.Rotate(0f, 0f, 1f);
            
        }
        else if (spawnerType == SpawnerTypes.Aim)
        {
            Aim(player);
        }
        else if(spawnerType == SpawnerTypes.Spread)
        {
            if (timer >= firingRate)
            {
                SpreadShot(spreadCount);
                timer = 0f;
            }
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
        spawnedBullet.GetComponent<BulletScript>().bulletDirection = transform.right;
    }

    public void SpreadShot(int shotCount)
    {

        Vector3 originalDirection = transform.right;

        Quaternion rotation = Quaternion.Euler(0, 0, 360f / shotCount);
        Vector3 rotatedVector = originalDirection;
        for (int i = 0; i < shotCount; i++)
        {
            spawnedBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            spawnedBullet.GetComponent<BulletScript>().movespeed = bulletSpeed;
            spawnedBullet.GetComponent<BulletScript>().bulletLife = bulletLife;
            spawnedBullet.GetComponent<BulletScript>().bulletDirection = originalDirection;

            rotatedVector = rotation * originalDirection;
            originalDirection = rotatedVector;
        }
    
    }

    public void Aim(GameObject target)
    {
        Vector3 targetDirection = transform.position - target.transform.position;

        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        float offset = 180f;
        transform.rotation = Quaternion.Euler(0, 0, angle + offset);
    }
}
