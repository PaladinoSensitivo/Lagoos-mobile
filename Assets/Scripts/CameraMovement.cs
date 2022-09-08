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
    private bool isMoving;

    public bool gyroEnabled;
    private Gyroscope gyro;

    private GameObject cameraContainer;

    private void Start() {
        //Criando uma parent para a camera, para poder mover a camera e definir a rotacao.
        cameraContainer = new GameObject("Camera Container");
        cameraContainer.transform.position = transform.position;
        transform.SetParent(cameraContainer.transform);
	}

	void Update()
    {
        //chamando o metodo
        MoveCamera();

        //chamando o metodo que retorna uma bool e atualizar minha rotacao da camera conforme a valor que retorna.
        //verdadeiro retorna a toracao do giroscopio, falso retorna uma rotacao ja pre definida
        if(gyroEnabled) {
            transform.localRotation = GyroToUnity(gyro.attitude);
            Debug.Log("Gyro on");
        }
        else{
            transform.rotation = Quaternion.Euler(30f, 45f, 0f);
            Debug.Log("Gyro off");
		}
    }

    void MoveCamera() {
		#region Inputs
		if(Input.GetKeyDown(KeyCode.Space)) {
            isMoving = true;                    //tornando a variavel verdadeira para dar o trigger na movimentacao
            currentPos = target;                //definindo o destino da minha camera
            touchCamera.enabled = false;        //desligando o script responsavel pela movimentacao do cenario
            gyroEnabled = EnableGyro();         //chamando o metodo para atualizar o valor que ele deve retornar
        }


        if(Input.GetKeyDown(KeyCode.Backspace)) {
            isMoving = true;
            currentPos = start;
            touchCamera.enabled = true;
            cameraContainer.transform.rotation = currentPos.rotation;       //retornando o parent para a rotacao inicial
            gyroEnabled = EnableGyro();
        }
		#endregion

        //animacao da movimentacao da camera caso a viariavel seja verdadeira e sua posicao atual seja diferente da posicao desejada
		#region Position
		if(isMoving && cameraContainer.transform.position != currentPos.position ) {
            cameraContainer.transform.position = Vector3.SmoothDamp(cameraContainer.transform.position, currentPos.position, ref velocity, speed * Time.deltaTime);     
        }
		else {
            isMoving = false;
        }
        #endregion
    }

    private bool EnableGyro() {
        if(Input.GetKeyDown(KeyCode.Space)) {       //se o touch detectar a tag liga o giroscopio e faz com que o parent tenha a mesma rotacao do destino
            gyro = Input.gyro;
            gyro.enabled = true;

            cameraContainer.transform.rotation = target.rotation;


            return true;
        }
        else if(Input.GetKeyDown(KeyCode.Backspace)) {      //ao clicar botao de retornar desliga o giroscopio e retorna falso para manter a camera na sua posicao e rotacao original
            gyro.enabled = false;
            return false;
		}
        return false;
    }

    //metodo responsavel pela calibracao do giroscopio
    private Quaternion GyroToUnity(Quaternion q) {
        return new Quaternion(q.x,
            q.y,
            -q.z,
            -q.w);
    }
}
