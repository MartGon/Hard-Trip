using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Flags : MonoBehaviour {
    private static int Lenguage=0;
    private static bool Load=false;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public static void setLenguage(int ID_lenguage)
    {
        Lenguage = ID_lenguage;
    }
    public static int getLenguage()
    {
        return Lenguage;
    }

    public static void setLoad(bool change)
    {
        Load = change;
    }
    public static bool getLoad()
    {
        return Load;
    }

}
