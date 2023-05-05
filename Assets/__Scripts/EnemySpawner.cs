using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRadius = 50f;
    public float circleRadius = 10f;
    public float spawnDelay = 5f;
    public int enemiesPerWave = 5;
    public float waveDelay = 10f;

    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);

            for (int i = 0; i < enemiesPerWave; i++)
            {
                Vector3 spawnPosition = GetRandomSpawnPosition();
                GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

                Vector3 directionToPlayer = (playerTransform.position - enemy.transform.position).normalized;

                Vector3 perpendicular = Vector3.Cross(directionToPlayer, Vector3.up);

                Vector3[] directions = new Vector3[]
                {
                    directionToPlayer,
                    Quaternion.AngleAxis(60f, Vector3.up) * perpendicular,
                    Quaternion.AngleAxis(-60f, Vector3.up) * perpendicular,
                };

                Vector3 randomDirection = directions[Random.Range(0, directions.Length)];

                Vector3 startPosition = spawnPosition + randomDirection * circleRadius;
                enemy.transform.position = startPosition;
                
            }
            enemiesPerWave += Random.Range(1, 5);
            //Debug.Log(enemiesPerWave.ToString());
            yield return new WaitForSeconds(waveDelay);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector2 randomCircle = Random.insideUnitCircle.normalized * spawnRadius;
        Vector3 randomPosition = new Vector3(randomCircle.x, 0f, randomCircle.y);

        Vector3 playerPosition = playerTransform.position;
        playerPosition.y = 0f;

        randomPosition += playerPosition;
        return randomPosition;
    }
}


