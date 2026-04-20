using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused;

    void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Debug.Log("RESUME CLICKED");
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
    }
}
