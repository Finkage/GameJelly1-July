using UnityEngine;

public class GameController : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenu;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
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
}
