using System;
using Helpers;
using LastKnownPosition;
using LastKnownPosition.Events;
using Models;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private string levelName;
    [SerializeField] private GameObject missingPersonSpawnerGameObject;
    [SerializeField] private GameObject playerGameObject;
    [SerializeField] private bool isTutorial;
    
    private FirstPersonPlayer _player;
    private TimeManager _timeManager;
    private UIManager _uiManager;
    private TutorialManager _tutorialManager;
    
    void Start()
    {
        _timeManager = gameObject.GetComponent<TimeManager>();
        _timeManager.CountdownReached += OnCountdownReached;
        
        _uiManager = gameObject.GetComponent<UIManager>();
        _uiManager.ResetHud();
        
        _tutorialManager = gameObject.GetComponent<TutorialManager>();
        _tutorialManager.StepChanged += OnStepChanged;
        
        _player = playerGameObject.GetComponent<FirstPersonPlayer>();
        _player.MissingPersonFound += OnMissingPersonFound;
        
        SetupLevel();
    }

    private void OnStepChanged(object sender, OnStepChangedEventArgs e)
    {
        _uiManager.SetTutorialStep(e.Text);
    }

    void OnDestroy()
    {
        _player.MissingPersonFound -= OnMissingPersonFound;
        _timeManager.CountdownReached -= OnCountdownReached;
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

        if (level.IsTutorial)
        {
            SetupTutorial();
        }
    }

    private void SetupTutorial()
    {
        var tutorial = JsonHelper.FromJson<Tutorial>("Tutorial");

        if (tutorial is null)
        {
            isTutorial = false;
            return;
        }

        _tutorialManager.enabled = true;
        _tutorialManager.LoadTutorial(tutorial);
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
