using UnityEngine;
using UnityEngine.AI;

public class MissingPersonSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public Terrain terrain;
    public GameObject missingPersonPrefab;

    [Header("Spawn Bounds")]
    public float minX = 153f;
    public float maxX = 754f;
    public float minZ = 156f;
    public float maxZ = 768f;

    [Header("Settings")]
    public int maxAttempts = 25;
    public bool showSpawnRectangle = true;


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

    void OnDrawGizmos()
    {
        if (!showSpawnRectangle)
            return;

        Gizmos.color = Color.red;

        Vector3 bottomLeft = new Vector3(minX, 0f, minZ);
        Vector3 bottomRight = new Vector3(maxX, 0f, minZ);
        Vector3 topLeft = new Vector3(minX, 0f, maxZ);
        Vector3 topRight = new Vector3(maxX, 0f, maxZ);

        // Lift slightly so it’s visible above terrain
        float yOffset = 200f;

        bottomLeft.y = bottomRight.y = topLeft.y = topRight.y = yOffset;

        Gizmos.DrawLine(bottomLeft, bottomRight);
        Gizmos.DrawLine(bottomRight, topRight);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, bottomLeft);
    }
}