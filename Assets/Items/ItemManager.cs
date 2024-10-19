using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    enum ItemTypes{}
    [SerializeField] private ItemTypes itemtype;
    public Renderer map;

    public GameObject player;
    private GameObject spawnedItem;
    public GameObject item;
    private float timer = 0f;
    public float spawnRate;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnRate){
            Instantiate(item, randomSpawn(map), Quaternion.identity);
            timer = 0f;
        }

    }

    public Vector3 randomSpawn(Renderer renderer){
        float maxX = renderer.bounds.max.x;
        float minX = renderer.bounds.min.x;
        float offset = 2f;
        float radius = (maxX - minX - offset)/2;

        return Random.insideUnitCircle * radius;
    }
}
