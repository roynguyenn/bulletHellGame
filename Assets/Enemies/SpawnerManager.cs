using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    private double _time = 0;
    private double _period = 1;

    private Dictionary<GameObject, double> _enemyPrefabs = new Dictionary<GameObject, double>();

    void Start()
    {
        LoadEnemyPrefabs();
    }

    private void LoadEnemyPrefabs()
    {
        _enemyPrefabs.Add(Resources.Load<GameObject>("Enemies/BasicEnemy"), 1);
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
        float padding = 2.0f;

        float xOffset = (spriteSize.x / 2) + padding;
        float yOffset = (spriteSize.y / 2) + padding;

        Vector3 spawnPosition;
        int side = UnityEngine.Random.Range(0, 4);

        switch (side)
        {
            case 0:
                spawnPosition = new Vector3(-camWidth / 2 - xOffset, UnityEngine.Random.Range(-yOffset / 2, yOffset / 2), 0);
                break;
            case 1:
                spawnPosition = new Vector3(camWidth / 2 + xOffset, UnityEngine.Random.Range(-yOffset / 2, yOffset / 2), 0);
                break;
            case 2:
                spawnPosition = new Vector3(UnityEngine.Random.Range(-xOffset / 2, xOffset / 2), camHeight / 2 + yOffset, 0);
                break;
            case 3:
                spawnPosition = new Vector3(UnityEngine.Random.Range(-xOffset / 2, xOffset / 2), -camHeight / 2 - yOffset, 0);
                break;
            default:
                spawnPosition = Vector3.zero;
                break;
        }

        return spawnPosition;
    }

    void Update()
    {
        if (_time > _period)
        {
            _time = 0;
            GameObject randomEnemyPrefab = PickRandomEnemyPrefab();
            if (randomEnemyPrefab != null)
            {
                GameObject newEnemy = CreateEnemyInstance(randomEnemyPrefab);
                Debug.Log($"Spawned enemy: {newEnemy.GetComponent<Enemy>().Name}");
            }
        }
        _time += Time.deltaTime;
    }
}
