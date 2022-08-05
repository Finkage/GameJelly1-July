using UnityEngine;

public class WinGame : MonoBehaviour
{
    private GameController gameController;
    private GameObject winMenu;

    private void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        winMenu = GameObject.Find("UI Canvas").transform.Find("WinMenu").gameObject;
    }

    private void OnCollisionEnter(Collision collision)
    {
        winMenu.SetActive(true);
        gameController.SetWinCondition(true);
    }
}
