using Models;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject levelCompletePanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject hudPanel;
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private TextMeshProUGUI tutorialText;
    [SerializeField] private GameObject mapPanel;
    [SerializeField] private GameObject notesPanel;
    [SerializeField] private NotesPanelController notesController;
    [SerializeField] private FirstPersonPlayer firstPersonPlayer;

    public bool isNotesOpen;
    public bool isMapOpen;
    public bool isPaused;


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isNotesOpen == false)
        {
            ToggleMap();
        }

        if (Input.GetKeyDown(KeyCode.Tab) && isMapOpen == false)
        {
            ToggleNotes();
        }
    }

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

    public void ToggleMap()
    {
        SoundManager.Instance.PlayPageTurn();
        isMapOpen = !isMapOpen;
        bool isActive = mapPanel.activeSelf;
        mapPanel.SetActive(!isActive);
    }

    public void ToggleNotes()
    {
        SoundManager.Instance.PlayPageTurn();
        Cursor.visible = false;

        isNotesOpen = !isNotesOpen;
        notesPanel.SetActive(isNotesOpen);

        firstPersonPlayer.SetMovementEnabled(!isNotesOpen);
        notesController.SetActive(isNotesOpen);
    }
    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            pausePanel.SetActive(true);
            hudPanel.SetActive(false);
        }
        else
        {
            Time.timeScale = 1f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            pausePanel.SetActive(false);
            hudPanel.SetActive(true);
        }
    }

        public void ResumeGame()
    {
        TogglePause();
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
        );
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenuAine");
    }
}
