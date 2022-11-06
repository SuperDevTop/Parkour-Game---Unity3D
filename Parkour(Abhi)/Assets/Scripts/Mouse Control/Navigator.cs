using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigator : MonoBehaviour
{
    NavMeshAgent agent;
    Targeter targeter;
    Animator animator;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        targeter = GetComponent<Targeter>();
        animator = GetComponent<Animator>();
    }

    public void navigateTo(Vector3 position)
    {
        agent.SetDestination(new Vector3(agent.transform.position.x, agent.transform.position.y, position.z + 20f));
        //agent.transform.position = new Vector3(agent.transform.position.x, agent.transform.position.y, agent.transform.position.z + 0.2f);
        targeter.target = null;
        animator.SetBool("Run", true); 
    }

    private void Update()
    {
        if (!ControllerMethod.keyboardControll)
        {
            agent.enabled = true;
            agent.transform.rotation = Quaternion.Euler(0, 0, 0);
            GetComponent<Animator>().SetFloat("Distance", agent.remainingDistance);             
        }
    }
}
