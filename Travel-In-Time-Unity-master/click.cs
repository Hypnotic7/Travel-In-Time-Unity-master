using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
  private NavMeshAgent navMeshAgent;
	// Use this for initialization
	void Start ()
	{
	    navMeshAgent = GetComponent<NavMeshAgent>();
	}

	// Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100))
            {
                navMeshAgent.SetDestination(hit.point);
            }
        }

    }
}
