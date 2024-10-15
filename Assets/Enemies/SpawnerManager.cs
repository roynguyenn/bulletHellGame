using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    private double _time;
    private double _period = 2;
    private int _maxEnemiesToSpawn = 1;
    private double _spawnChance = 1;

    private Dictionary<GameObject, double> _enemyPrefabs = new Dictionary<GameObject, double>();

    void Start()
    {
        LoadEnemyPrefabs();
    }

    private void LoadEnemyPrefabs()
    {
        _enemyPrefabs.Add(Resources.Load<GameObject>("Enemies/BasicEnemy"), 0);
        _enemyPrefabs.Add(Resources.Load<GameObject>("Enemies/SprintingEnemy"), 1);
    }

    private GameObject PickRandomEnemyPrefab()
    {
        double totalWeight = 0;
        foreach (var weight in _enemyPrefabs.Values)
            totalWeight += weight;

        double randomValue = UnityEngine.Random.value * totalWeight;
        foreach (var entry in _enemyPrefabs)
        {
            if (randomValue < entry.Value)
                return entry.Key;
            randomValue -= entry.Value;
        }
        return null;
    }

    private GameObject CreateEnemyInstance(GameObject enemyPrefab)
    {
        Vector3 spawnPosition = GetRandomSpawnPosition(enemyPrefab);
        GameObject enemyInstance = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        return enemyInstance;
    }

    private Vector3 GetRandomSpawnPosition(GameObject enemyPrefab)
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null)
            return Vector3.zero;

        float camHeight = mainCamera.orthographicSize * 2;
        float camWidth = camHeight * mainCamera.aspect;

        SpriteRenderer spriteRenderer = enemyPrefab.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
            return Vector3.zero;

        Vector2 spriteSize = spriteRenderer.size;
        float xOffset = spriteSize.x / 2;
        float yOffset = spriteSize.y / 2;
        float padding = 1.0f;

        Vector3 spawnPosition;
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        Debug.Log(camHeight + " " + camWidth + " " + xOffset + " " + yOffset + " " + padding);
        float spawnDistance = Mathf.Sqrt(Mathf.Pow(camWidth / 2, 2) + Mathf.Pow(camHeight / 2, 2)) + Mathf.Max(xOffset, yOffset) + padding;
        float randomAngle = UnityEngine.Random.Range(0f, 2 * Mathf.PI);

        spawnPosition = playerPosition + new Vector3(Mathf.Cos(randomAngle) * spawnDistance, Mathf.Sin(randomAngle) * spawnDistance, 0);

        return spawnPosition;
    }
    
    void Update()
    {
        if (_time > _period)
        {
            _time = 0;
            if (UnityEngine.Random.value < _spawnChance)
            {
                int enemiesToSpawn = UnityEngine.Random.Range(1, _maxEnemiesToSpawn + 1);
                for (int i = 0; i < enemiesToSpawn; i++)
                {
                    GameObject randomEnemyPrefab = PickRandomEnemyPrefab();
                    if (randomEnemyPrefab != null)
                    {
                        GameObject newEnemy = CreateEnemyInstance(randomEnemyPrefab);
                        Debug.Log($"Spawned enemy: {newEnemy.GetComponent<Enemy>().Name}");
                    }
                }
            }
        }
        _time += Time.deltaTime;
    }
}
