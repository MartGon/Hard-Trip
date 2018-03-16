using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour {

    public Text TextLanguage;
    private void Start()
    {
        Flags.setLenguage(0);
        Flags.setLoad(false);
    }
    public void NewGame(string level)
    {
        Debug.LogWarning("Change to level 1");
        SceneManager.LoadScene(level);
    }

    public void ExitGame()
    {
        Debug.LogWarning("Exiting");
        Application.Quit();
    }
    public void LoadGame() 
    {
        Flags.setLoad(true);
        SceneManager.LoadScene("Alpha");
    }
    public void Language()
    {
        if (Flags.getLenguage() == 1)
        {
            Flags.setLenguage(0);
            TextLanguage.text = "Español";
            Debug.LogError(Flags.getLenguage());

        }
        else
        {
            Flags.setLenguage(1);
            TextLanguage.text = "Inglés";
            Debug.LogError(Flags.getLenguage());
        }
    }
}
