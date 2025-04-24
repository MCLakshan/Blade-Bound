using UnityEngine;
using UnityEngine.UI; // Import the UI namespace for Slider

public class SetDefaultSliderValue : MonoBehaviour
{
    // Reference to the Slider and AudioSource
    public Slider volumeSlider;
    public AudioSource audioSource;

    // Default value for the slider (range 0.0 to 1.0)
    private float defaultValue = 0.2f;

    void Start()
    {
        // Set the slider to the default value when the game starts
        if (volumeSlider != null)
        {
            volumeSlider.value = defaultValue;
        }

        // Set the audio source volume based on the default value
        if (audioSource != null)
        {
            audioSource.volume = defaultValue;
        }

        // Optionally subscribe to the slider's value change event
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.AddListener(OnSliderValueChanged);
        }
    }

    // Method to handle slider value changes
    public void OnSliderValueChanged(float value)
    {
        if (audioSource != null)
        {
            audioSource.volume = value;
        }
    }
}
