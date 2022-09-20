using UnityEngine;
using UnityEngine.UI;

public class LightController : MonoBehaviour
{

    public Slider slider;
    public Light sceneLight;
    public Text hiddenMessage;

    private void Start()
    {
        hiddenMessage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        sceneLight.intensity = slider.value;
        if (slider.value >= 0.5)
        {
            hiddenMessage.enabled = false;
        }
        else
        {
            hiddenMessage.enabled = true;
        }
    }
}
