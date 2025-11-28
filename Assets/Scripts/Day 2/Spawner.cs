using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public GameObject[] enemyPrefabs;      

    [Header("Spawn Chance (Optional)")]
    public float[] spawnWeights;            

    [Header("Spawn Settings")]
    public float spawnInterval = 1f;
    public float spawnBoundaryY = 4f;
    public float spawnBoundaryX = 10f;

    private float sinceLastSpawn = 0f;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;

        if (enemyPrefabs == null || enemyPrefabs.Length == 0)
            Debug.LogError("Spawner ERROR: enemyPrefabs kosong!");

        if (spawnWeights.Length != 0 && spawnWeights.Length != enemyPrefabs.Length)
        {
            Debug.LogWarning("spawnWeights panjangnya tidak sama dengan enemyPrefabs, akan abaikan weights.");
            spawnWeights = new float[0]; // disable weighted spawn
        }
    }

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
        if (enemyPrefabs.Length == 0)
            return;

        GameObject prefabToSpawn = ChooseEnemyPrefab();

        float randomY = Random.Range(-spawnBoundaryY, spawnBoundaryY);

        float xPos = cam.transform.position.x + spawnBoundaryX;
        float yPos = cam.transform.position.y + randomY;

        Vector3 spawnPosition = new Vector3(xPos, yPos, 0f);

        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
    }

    GameObject ChooseEnemyPrefab()
    {
        // kalau tidak ada weight → pilih acak biasa
        if (spawnWeights.Length == 0)
        {
            int r = Random.Range(0, enemyPrefabs.Length);
            return enemyPrefabs[r];
        }

        // weighted spawn (peluang berbeda)
        float total = 0f;
        foreach (float w in spawnWeights)
            total += Mathf.Max(0, w);

        float rand = Random.Range(0f, total);
        float sum = 0f;

        for (int i = 0; i < spawnWeights.Length; i++)
        {
            sum += Mathf.Max(0, spawnWeights[i]);
            if (rand <= sum)
                return enemyPrefabs[i];
        }

        return enemyPrefabs[enemyPrefabs.Length - 1]; // fallback
    }
}
