using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DontStopMusic : MonoBehaviour
{

	// Use this for initialization
	void Awake ()
    {
        DontDestroyOnLoad(this.gameObject);
	}

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Alpha" || SceneManager.GetActiveScene().name == "IntroVideo")
            gameObject.SetActive(false);
    }
}
