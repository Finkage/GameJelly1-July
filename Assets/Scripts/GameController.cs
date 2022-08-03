using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public static bool gameIsPaused = false;

    [Header("Pause Menu")]
    public GameObject pauseMenu;
    public TMP_InputField inputSens;
    public PlayerCamera playerCamera;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
            ToggleMenu(pauseMenu);

            if (pauseMenu.activeSelf)
                InitializePauseMenu();
            else
                Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void ToggleMenu(GameObject menu)
    {
        menu.SetActive(!menu.activeSelf);
    }

    private void InitializePauseMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        inputSens.text = playerCamera.lookSensitivity.ToString("F1");
    }

    private void TogglePause()
    {
        gameIsPaused = !gameIsPaused;
        Time.timeScale = gameIsPaused ? 0f : 1f;
    }
}
