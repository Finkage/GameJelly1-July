using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    [Header("Mouse Settings")]
    public TMP_InputField inputSens;
    public Slider mouseSlider;
    public PlayerCamera playerCamera;

    [Header("Music Settings")]
    public Slider musicSlider;
    public Toggle musicToggle;

    private void Start()
    {
        if (SaveManager.Instance.mouseSensitivity > 0)
            mouseSlider.value = SaveManager.Instance.mouseSensitivity;

        musicSlider.value = AudioManager.Instance.soundtrack.volume;
        musicToggle.isOn = AudioManager.Instance.soundtrack.mute;
    }

    public void InitializePauseMenu()
    {
        // Only set to slider value if player has not altered the mouse sensitivity
        if (SaveManager.Instance.mouseSensitivity <= 0)
            SaveManager.Instance.mouseSensitivity = mouseSlider.value;

        inputSens.text = SaveManager.Instance.mouseSensitivity.ToString("F1");
    }

    public void OnConfirmInput()
    {
        float sens = float.Parse(inputSens.text);

        if (sens > mouseSlider.maxValue)
            playerCamera.SetMouseSensitivity(mouseSlider.maxValue);
        else if (sens < mouseSlider.minValue)
            playerCamera.SetMouseSensitivity(mouseSlider.minValue);
        else
            playerCamera.SetMouseSensitivity(sens);

        inputSens.text = playerCamera.GetMouseSensitivity().ToString("F1");
        mouseSlider.value = sens;
    }

    public void OnMouseSliderChange()
    {
        playerCamera.SetMouseSensitivity(mouseSlider.value);
        inputSens.text = mouseSlider.value.ToString("F1");
    }

    public void OnMusicSliderChange()
    {
        AudioManager.Instance.soundtrack.volume = musicSlider.value;
    }

    public void OnMusicToggleChange()
    {
        AudioManager.Instance.soundtrack.mute = musicToggle.isOn;
    }
}
