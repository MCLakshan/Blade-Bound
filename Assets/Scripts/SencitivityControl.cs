using UnityEngine;
using UnityEngine.UI;

public class SencitivityControl : MonoBehaviour
{
    [SerializeField] GameObject camera_obj = null;
    public Slider slider = null;

    private float defaultValue = 2f;

    void Start()
    {
        if (camera_obj != null && slider != null)
        {
            CameraController controller = camera_obj.GetComponent<CameraController>();
            if (controller != null)
            {
                // Set the slider to the mapped default value
                slider.value = defaultValue / 10;
                controller.set_rotationSpeed(defaultValue); // Set the initial sensitivity
            }
            else
            {
                Debug.LogWarning("CameraController component not found on camera_obj");
            }
        }
        else
        {
            Debug.LogError("camera_obj or slider is not assigned.");
        }
    }

    public void OnSliderValueChanged(float value)
    {
        if (camera_obj != null && slider != null)
        {
            CameraController controller = camera_obj.GetComponent<CameraController>();
            if (controller != null)
            {
                // Map slider value to the desired range and set the sensitivity
                controller.set_rotationSpeed(Map(value));
            }
            else
            {
                Debug.LogWarning("CameraController component not found on camera_obj during slider change.");
            }
        }
    }

    public float Map(float value)
    {
        // Multiply by 10 to adjust the value from 0-1 range (slider) to 0-10
        return value * 10;
    }
}
