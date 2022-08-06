using UnityEngine;

public class ViewMap : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject mapCamera;

    public bool mapIsUp = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            ToggleMap();
    }

    private void ToggleMap()
    {
        mainCamera.SetActive(!mainCamera.activeSelf);
        mapCamera.SetActive(!mapCamera.activeSelf);
    }
}
