using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject levelCompletePanel;
    [SerializeField] private GameObject hudPanel;
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private TextMeshProUGUI tutorialText;

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

    public void SetTutorialStep(string text)
    {
        tutorialPanel.SetActive(true);
        tutorialText.text = text;
    }

    public void DisableDialog()
    {
        tutorialText.text = string.Empty;
        tutorialPanel.SetActive(false);
    }
}
