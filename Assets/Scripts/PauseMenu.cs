using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public TMP_InputField inputSens;
    public Slider slider;
    public PlayerCamera playerCamera;

    public void InitializePauseMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        inputSens.text = playerCamera.GetMouseSensitivity().ToString("F1");
    }

    public void OnConfirmInput()
    {
        float sens = float.Parse(inputSens.text);

        if (sens > slider.maxValue)
            playerCamera.SetMouseSensitivity(slider.maxValue);
        else if (sens < slider.minValue)
            playerCamera.SetMouseSensitivity(slider.minValue);
        else
            playerCamera.SetMouseSensitivity(sens);

        inputSens.text = playerCamera.GetMouseSensitivity().ToString("F1");
        slider.value = sens;
    }
}
