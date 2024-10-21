using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Renderer map;

    public Dictionary<GameObject, float> itemList = new Dictionary<GameObject, float>();
    public GameObject player;
    private GameObject spawnedItem;
    public GameObject item;
    private float timer = 0f;
    public float spawnRate;
    
    // Start is called before the first frame update
    void Start()
    {
        // itemList.Add(Resources.Load<GameObject>("Item Types/Burger"), 70);
        itemList.Add(Resources.Load<GameObject>("Item Types/Shrimp"), 29);
        //itemList.Add(Resources.Load<GameObject>("Item Types/Tiramisu"), 1);
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnRate){
            Instantiate(pickRandomItem(), randomSpawn(map), Quaternion.identity);
            timer = 0f;
        }
    }

    public Vector3 randomSpawn(Renderer renderer){
        float maxX = renderer.bounds.max.x;
        float minX = renderer.bounds.min.x;
        float offset = 10f;
        float radius = (maxX - minX)/2 - offset;

        return Random.insideUnitCircle * radius;
    }

    public GameObject pickRandomItem(){
        float totalWeight = 0f;
        foreach (var weight in itemList.Values){
            totalWeight += weight;
        }

        float randomWeight = Random.Range(0, totalWeight);
        
        float cumulativeWeight = 0f;
        foreach (var item in itemList){
            cumulativeWeight += item.Value;

            if (randomWeight <= cumulativeWeight){
                return item.Key;
            }
            
        }
        return null;
    }
    
}
