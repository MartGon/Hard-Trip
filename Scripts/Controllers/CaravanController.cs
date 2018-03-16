using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CaravanController : MonoBehaviour {

   private GameObject[] Caravan;

    private void Start()
    {
        //AVISO: Solo si se utiliza el mismo tag para todos los carruajes.
        Caravan=GameObject.FindGameObjectsWithTag("carriage");
    }

    public void ChangeTarget(Transform newTarget)
    {
        for(int i=0; i<Caravan.Length; i++)
        {
            if(!Caravan[i].GetComponent<NavMeshAgent>().enabled)
                Caravan[i].GetComponent<NavMeshAgent>().enabled= true;

            Caravan[i].GetComponent<GoAndNavigate>().Target = newTarget;
        }
    }

    public void StopCaravan()
    {
        for (int i = 0; i < Caravan.Length; i++)
        {
            Caravan[i].GetComponent<NavMeshAgent>().enabled = false;
        }
    }

    public void MoveCaravan()
    {
        for(int i = 0; i < Caravan.Length; i++)
        {
            Caravan[i].GetComponent<NavMeshAgent>().enabled = true;
        }
    }

    public void disableCaravan()
    {
        for(int i = 0; i < Caravan.Length; i++)
        {
            Caravan[i].SetActive(false);
        }
    }

}
