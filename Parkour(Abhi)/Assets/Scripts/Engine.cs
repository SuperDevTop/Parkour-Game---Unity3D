using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Engine : MonoBehaviour
{
    public GameObject exitMenu;
    public Text displayText;
    public AudioSource audio1;
    public AudioSource audio2;
    
    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "exit")
        {
            exitMenu.SetActive(true);
            displayText.text = "Congratulations!";
        }

        if(col.tag == "enemy")
        {
            exitMenu.SetActive(true);
            displayText.text = "Game over!";
        }

        if(col.tag == "stop" && !ControllerMethod.keyboardControll)  
        {
            this.GetComponent<NavMeshAgent>().isStopped = true;
            this.GetComponent<NavMeshAgent>().enabled = false;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.tag == "stop" && !ControllerMethod.keyboardControll)
        {
            this.GetComponent<NavMeshAgent>().isStopped = false;
            this.GetComponent<NavMeshAgent>().enabled = true;
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            audio1.Play();
        }

        if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            audio1.Stop();
        }

        if(Input.GetKey(KeyCode.Space))
        {
            audio2.Play();
        }
    }
}