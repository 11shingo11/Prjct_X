using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;
    public List<Transform> spawnAreas;
    public float spawnIntervalMin = 1f;
    public float spawnIntervalMax = 3f;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(spawnIntervalMin, spawnIntervalMax));

            int randomEnemyIndex = Random.Range(0, enemyPrefabs.Count);
            GameObject randomEnemyPrefab = enemyPrefabs[randomEnemyIndex];


            int randomSpawnAreaIndex = -1; // »нициализируем индекс случайной области с недопустимым значением

            // ѕовтор€ем выбор случайной области, пока не найдем подход€щие координаты
            while (randomSpawnAreaIndex == -1 || !CheckCoordinates(randomSpawnAreaIndex))
            {
                randomSpawnAreaIndex = Random.Range(0, spawnAreas.Count);
            }
            Transform randomSpawnArea = spawnAreas[randomSpawnAreaIndex];
            Debug.Log("Coordinates of selected spawn area: " + randomSpawnArea.transform.position);

            Vector3 randomPosition = GetRandomPositionInsideArea(randomSpawnArea);
            GameObject enemy = Instantiate(randomEnemyPrefab, randomPosition, Quaternion.identity);

            // ƒополнительные действи€ по спавну врага...
        }
    }


    bool CheckCoordinates(int spawnAreaIndex)
    {
        // ѕолучаем координаты выбранной области
        Vector3 spawnAreaPosition = spawnAreas[spawnAreaIndex].position;

        // ѕровер€ем координаты х и z на соответствие диапазону
        if (spawnAreaPosition.x >= -68f && spawnAreaPosition.x <= 68f &&
            spawnAreaPosition.z >= -74f && spawnAreaPosition.z <= 77f)
        {
            //  оординаты наход€тс€ в допустимом диапазоне
            return true;
        }
        else
        {
            //  оординаты не наход€тс€ в допустимом диапазоне
            return false;
        }
    }

    private Vector3 GetRandomPositionInsideArea(Transform area)
    {
        Collider areaCollider = area.GetComponent<Collider>();
        if (areaCollider != null)
        {
            Vector3 randomPosition = Vector3.zero;
            if (areaCollider is BoxCollider)
            {
                BoxCollider boxCollider = (BoxCollider)areaCollider;
                randomPosition = area.position + new Vector3(
                    Random.Range(-boxCollider.size.x / 2f, boxCollider.size.x / 2f),
                    -1.3f, // ”становить позицию по оси Y на 0
                    Random.Range(-boxCollider.size.z / 2f, boxCollider.size.z / 2f)
                );
            }
            else if (areaCollider is SphereCollider)
            {
                SphereCollider sphereCollider = (SphereCollider)areaCollider;
                Vector2 randomCircle = Random.insideUnitCircle * sphereCollider.radius;
                randomPosition = area.position + new Vector3(
                    randomCircle.x,
                    -1f, // ”становить позицию по оси Y на 0
                    randomCircle.y
                );
            }

            return randomPosition;
        }

        return area.position;
    }
}




//public class EnemySpawner : MonoBehaviour
//{
//    public List<GameObject> enemyPrefabs;
//    public float spawnRadius = 50f;
//    public float circleRadius = 10f;
//    public float spawnDelay = 5f;
//    public int enemiesPerWave = 5;
//    public float waveDelay = 10f;

//    private Transform playerTransform;

//    private void Start()
//    {
//        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
//        StartCoroutine(SpawnEnemies());
//    }

//    private IEnumerator SpawnEnemies()
//    {
//        while (true)
//        {
//            yield return new WaitForSeconds(spawnDelay);

//            for (int i = 0; i < enemiesPerWave; i++)

//            {
//                Vector3 spawnPosition = GetRandomSpawnPosition();
//                GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

//                Vector3 directionToPlayer = (playerTransform.position - enemy.transform.position).normalized;

//                Vector3 perpendicular = Vector3.Cross(directionToPlayer, Vector3.up);

//                Vector3[] directions = new Vector3[]
//                {
//                    directionToPlayer,
//                    Quaternion.AngleAxis(60f, Vector3.up) * perpendicular,
//                    Quaternion.AngleAxis(-60f, Vector3.up) * perpendicular,
//                };

//                Vector3 randomDirection = directions[Random.Range(0, directions.Length)];

//                Vector3 startPosition = spawnPosition + randomDirection * circleRadius;
//                enemy.transform.position = startPosition;

//            }
//            enemiesPerWave += Random.Range(1, 5);
//            //Debug.Log(enemiesPerWave.ToString());
//            yield return new WaitForSeconds(waveDelay);
//        }
//    }

//    private Vector3 GetRandomSpawnPosition()
//    {
//        Vector2 randomCircle = Random.insideUnitCircle.normalized * spawnRadius;
//        Vector3 randomPosition = new Vector3(randomCircle.x, 0f, randomCircle.y);

//        Vector3 playerPosition = playerTransform.position;
//        playerPosition.y = 0f;

//        randomPosition += playerPosition;
//        return randomPosition;
//    }
//}


