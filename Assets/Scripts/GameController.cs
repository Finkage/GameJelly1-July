using UnityEngine;

public class GameController : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenu;

    private static bool gameIsWon = false;

    public void Update()
    {
        if (gameIsWon)
        {
            PauseGame();
            SetPauseState();
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
            SetPauseState();
            ToggleMenu(pauseMenu);

            if (pauseMenu.activeSelf)
                pauseMenu.GetComponent<PauseMenu>().InitializePauseMenu();
            else
                Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void ToggleMenu(GameObject menu)
    {
        menu.SetActive(!menu.activeSelf);
    }

    private void TogglePause()
    {
        gameIsPaused = !gameIsPaused;
        Time.timeScale = gameIsPaused ? 0f : 1f;
    }

    private void PauseGame()
    {
        gameIsPaused = true;
        Time.timeScale = 0f;
    }

    private void SetPauseState()
    {
        if (!gameIsPaused)
            return;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public bool GetWinCondition()
    {
        return gameIsWon;
    }

    public void SetWinCondition(bool gameWon)
    {
        gameIsWon = gameWon;
    }
}
