using Helpers;
using JetBrains.Annotations;
using LastKnownPosition;
using Models;
using UnityEngine;
using UnityEngine.AI;

public class MissingPersonSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public Terrain terrain;
    public GameObject missingPersonPrefab;
    public GameObject scentRingPrefab;

    [Header("Spawn Bounds")]
    public float minX = 153f;
    public float maxX = 754f;
    public float minZ = 156f;
    public float maxZ = 768f;

    public GameObject [] misPerLocations;
    private int missingPerSelector;

    [Header("Settings")]
    public int maxAttempts = 25;
    public bool showSpawnRectangle = true;
    
    
   public void SpawnMissingPerson()
    {
        missingPerSelector = Random.Range(0,(misPerLocations.Length - 1));
          GameObject selectedLocationGameObject = misPerLocations[missingPerSelector];
          Vector3 selectedLocation = selectedLocationGameObject.transform.position;
          
          var terrainY = terrain.GetTerrainHeightAtPosition(selectedLocation.x, selectedLocation.z);
          var locationVector = new Vector3(selectedLocation.x, terrainY, selectedLocation.z);
          Instantiate(missingPersonPrefab, locationVector, Quaternion.identity);
          Debug.Log("Spawned at" + selectedLocationGameObject);
          GenerateScentRing(1, selectedLocationGameObject.transform.position.x, selectedLocationGameObject.transform.position.z, 50, 1);
          GenerateScentRing(2, selectedLocationGameObject.transform.position.x, selectedLocationGameObject.transform.position.z, 80, 2);
          GenerateScentRing(3, selectedLocationGameObject.transform.position.x, selectedLocationGameObject.transform.position.z, 10, 3);
          GenerateScentRing(4, selectedLocationGameObject.transform.position.x, selectedLocationGameObject.transform.position.z, 200, 4);
          GenerateScentRing(5, selectedLocationGameObject.transform.position.x, selectedLocationGameObject.transform.position.z, 400, 5);
    }
 
    public void SpawnMissingPerson(MissingPerson missingPerson)
    {
        var location = missingPerson.Location;
        var terrainY = terrain.GetTerrainHeightAtPosition(location.X, location.Z);
        var locationVector = new Vector3(location.X, terrainY, location.Z);
        Instantiate(missingPersonPrefab, locationVector, Quaternion.identity);

        for (int i = 0; i < missingPerson.Rings.Count; i++)
        {
            if (missingPerson.Rings[i].Location.X is 0 && missingPerson.Rings[i].Location.Z is 0)
            {
                missingPerson.Rings[i].Location = missingPerson.Location;
            }
            
            GenerateScentRing(
                missingPerson.Rings[i].Id,
                missingPerson.Rings[i].Location.X, 
                missingPerson.Rings[i].Location.Z, 
                missingPerson.Rings[i].Radius, 
                missingPerson.Rings[i].Weight,
                missingPerson.Rings[i].ChildLocation);
        }
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

        // Lift slightly so it�s visible above terrain
        float yOffset = 200f;

        bottomLeft.y = bottomRight.y = topLeft.y = topRight.y = yOffset;

        Gizmos.DrawLine(bottomLeft, bottomRight);
        Gizmos.DrawLine(bottomRight, topRight);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, bottomLeft);
    }

    void GenerateScentRings(GameObject missingPerson)
    {
        GenerateScentRing(1, missingPerson.transform.position.x, missingPerson.transform.position.z, 50, 1);
        GenerateScentRing(2, missingPerson.transform.position.x, missingPerson.transform.position.z, 80, 2);
        GenerateScentRing(3, missingPerson.transform.position.x, missingPerson.transform.position.z, 10, 3);
        GenerateScentRing(4, missingPerson.transform.position.x, missingPerson.transform.position.z, 200, 4);
        GenerateScentRing(5, missingPerson.transform.position.x, missingPerson.transform.position.z, 400, 5);
    }

    void GenerateScentRing(int id, float x, float z, float radius, int weight, [CanBeNull] Location childLocation = null)
    {
        Vector3 position = new Vector3(x, Constants.RingOffset, z);
        
        var scentRing = Instantiate(scentRingPrefab, position, Quaternion.identity);

        scentRing.transform.localScale = new Vector3(radius*2, 0, radius*2);

        var scentRingData = scentRing.GetComponent<ScentRing>();
        scentRingData.Initialize(
            id,
            new Vector2(x, z),
            radius,
            weight,
            childLocation);
    }
}