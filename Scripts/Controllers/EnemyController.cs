using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    
    public static void SpawnEnemiesByPath(int phase, int path)
    {
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
        Camino[] Camino = (Camino[])FindObjectsOfTypeAll(typeof(Camino));
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos

        if (Camino.Length == 0)
            Debug.Log("Camino vacío");

        foreach (Camino camino in Camino)
        {
            if (camino.phase == phase && camino.path == path)
            {
                //Debug.Log("Cumpliendo criterios de spawn");
                camino.gameObject.SetActive(true);
            }
            else
                camino.gameObject.SetActive(false);
        }
    }
}
