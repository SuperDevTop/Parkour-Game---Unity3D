using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControllerMethod : MonoBehaviour
{
    public GameObject player;
    public GameObject gameUI;
    public Camera mouseCamera;
    public static bool keyboardControll;     

    void Start()
    {
        gameUI.transform.localScale = new Vector3(Screen.width / 1366f, Screen.height / 768f, 1f);
        keyboardControll = true;
    }

    void Update()
    {
        gameUI.transform.localScale = new Vector3(Screen.width / 1366f, Screen.height / 768f, 1f);
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////         UI Buttons       ///////////////////////////////////////////
    public void KeyboardBtnClick()
    {
        keyboardControll = true;
        mouseCamera.gameObject.SetActive(false);
    }

    public void MouseBtnClick()
    {
        keyboardControll = false;
        mouseCamera.gameObject.SetActive(true);
    }

    public void RestartBtnClick()
    {
        player.transform.position = new Vector3(0, -0.5f, -12f);
        player.GetComponent<NavMeshAgent>().isStopped = true;
        player.GetComponent<NavMeshAgent>().enabled = false;
    }

    public void ExitBtnClick()
    {
        Application.Quit(0);
    }
}
