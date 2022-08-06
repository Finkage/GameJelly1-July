using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public bool devMode = false;
    public static bool gameIsPaused = false;
    public GameObject pauseMenu;

    private static bool gameIsWon = false;

    public void Start()
    {
        ResumeGame();
        SetWinCondition(false);

        if (!AudioManager.Instance.muteMusic)
            AudioManager.Instance.PlayAudio(AudioManager.Instance.soundtrack);
        else
            AudioManager.Instance.PauseAudio(AudioManager.Instance.soundtrack);
    }

    public void Update()
    {
        if (gameIsWon)
        {
            PauseGame();
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu(pauseMenu);

            if (pauseMenu.activeSelf)
            {
                PauseGame();
                pauseMenu.GetComponent<PauseMenu>().InitializePauseMenu();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    public void ToggleMenu(GameObject menu)
    {
        menu.SetActive(!menu.activeSelf);
    }

    private void PauseGame()
    {
        gameIsPaused = true;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void ResumeGame()
    {
        gameIsPaused = false;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public bool GetWinCondition()
    {
        return gameIsWon;
    }

    public void SetWinCondition(bool gameWon)
    {
        gameIsWon = gameWon;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
