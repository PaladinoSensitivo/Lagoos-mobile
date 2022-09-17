using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]private Transform[] targetPosition;
    [SerializeField]private Transform target, start;
    [SerializeField]private float speed;
    [SerializeField]private TouchCamera touchCamera;
    private Transform currentPos;
    private Vector3 velocity = Vector3.zero;
    private bool isMoving, gyroEnabled;
    public bool isReturning;
    private Gyroscope gyro;
    private GameObject cameraContainer;

    RaycastHit hit;

    private void Start() {
        cameraContainer = new GameObject("Camera Container");
        cameraContainer.transform.position = transform.position;
        transform.SetParent(cameraContainer.transform);

        gyro = Input.gyro;
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
					switch(hit.collider.tag) {
                        case "Cabinet":
                            currentPos = targetPosition[0];
                            cameraContainer.transform.rotation = targetPosition[0].rotation;
                            SwitchPosition();
                            break;
                        case "Desk":
                            currentPos = targetPosition[1];
                            cameraContainer.transform.rotation = targetPosition[1].rotation;
                            SwitchPosition();
                            break;
                        case "Bed":
                            currentPos = targetPosition[2];
                            cameraContainer.transform.rotation = targetPosition[2].rotation;
                            SwitchPosition();
                            break;
                        case "Stand":
                            currentPos = targetPosition[3];
                            cameraContainer.transform.rotation = targetPosition[3].rotation;
                            SwitchPosition();
                            break;
                        case "Shelf":
                            currentPos = targetPosition[4];
                            cameraContainer.transform.rotation = targetPosition[4].rotation;
                            SwitchPosition();
                            break;
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
        transform.rotation = Quaternion.Euler(30f, 45f, 0f);
        gyroEnabled = EnableGyro();
    }

    void SwitchPosition() {
        isMoving = true;
        isReturning = false;
        touchCamera.enabled = false;
        gyroEnabled = EnableGyro();
    }

    private bool EnableGyro() {
        if(hit.collider != null) {

            if(isReturning == false) {      
                
                gyro.enabled = true;
                return true;
            }
		}
        else if(isReturning == true) {      
            gyro.enabled = false;           
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
