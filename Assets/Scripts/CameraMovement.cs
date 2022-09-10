using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]private Transform target, start;
    [SerializeField]private float speed;
    [SerializeField]private TouchCamera touchCamera;
    private Transform currentPos;
    private Vector3 velocity = Vector3.zero;
    private bool isMoving, isReturning, gyroEnabled;

    private Gyroscope gyro;

    private GameObject cameraContainer;

    RaycastHit hit;

    private void Start() {
        cameraContainer = new GameObject("Camera Container");
        cameraContainer.transform.position = transform.position;
        transform.SetParent(cameraContainer.transform);
	}

	void Update()
    {

        if(gyroEnabled) {
            transform.localRotation = GyroToUnity(gyro.attitude);
            Debug.Log("Gyro on");
        }
        MoveCamera();

        if(Input.touchCount > 0) {

		    Touch touch = Input.GetTouch(0);
		    Vector3 pos = touch.position;

            if(touch.phase == TouchPhase.Began) {
                Ray ray = Camera.main.ScreenPointToRay(pos);

                if(Physics.Raycast(ray, out hit)) {
                    if(hit.collider.tag == "Cabinet") {
                        isMoving = true;
                        currentPos = target;
                        touchCamera.enabled = false;
                        gyroEnabled = EnableGyro();

                        cameraContainer.transform.rotation = target.rotation;

                    }
			    }
		    }
		}
    }

    void MoveCamera() {

		if(isMoving && cameraContainer.transform.position != currentPos.position ) {
            cameraContainer.transform.position = Vector3.SmoothDamp(cameraContainer.transform.position, currentPos.position, ref velocity, speed * Time.deltaTime);     
        }
		else {
            isMoving = false;
        }

    }

    public void ReturnCamera() {
        isMoving = true;
        isReturning = true;
        currentPos = start;
        touchCamera.enabled = true;
        cameraContainer.transform.rotation = currentPos.rotation;
        gyroEnabled = EnableGyro();
    }

    private bool EnableGyro() {
        if(hit.collider != null) {

            if(hit.collider.tag == "Cabinet") {      
                gyro = Input.gyro;
                gyro.enabled = true;

                

                return true;
            }
		}
        else if(isReturning == true) {      
            gyro.enabled = false;
            isReturning = false;
            transform.rotation = Quaternion.Euler(30f, 45f, 0f);
            return false;
		}
        return false;
    }

    private Quaternion GyroToUnity(Quaternion q) {
        return new Quaternion(q.x,
            q.y,
            -q.z,
            -q.w);
    }
}
