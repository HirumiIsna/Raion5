using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 1f;

    private float sinceLastSpawn = 0f;

    [Header("Spawn Boundary")]
    public float spawnBoundaryY = 4f;
    public float spawnBoundaryX = 10f;

    void Update()
    {
        sinceLastSpawn += Time.deltaTime;

        if (sinceLastSpawn >= spawnInterval)
        {
            SpawnEnemy();
            sinceLastSpawn = 0f;
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null)
        {
            Debug.Log("Spawner Error: enemyPrefab is null!");
            return;
        }

        Camera cam = Camera.main;

        float randomY = Random.Range(-spawnBoundaryY, spawnBoundaryY);

        // Spawn di kanan kamera
        float xPos = cam.transform.position.x + spawnBoundaryX;
        float yPos = cam.transform.position.y + randomY;

        Vector3 spawnPosition = new Vector3(xPos, yPos, 0f);

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

}
