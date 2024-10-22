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
            
            if (shootTimer >= firingRate && inRange(player))
            {
                Fire(Aim(player));
                shootTimer = 0f;
            }
        }
        else if(spawnerType == SpawnerTypes.Spread)
        {
            if (shootTimer >= firingRate)
            {
                SpreadShot(spreadCount);
                shootTimer = 0f;
            }
        }
        
    }

    public void Fire(Vector3 direction)
    {
        spawnedBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        spawnedBullet.GetComponent<BulletScript>().movespeed = bulletSpeed;
        spawnedBullet.GetComponent<BulletScript>().bulletLife = bulletLife;
        spawnedBullet.GetComponent<BulletScript>().bulletDirection = direction;
        spawnedBullet.GetComponent<BulletScript>().player = player;
        // spawnedBullet.transform.rotation = transform.rotation;
        // spawnedBullet.transform.Rotate(0,0,-90f);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float offset = -90f;
        spawnedBullet.transform.rotation = Quaternion.Euler(0, 0, angle + offset);
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

    public Vector3 Aim(GameObject target)
    {
        Vector3 targetDirection = transform.position - target.transform.position;
        // float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        // float offset = 180f;
        // transform.rotation = Quaternion.Euler(0, 0, angle + offset);
        return -targetDirection.normalized;
    }

    public bool inRange(GameObject player){
        Vector3 playerPos = player.transform.position;
        float distance = (playerPos - gameObject.transform.position).magnitude;

        return distance <= targetRange;
    }
}
