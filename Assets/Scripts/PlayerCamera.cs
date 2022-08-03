// https://www.youtube.com/watch?v=f473C43s8nE

using UnityEngine;
using TMPro;

public class PlayerCamera : MonoBehaviour
{
    public float lookSensitivity;
    public Transform orientation;
    public float xRotation;
    public float yRotation;
    public TMP_InputField inputSens;

    private void Start()
    {
        // Lock cursor to middle of screen and hide it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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

    public void SetMouseSensitivity()
    {
        lookSensitivity = float.Parse(inputSens.text);
    }
}
