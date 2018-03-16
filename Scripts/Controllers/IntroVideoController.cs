using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroVideoController : MonoBehaviour
{
	
    public float timeLeft = 41;

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;

        if (timeLeft < 0 ||Input.GetKeyDown(KeyCode.Space)) 
        {
            SceneManager.LoadScene("LoadingScreen");
        }
    }
}

