using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SaveController: MonoBehaviour {
    public  GameObject[] Caravan;
    public  GameObject Character;
    public Transform savepoint2;
    public  InterfaceController InterfaceController;
    public DecisionPrincessTrigger decisionPrincessTrigger;


    

    public  void SavePoint(int phase, int decision, float qmatrix0, float qmatrix1, int iterations0, int iterations1)
    {
        //guardamos los valores de la AI
        PlayerPrefs.SetInt("Phase", phase);
        PlayerPrefs.SetFloat("QMatrix0", qmatrix0);
        PlayerPrefs.SetFloat("QMatrix1", qmatrix1);
        PlayerPrefs.SetInt("Iterations0", iterations0);
        PlayerPrefs.SetInt("Iterations1", iterations1);

        //guardamos decision del camino 
        PlayerPrefs.SetInt("Decision", decision);

        for (int i = 0; i < Caravan.Length; i++)
            {
                //guarda los puntos de vida de cada caravana
                PlayerPrefs.SetInt("CarriageHealth"+ Caravan[i].GetComponent<Carriage>().getId(),
                    Caravan[i].GetComponent<Carriage>().getHealthPoints());
                //guardamos la posicion de la caravana
                PlayerPrefs.SetFloat("PosCar" + Caravan[i].GetComponent<Carriage>().getId() + "X", Caravan[i].transform.position.x);
                PlayerPrefs.SetFloat("PosCar" + Caravan[i].GetComponent<Carriage>().getId() + "Y", Caravan[i].transform.position.y);
                PlayerPrefs.SetFloat("PosCar" + Caravan[i].GetComponent<Carriage>().getId() + "Z", Caravan[i].transform.position.z);
                PlayerPrefs.SetFloat("RotCar" + Caravan[i].GetComponent<Carriage>().getId() + "X", Caravan[i].transform.rotation.x);
                PlayerPrefs.SetFloat("RotCar" + Caravan[i].GetComponent<Carriage>().getId() + "Y", Caravan[i].transform.rotation.y);
                PlayerPrefs.SetFloat("RotCar" + Caravan[i].GetComponent<Carriage>().getId() + "Z", Caravan[i].transform.rotation.z);
                PlayerPrefs.SetFloat("RotCar" + Caravan[i].GetComponent<Carriage>().getId() + "W", Caravan[i].transform.rotation.w);

        }
           
            

        //guardamos posiciones de caravanas y personaje
        PlayerPrefs.SetFloat("PosCharX", Character.transform.position.x);
        PlayerPrefs.SetFloat("PosCharY", Character.transform.position.y);
        PlayerPrefs.SetFloat("PosCharZ", Character.transform.position.z);

        
       
    }
   
    public void LoadSavePoint()
    {
        //Cargamos los valores de la AI
        PrincessController.loadAI(PlayerPrefs.GetFloat("QMatrix0"), PlayerPrefs.GetFloat("QMatrix1"),
            PlayerPrefs.GetInt("Iterations0"), PlayerPrefs.GetInt("Iterations1"));
        decisionPrincessTrigger.load(true, PlayerPrefs.GetInt("Decision"));
        ConfigController.phase = PlayerPrefs.GetInt("Phase");
        //Character.transform.position = new Vector3(0, 0, 0);

        //Posicion del personaje
        Character.SetActive(false);
        Character.transform.position = new Vector3(PlayerPrefs.GetFloat("PosCharX"), 
        PlayerPrefs.GetFloat("PosCharY"), PlayerPrefs.GetFloat("PosCharZ"));

       // Debug.LogError("PosCharX " + PlayerPrefs.GetFloat("PosCharX"));
       // Debug.LogError("PosCharY " + PlayerPrefs.GetFloat("PosCharY"));
       // Debug.LogError("PosCharZ " + PlayerPrefs.GetFloat("PosCharZ"));
        Character.SetActive(true);

        //Posicion de las caravanas
        for (int i = 0; i < Caravan.Length; i++)
        {
            Caravan[i].transform.position = new Vector3(PlayerPrefs.GetFloat("PosCar" + i + "X"),
                PlayerPrefs.GetFloat("PosCar" + i + "Y"), PlayerPrefs.GetFloat("PosCar" + i + "Z"));
            Caravan[i].transform.rotation = new Quaternion(PlayerPrefs.GetFloat("RotCar" + i + "X"),
                PlayerPrefs.GetFloat("RotCar" + i + "Y"), PlayerPrefs.GetFloat("RotCar" + i + "Z"), PlayerPrefs.GetFloat("RotCar" + i + "W"));
            int health = PlayerPrefs.GetInt("CarriageHealth" + i);
            Caravan[i].GetComponent<Carriage>().setHealthPoints(health);
           InterfaceController.dealDamageCarriage(i, 100 - health);
            if (health <= 0)
            {
                Caravan[i].SetActive(false);
            }
            else Caravan[i].GetComponent<NavMeshAgent>().enabled = true;
           


        }
        //Debug.LogError(PlayerPrefs.GetInt("Phase"));
        if (PlayerPrefs.GetInt("Phase") == 2)
        {
            savepoint2.GetComponent<BoxCollider>().enabled = true;
            savepoint2.GetComponent<SphereCollider>().enabled = false;
        }

    }
}
