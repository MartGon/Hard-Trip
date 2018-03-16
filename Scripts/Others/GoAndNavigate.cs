using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoAndNavigate : MonoBehaviour {

   public Transform Target;
   public NavMeshAgent myNav;

	// Use this for initialization
	void Start ()
    {
        myNav = GetComponent<NavMeshAgent>();

	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Target != null)
            if(myNav.enabled)
                myNav.SetDestination(Target.position);

	}
}
