using Models;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private string levelName;
    [SerializeField] private GameObject missingPersonSpawnerGameObject;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetupLevel();
    }

    private void SetupLevel()
    {
        var levelJson = Resources.Load<TextAsset>(levelName);
        var missingPersonSpawner = missingPersonSpawnerGameObject.GetComponent<MissingPersonSpawner>();

        if (string.IsNullOrEmpty(levelJson.text))
        {
            missingPersonSpawner.SpawnMissingPerson();
            return;
        }
        
        var level = JsonUtility.FromJson<Level>(levelJson.text);
        missingPersonSpawner.SpawnMissingPerson(level.MissingPerson);
    }
}
