using System;
using LastKnownPosition;
using Models;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private string levelName;
    [SerializeField] private GameObject missingPersonSpawnerGameObject;
    [SerializeField] private GameObject playerGameObject;
    
    private FirstPersonPlayer _player;
    
    void Start()
    {
        SetupLevel();
        
        _player = playerGameObject.GetComponent<FirstPersonPlayer>();
        _player.MissingPersonFound += OnMissingPersonFound;
    }

    void OnDestroy()
    {
        _player.MissingPersonFound -= OnMissingPersonFound;
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
    
    private void OnMissingPersonFound(object sender, EventArgs e)
    {
        TimeManager.Pause();
    }
}
