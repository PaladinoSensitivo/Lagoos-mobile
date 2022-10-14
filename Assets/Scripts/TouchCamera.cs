using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchCamera : MonoBehaviour
{
    [SerializeField]
    private float speedRotation;
    private bool onInterface;
    void Update()
    {
        if(onInterface) return;
        if(Input.touchCount == 1) {
            Touch screenTouch = Input.GetTouch(0);
            if(screenTouch.phase == TouchPhase.Moved) {
               
                transform.Rotate(0f, -screenTouch.deltaPosition.x * speedRotation, 0f, Space.World);
			}

	    }

    }
    public void OnInterface() {
        onInterface = true;
	}
    public void OutInterface() {
        onInterface = false;
    }
}
