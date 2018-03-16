using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyControllers : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {

        if (SceneManager.GetActiveScene().name == "Alpha")
            DontDestroyOnLoad(this.gameObject);
            
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(SceneManager.GetActiveScene().name != "Alpha" && SceneManager.GetActiveScene().name != "level2")
            Destroy(this.gameObject);
    }
}
