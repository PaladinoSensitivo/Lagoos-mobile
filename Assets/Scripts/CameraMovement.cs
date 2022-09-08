using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]private Transform target, start;
    [SerializeField]private float speed;
    [SerializeField]private TouchCamera touchCamera;
    private Transform currentPos;
    private Vector3 velocity = new Vector3 (1, 0, 0);
    private bool isMoving, isRotating;

    private bool gyroEnabled;
    private Gyroscope gyro;

    private GameObject cameraContainer;

    private void Start() {
        cameraContainer = new GameObject("Camera Container");
        cameraContainer.transform.position = transform.position;
        transform.SetParent(cameraContainer.transform);
	}

	void Update()
    {
        MoveCamera();
        if(gyroEnabled) {
            transform.localRotation = GyroToUnity(gyro.attitude);
        }
    }

    void MoveCamera() {
		#region Inputs
		if(Input.GetKeyDown(KeyCode.Space)) {
            isMoving = true;
            currentPos = target;
            touchCamera.enabled = false;
            gyroEnabled = EnableGyro();
        }


        if(Input.GetKeyDown(KeyCode.Backspace)) {
            isMoving = true;
            isRotating = true;
            currentPos = start;
            touchCamera.enabled = true;
            gyro.enabled = false;
            cameraContainer.transform.rotation = currentPos.rotation;
            
        }
		#endregion

		#region Position
		if(isMoving && transform.position != currentPos.position ) {
            transform.position = Vector3.SmoothDamp(transform.position, currentPos.position, ref velocity, speed * Time.deltaTime);
        }
		else {
            isMoving = false;
        }
        #endregion

        /*#region Rotation
        if(isRotating && cameraContainer.transform.rotation != currentPos.rotation) {
            cameraContainer.transform.rotation = Quaternion.Slerp(cameraContainer.transform.rotation, currentPos.rotation, Time.deltaTime);
        }
        else {
            isRotating = false;
        }
        #endregion*/

    }

    private bool EnableGyro() {
        if(SystemInfo.supportsGyroscope) {
            gyro = Input.gyro;
            gyro.enabled = true;

            cameraContainer.transform.rotation = target.rotation;


            return true;
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
