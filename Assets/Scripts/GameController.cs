using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]private GameObject _tutorial;
    void Start()
    {
        Time.timeScale = 0;
    }


    public void StartGame()
    {
        Time.timeScale = 1;
        _tutorial.SetActive(false);
    }
}
