using System;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float remainingTime;

    private bool _countdownHit;

    public event EventHandler CountdownReached;
    
    public static void Pause() 
    {
        Time.timeScale = 0;
    }
       
    public static void Resume()
    {
        Time.timeScale = 1; 
    }
    
    private void Update()
    {
        HandleCountdown();
    }

    private void HandleCountdown()
    {
        if (_countdownHit)
        {
            return;
        }

        if (remainingTime <= 0)
        {
            timerText.text = "0:00";
            _countdownHit = true;
            return;
        }
        
        remainingTime -= Time.deltaTime;
        var minutes = Mathf.FloorToInt(remainingTime / 60);
        var seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}