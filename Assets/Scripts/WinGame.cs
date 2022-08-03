using UnityEngine;

public class WinGame : MonoBehaviour
{
    public GameController gameController;
    public GameObject winMenu;

    private void OnCollisionEnter(Collision collision)
    {
        winMenu.SetActive(true);
        gameController.SetWinCondition(true);
    }
}
