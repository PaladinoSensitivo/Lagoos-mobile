using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroCamera : MonoBehaviour
{
    private bool gyroEnabled;
    private Gyroscope gyro;

	private GameObject cameraContainer;
	private Quaternion rot;

	private void Start() {
		gyroEnabled = EnableGyro();
	}

	private bool EnableGyro() {
		if(SystemInfo.supportsGyroscope) {
			gyro = Input.gyro;
			gyro.enabled = true;

			//transform.rotation = Quaternion.Euler(30f, 45f, 0f);

			return true;
		}
		return false;
	}

	private void Update() {
		if(gyroEnabled) {
			transform.localRotation = GyroToUnity(gyro.attitude);
		}

	}
	private Quaternion GyroToUnity(Quaternion q) {
		return new Quaternion(q.x,
			q.y,
			-q.z,
			-q.w);
	}
}
