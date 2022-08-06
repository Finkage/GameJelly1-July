using UnityEngine;

public class ViewMap : MonoBehaviour
{
    public static bool mapIsUp = false;

    public GameObject mainCamera;
    public GameObject mapCamera;

    private void Update()
    {
        if (GameController.gameIsPaused)
            return;

        if (Input.GetKeyDown(KeyCode.Tab))
            ToggleMap();
    }

    private void ToggleMap()
    {
        mainCamera.SetActive(!mainCamera.activeSelf);
        mapCamera.SetActive(!mapCamera.activeSelf);

        mapIsUp = mapCamera.activeSelf;
    }
}
