using UnityEngine;

public class GameController : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenu;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
            ToggleMenu(pauseMenu);
        }
    }

    public void ToggleMenu(GameObject menu)
    {
        menu.SetActive(!menu.activeSelf);
        Cursor.visible = gameIsPaused;
    }

    public void PauseGame()
    {
        gameIsPaused = !gameIsPaused;
        Time.timeScale = gameIsPaused ? 0f : 1f;
    }
}
