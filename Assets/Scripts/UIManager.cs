using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject levelCompletePanel;
    [SerializeField] private GameObject hudPanel;

    public void ResetHud()
    {
        Cursor.visible = false;
        hudPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        levelCompletePanel.SetActive(false);
    }

    public void GameOver()
    {
        Cursor.visible = true;
        
        Cursor.lockState = CursorLockMode.None;
        gameOverPanel.SetActive(true);
        levelCompletePanel.SetActive(false);
        hudPanel.SetActive(false);
    }

    public void LevelComplete()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        levelCompletePanel.SetActive(true);
        gameOverPanel.SetActive(false);
        hudPanel.SetActive(false);
    }
}
