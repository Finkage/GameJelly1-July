// https://www.youtube.com/watch?v=f473C43s8nE

using UnityEngine;
using UnityEngine.UI;

public class PlayerCamera : MonoBehaviour
{
    public Transform playerTransform;
    public Transform orientation;
    public float xRotation;
    public float yRotation;
    public Slider mouseSensitivitySlider;

    private void Start()
    {
        // Lock cursor to middle of screen and hide it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Only set to slider value if player has not altered the mouse sensitivity
        if (SaveManager.Instance.mouseSensitivity <= 0)
            SetMouseSensitivity(mouseSensitivitySlider.value);
        else
            SetMouseSensitivity(SaveManager.Instance.mouseSensitivity);
    }

    private void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * SaveManager.Instance.mouseSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * SaveManager.Instance.mouseSensitivity;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Rotate camera and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

        // Rotate player game object
        playerTransform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    public float GetMouseSensitivity()
    {
        return SaveManager.Instance.mouseSensitivity;
    }

    public void SetMouseSensitivity(float newSens)
    {
        SaveManager.Instance.mouseSensitivity = newSens;
    }
}
