using UnityEngine;
using UnityEngine.AI;

public class MissingPersonSpawner : MonoBehaviour
{
    public Terrain terrain;
    public GameObject missingPersonPrefab;

    // Bounds for spawning
    public float minX = -13f;
    public float maxX = 562f;
    public float minZ = 0f;
    public float maxZ = 700f;

    public int maxAttempts = 25;

    void Start()
    {
        SpawnMissingPerson();
    }

    void SpawnMissingPerson()
    {
        for (int i = 0; i < maxAttempts; i++)
        {
            float randomX = Random.Range(minX, maxX);
            float randomZ = Random.Range(minZ, maxZ);

            // Get terrain height at the point generated
            float terrainY = terrain.SampleHeight(new Vector3(randomX, 0f, randomZ));

            Vector3 candidate = new Vector3(randomX, terrainY, randomZ);

            NavMeshHit hit;

            if (NavMesh.SamplePosition(candidate, out hit, 2f, NavMesh.AllAreas))
            {
                Instantiate(missingPersonPrefab, hit.position, Quaternion.identity);
                return;
            }
        }

        Debug.LogWarning("Failed to find valid NavMesh position.");
    }
}