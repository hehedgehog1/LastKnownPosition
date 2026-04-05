using System;
using Helpers;
using LastKnownPosition;
using Models;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private string levelName;
    [SerializeField] private GameObject missingPersonSpawnerGameObject;
    [SerializeField] private GameObject playerGameObject;
    
    private FirstPersonPlayer _player;
    private TimeManager _timeManager;
    private UIManager _uiManager;
    
    void Start()
    {
        SetupLevel();
        _timeManager = gameObject.GetComponent<TimeManager>();
        _timeManager.CountdownReached += OnCountdownReached;
        _uiManager = gameObject.GetComponent<UIManager>();
        _uiManager.ResetHud();
        _player = playerGameObject.GetComponent<FirstPersonPlayer>();
        _player.MissingPersonFound += OnMissingPersonFound;
    }

    void OnDestroy()
    {
        _player.MissingPersonFound -= OnMissingPersonFound;
    }

    private void SetupLevel()
    {
        var level = JsonHelper.FromJson<Level>(levelName);
        var missingPersonSpawner = missingPersonSpawnerGameObject.GetComponent<MissingPersonSpawner>();
        if (level is null)
        {
            missingPersonSpawner.SpawnMissingPerson();
            return;
        }
        
        missingPersonSpawner.SpawnMissingPerson(level.MissingPerson);
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        _uiManager.ResetHud();
        TimeManager.Resume();
    }
    
    private void OnMissingPersonFound(object sender, EventArgs e)
    {
        LevelComplete();
    }

    private void LevelComplete()
    {
        TimeManager.Pause();
        _uiManager.LevelComplete();
    }
    
    private void OnCountdownReached(object sender, EventArgs e)
    {
        GameOver();
    }

    private void GameOver()
    {
        TimeManager.Pause();
        _uiManager.GameOver();
    }
}
