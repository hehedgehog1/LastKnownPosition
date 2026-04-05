using System;
using System.Linq;
using LastKnownPosition.Events;
using Models;
using UnityEngine;
using EventHandler = System.EventHandler;

public class TutorialManager : MonoBehaviour
{
    public event EventHandler TutorialCompleted;
    public event OnStepChangedEventHandler StepChanged;
    
    private Tutorial _tutorial;
    private Step _currentStep;
    private int _currentStepIndex;
    private int _totalSteps;

    private void Update()
    {
        if (_tutorial is null)
        {
            return;
        }
        
        if (_currentStep is null)
        {
            _currentStep = _tutorial.Steps.FirstOrDefault();
        }
        
        ProcessStep();
    }

    private void ProcessStep()
    {
        if (_currentStep.Completed)
        {
            if (IsLastStep())
            {
                TutorialCompleted?.Invoke(this, EventArgs.Empty);
                return;
            }
            
            ProgressStep();
        }

        if (Input.GetKeyDown((KeyCode)_currentStep.ContinueKey))
        {
            _currentStep.Completed = true;
        }
    }

    private void ProgressStep()
    {
        if (IsLastStep())
        {
            return;
        }

        _currentStepIndex++;
        _currentStep = _tutorial.Steps[_currentStepIndex];
        StepChanged?.Invoke(
            this, 
            new OnStepChangedEventArgs
            {
                Text = _currentStep.Text
            });
    }

    private bool IsLastStep() => _currentStepIndex >= _totalSteps - 1;

    public void LoadTutorial(Tutorial tutorial)
    {
        _tutorial = tutorial;
        _currentStep = _tutorial.Steps.FirstOrDefault();
        _totalSteps = _tutorial.Steps.Count;
        StepChanged?.Invoke(
            this, 
            new OnStepChangedEventArgs
            {
                Text = _currentStep.Text
            });
    }
}
