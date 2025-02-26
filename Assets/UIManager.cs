using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject settingsMenu;
    public GameObject pauseMenu;
    private bool isPaused = false;

    void Start()
    {
        startMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {

            if (pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1; // Resume game
            }
            else
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0; // Pause game
            }
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenuScene");
    }
    public void StartGame()
    {
        startMenu.SetActive(false); // Hide start menu
        Time.timeScale = 1; // Ensure time resumes in case it was paused
    }

    public void OpenSettings()
    {
        startMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsMenu.SetActive(false); // Just close settings

        // Only unpause if the pause menu was open when settings was opened
        if (pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(true);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}

