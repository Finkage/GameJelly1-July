using UnityEngine;

public class ViewMap : MonoBehaviour
{
    public static ViewMap Instance;

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
