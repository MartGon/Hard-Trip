using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour {

    private static GameObject[] lights;
    private void Start()
    {
        lights=GameObject.FindGameObjectsWithTag("Light");

    }

    public static void SwapLight(bool change)
    {
        for(int i=0; i<lights.Length; i++)
        {
            lights[i].SetActive(change);
        }
    }
}
