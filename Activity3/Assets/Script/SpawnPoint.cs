using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject enemyPrefab; // Drag and drop the enemy prefab here
    public float spawnInterval = 2f; // The interval between each spawn

    private void Start()
    {
        StartCoroutine(SpawnEnemyCoroutine());
    }

    private IEnumerator SpawnEnemyCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            Vector2 spawnPosition = new Vector2(transform.position.x, transform.position.y - 1f);
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
