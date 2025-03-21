using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fastener : MonoBehaviour
{
    public WeeklyFinances wf;
    GameObject[] agents;
    void Start()
    {
        agents = GameObject.FindGameObjectsWithTag("Bot");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            foreach(GameObject agent in agents)
            {
                NavMeshAgent curAgent = agent.GetComponent<NavMeshAgent>();
                curAgent.acceleration = 150;
                curAgent.speed = 42;
            }
            wf.dayLengthSeconds = 1.5f;
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            foreach (GameObject agent in agents)
            {
                NavMeshAgent curAgent = agent.GetComponent<NavMeshAgent>();
                curAgent.acceleration = 75;
                curAgent.speed = 21;
            }
            wf.dayLengthSeconds = 3f;
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            foreach (GameObject agent in agents)
            {
                NavMeshAgent curAgent = agent.GetComponent<NavMeshAgent>();
                curAgent.acceleration = 500;
                curAgent.speed = 84;
            }
            wf.dayLengthSeconds = 0.75f;
        }
    }
}
