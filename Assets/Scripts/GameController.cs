using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public bool devMode = false;
    public static bool gameIsPaused = false;
    public GameObject pauseMenu;
    public GameObject restartButton;
    public GameObject mainMenu;
    public GameObject posedMainMenu;
    public GameObject flagIndicator;
    public MazeGeneration mazeGeneration;
    public GameObject mainMenuCamera;
    public GameObject playerCamera;

    private static bool gameIsWon = false;

    public void Start()
    {
        PauseGame();
        SetWinCondition(true);

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
            if (pauseMenu.activeSelf)
                HidePauseMenu();
            else
                DisplayPauseMenu();
        }
    }

    public void StartGame()
    {
        ResumeGame();
        SetWinCondition(false);
        mazeGeneration.CreateNewMaze();
        mainMenuCamera.SetActive(false);
        playerCamera.SetActive(true);
        mainMenu.SetActive(false);
        posedMainMenu.SetActive(false);
        flagIndicator.SetActive(true);
    }

    public void DisplayPauseMenu()
    {
        PauseGame();
        pauseMenu.SetActive(true);
        pauseMenu.GetComponent<PauseMenu>().InitializePauseMenu();

        restartButton.SetActive(!gameIsWon);
    }

    public void HidePauseMenu()
    {
        pauseMenu.SetActive(false);

        if (!gameIsWon)
            ResumeGame();
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
