using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    enum SpawnerTypes { Aim, Spread, }

    // Variables for any spawner type
    [SerializeField] private SpawnerTypes spawnerType;
    public GameObject bullet;
    public GameObject player;
    private GameObject spawnedBullet;

    public float bulletLife = 8f;
    public float bulletSpeed = 5f;
    public float firingRate = 1f;
    private float shootTimer = 0f;
    public float rotationSpeed;
    public float lifeTime;
    private float lifeTimer = 0f;
    public bool rotate;
    public float targetRange;
    

    // Spread type variables
    public int spreadCount;

    
    // Start is called before the first frame update
    void Start()
    {
        if (!player){
            player = GameObject.FindWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer += Time.deltaTime;
        lifeTimer += Time.deltaTime;
        if (lifeTimer >= lifeTime){
            Destroy(gameObject);
        }
        if (rotate)
        {
            transform.Rotate(0f, 0f, rotationSpeed);
            
        }

        if (spawnerType == SpawnerTypes.Aim)
        {
            Aim(player);
        }
        else if(spawnerType == SpawnerTypes.Spread)
        {
            if (shootTimer >= firingRate)
            {
                SpreadShot(spreadCount);
                shootTimer = 0f;
            }
        }
        
        

        if (shootTimer >= firingRate && inRange(player))
        {
            Fire();
            shootTimer = 0f;
        }
    }

    public void Fire()
    {
        spawnedBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        spawnedBullet.GetComponent<BulletScript>().movespeed = bulletSpeed;
        spawnedBullet.GetComponent<BulletScript>().bulletLife = bulletLife;
        spawnedBullet.GetComponent<BulletScript>().bulletDirection = transform.right;
        spawnedBullet.GetComponent<BulletScript>().player = player;
        spawnedBullet.transform.rotation = transform.rotation;
        spawnedBullet.transform.Rotate(0,0,-90f);
    }

    public void SpreadShot(int shotCount)
    {

        Vector3 originalDirection = transform.right;
        float circleDeg = 360f;
        Quaternion rotation = Quaternion.Euler(0, 0, circleDeg / shotCount);
        Quaternion bulletRotate = transform.rotation * Quaternion.Euler(0,0,-90f);

        Vector3 rotatedVector = originalDirection;
        for (int i = 0; i < shotCount; i++)
        {
            
            spawnedBullet = Instantiate(bullet, transform.position, bulletRotate);
            spawnedBullet.GetComponent<BulletScript>().movespeed = bulletSpeed;
            spawnedBullet.GetComponent<BulletScript>().bulletLife = bulletLife;
            spawnedBullet.GetComponent<BulletScript>().bulletDirection = originalDirection;
            // Can't assign scene object to prefab, so need to instantiate during runtime
            spawnedBullet.GetComponent<BulletScript>().player = player;

            bulletRotate *= rotation;
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

    public bool inRange(GameObject player){
        Vector3 playerPos = player.transform.position;
        float distance = (playerPos - gameObject.transform.position).magnitude;

        return distance <= targetRange;
    }
}
