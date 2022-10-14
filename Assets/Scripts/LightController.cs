using UnityEngine;
using UnityEngine.UI;

public class LightController : MonoBehaviour
{

    public Slider slider;
    public Light sceneLight;
    public Text hiddenMessage;
    public GameObject sliderGameObject;

    private void Start()
    {
        hiddenMessage.enabled = false;
    }

    void Update() {
        sceneLight.intensity = slider.value;
        if(slider.value >= 0.5) {
            hiddenMessage.enabled = false;
        }
        else {
            hiddenMessage.enabled = true;
        }

        if(Input.touchCount > 0) {

            Touch touch = Input.GetTouch(0);
            Vector3 pos = touch.position;

            if(touch.phase == TouchPhase.Began) {
                Ray ray = Camera.main.ScreenPointToRay(pos);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit)) {
                    if(hit.collider.CompareTag("Lampada")) {
                        Debug.Log("Lampada");
                        sliderGameObject.SetActive(true);

                    }
                }
            }
        }
    }
}
