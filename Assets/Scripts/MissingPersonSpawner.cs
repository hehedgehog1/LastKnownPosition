using UnityEngine;

public class MissingPersonSpawner : MonoBehaviour
{
    public Terrain terrain;       
    public GameObject missingPersonPrefab;
    public float spawnHeight = 50f;

    void Start()
    {
        SpawnMissingPerson();
    }

    void SpawnMissingPerson()
    {
        float terrainWidth = terrain.terrainData.size.x;
        float terrainLength = terrain.terrainData.size.z;

        float randomX = Random.Range(0, terrainWidth);
        float randomZ = Random.Range(0, terrainLength);

        Vector3 rayStart = new Vector3(randomX, spawnHeight, randomZ);
        if (Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, spawnHeight * 2))
        {
            Instantiate(missingPersonPrefab, hit.point, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Failed to find terrain, retrying...");
            SpawnMissingPerson();
        }
    }
}