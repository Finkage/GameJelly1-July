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
            PauseGame();
            ToggleMenu(pauseMenu);
        }
    }

    public void ToggleMenu(GameObject menu)
    {
        menu.SetActive(!menu.activeSelf);
        Cursor.visible = gameIsPaused;
        inputSens.text = playerCamera.lookSensitivity.ToString("F1");
        
    }

    public void PauseGame()
    {
        gameIsPaused = !gameIsPaused;
        Time.timeScale = gameIsPaused ? 0f : 1f;
    }
}
