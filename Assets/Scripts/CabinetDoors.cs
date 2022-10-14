using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetDoors : MonoBehaviour
{
    [SerializeField]private Animator direitaAnim, esquerdaAnim;
    private bool rightOpen, leftOpen;
    

    void Update()
    {
        if(Input.touchCount > 0) {

            Touch touch = Input.GetTouch(0);
            Vector3 pos = touch.position;

            if(touch.phase == TouchPhase.Began) {
                Ray ray = Camera.main.ScreenPointToRay(pos);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit)) {
                    if(hit.collider.CompareTag("RightDoor") && rightOpen == false) {

                        direitaAnim.SetBool("isOpen", true);
                        rightOpen = true;
                    }
                    else if(hit.collider.CompareTag("RightDoor") && rightOpen == true) {
                        direitaAnim.SetBool("isOpen", false);
                        rightOpen = false;
                    }
                    else if(hit.collider.CompareTag("LeftDoor") && leftOpen == false) {
                        Debug.Log("Open");
                        esquerdaAnim.SetBool("isOpen", true);
                        leftOpen = true;
                    }
                    else if(hit.collider.CompareTag("LeftDoor") && leftOpen == true) {
                        Debug.Log("Close");
                        esquerdaAnim.SetBool("isOpen", false);
                        leftOpen = false;
                    }
                }
            }
        }
    }
}
