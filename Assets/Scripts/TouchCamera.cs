using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchCamera : MonoBehaviour
{
    [SerializeField]
    private float speedRotation;
    void Update()
    {
        if(Input.touchCount == 1) {
            Touch screenTouch = Input.GetTouch(0);
            if(screenTouch.phase == TouchPhase.Moved) {
               
                transform.Rotate(0f, -screenTouch.deltaPosition.x * speedRotation, 0f, Space.World);
			}

	    }

    }
}
