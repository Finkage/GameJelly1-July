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

    private void Start()
    {
        if (SaveManager.Instance.mouseSensitivity > 0)
            mouseSlider.value = SaveManager.Instance.mouseSensitivity;

        musicSlider.value = AudioManager.Instance.soundtrack.volume;
    }

    public void InitializePauseMenu()
    {
        inputSens.text = playerCamera.GetMouseSensitivity().ToString("F1");
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
}
