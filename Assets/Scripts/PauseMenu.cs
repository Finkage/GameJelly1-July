using UnityEngine;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public TMP_InputField inputSens;
    public PlayerCamera playerCamera;

    public void InitializePauseMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        inputSens.text = playerCamera.lookSensitivity.ToString("F1");
    }
}
