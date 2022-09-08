using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accel : MonoBehaviour
{
    private Vector3 tilt;
    public float velocity;
    private Rigidbody rigid;
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

   
    void Update()
    {
        tilt = Quaternion.Euler(0, -45, 0) * Input.acceleration;
    }

	private void FixedUpdate() {
        rigid.AddForce(tilt.x * velocity, 0, -tilt.z * velocity, ForceMode.Impulse);

    }
}
