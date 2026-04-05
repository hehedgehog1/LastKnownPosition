using Models;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    private Tutorial _tutorial;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadTutorial(Tutorial tutorial)
    {
        _tutorial = tutorial;
    }
}
