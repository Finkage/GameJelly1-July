// https://www.youtube.com/watch?v=f473C43s8nE

using UnityEngine;
using UnityEngine.UI;

public class PlayerCamera : MonoBehaviour
{
    public Transform orientation;
    public float xRotation;
    public float yRotation;
    public Slider mouseSensitivitySlider;

    private float lookSensitivity;

    private void Start()
    {
        // Lock cursor to middle of screen and hide it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SetMouseSensitivity(mouseSensitivitySlider.value);
    }

    private void Update()
    {
        // Get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * lookSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * lookSensitivity;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Rotate camera and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    public float GetMouseSensitivity()
    {
        return lookSensitivity;
    }

    public void SetMouseSensitivity(float newSens)
    {
        lookSensitivity = newSens;
    }
}
