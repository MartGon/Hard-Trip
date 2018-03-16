using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour {

    public GameObject Boss;
    public CaravanController caravanController;
    public InterfaceController interfaceController;
    public DialogController dialogController;

    private EstadoKey estado = EstadoKey.ESTADO_KEY_PRESSED_NONE;

    enum EstadoKey
    {
        ESTADO_KEY_PRESSED_NONE,
        ESTADO_KEY_PRESSED_C,
        ESTADO_KEY_PRESSED_A,
        ESTADO_KEY_PRESSED_V,
        ESTADO_KEY_PRESSED_I,
        ESTADO_KEY_PRESSED_T,
        ESTADO_KEY_PRESSED_E
    }

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!ConfigController.endGame)
            return;

        switch(estado)
        {
            case EstadoKey.ESTADO_KEY_PRESSED_NONE:
                ////Debug.Log("En estado No");
                if (Input.GetKeyDown(KeyCode.C))
                    estado = EstadoKey.ESTADO_KEY_PRESSED_C;
                else if (Input.inputString.Length != 0)
                {
                    estado = EstadoKey.ESTADO_KEY_PRESSED_NONE;
                    //Debug.Log("Se pulsó otra tecla: " + Input.inputString.Length);
                }
                break;
            case EstadoKey.ESTADO_KEY_PRESSED_C:
                //Debug.Log("En estaod C");
                if (Input.GetKeyDown(KeyCode.A))
                    estado = EstadoKey.ESTADO_KEY_PRESSED_A;
                else if (Input.inputString.Length != 0)
                {
                    estado = EstadoKey.ESTADO_KEY_PRESSED_NONE;
                    //Debug.Log("Se pulsó otra tecla: " + Input.inputString);
                }
                break;
            case EstadoKey.ESTADO_KEY_PRESSED_A:
                //Debug.Log("En estaod A");
                if (Input.GetKeyDown(KeyCode.V))
                    estado = EstadoKey.ESTADO_KEY_PRESSED_V;
                else if (Input.inputString.Length != 0)
                {
                    estado = EstadoKey.ESTADO_KEY_PRESSED_NONE;
                    //Debug.Log("Se pulsó otra tecla: " + Input.inputString);
                }
                break;
            case EstadoKey.ESTADO_KEY_PRESSED_V:
                //Debug.Log("En estaod V");
                if (Input.GetKeyDown(KeyCode.I))
                    estado = EstadoKey.ESTADO_KEY_PRESSED_I;
                else if (Input.inputString.Length != 0)
                {
                    estado = EstadoKey.ESTADO_KEY_PRESSED_NONE;
                    //Debug.Log("Se pulsó otra tecla: " + Input.inputString);
                }
                break;
            case EstadoKey.ESTADO_KEY_PRESSED_I:
                //Debug.Log("En estaod I");
                if (Input.GetKeyDown(KeyCode.T))
                    estado = EstadoKey.ESTADO_KEY_PRESSED_T;
                else if (Input.inputString.Length != 0)
                {
                    estado = EstadoKey.ESTADO_KEY_PRESSED_NONE;
                    //Debug.Log("Se pulsó otra tecla: " + Input.inputString);
                }
                break;
            case EstadoKey.ESTADO_KEY_PRESSED_T:
                //Debug.Log("En estaod T");
                if (Input.GetKeyDown(KeyCode.E))
                    estado = EstadoKey.ESTADO_KEY_PRESSED_E;
                else if (Input.inputString.Length != 0)
                {
                    estado = EstadoKey.ESTADO_KEY_PRESSED_NONE;
                    //Debug.Log("Se pulsó otra tecla: " + Input.inputString);
                }
                break;
            case EstadoKey.ESTADO_KEY_PRESSED_E:
                //Debug.Log("En estaod E");
                if (Input.GetKeyDown(KeyCode.C))
                {
                    estado = EstadoKey.ESTADO_KEY_PRESSED_NONE;
                    //spawnBoss();
                    ConfigController.hardMode = true;
                    //dialogController.hardModeOn();
                }
                else if (Input.inputString.Length != 0)
                {
                    estado = EstadoKey.ESTADO_KEY_PRESSED_NONE;
                    //Debug.Log("Se pulsó otra tecla: " + Input.inputString);
                }
                break;
            default:
                break;


        }

	}

    public void checkKeyCode(KeyCode keyCode)
    {
        if (Input.GetKeyDown(keyCode))
            setEstadoByKey(keyCode);
        else if (Input.inputString.Length != 0)
        {
            estado = EstadoKey.ESTADO_KEY_PRESSED_NONE;
            //Debug.Log("Se pulsó otra tecla: " + Input.inputString);
        }
    }

    public void setEstadoByKey(KeyCode keyCode)
    {
        switch(keyCode)
        {
            case KeyCode.C:
                if (estado == EstadoKey.ESTADO_KEY_PRESSED_E)
                {
                    //spawnBoss();
                    ConfigController.hardMode = true;
                    //dialogController.hardModeOn();
                    return;
                }

                estado = EstadoKey.ESTADO_KEY_PRESSED_A;
                break;
            case KeyCode.A:
                estado = EstadoKey.ESTADO_KEY_PRESSED_V;
                break;
            case KeyCode.V:
                estado = EstadoKey.ESTADO_KEY_PRESSED_I;
                break;
            case KeyCode.I:
                estado = EstadoKey.ESTADO_KEY_PRESSED_T;
                break;
            case KeyCode.T:
                estado = EstadoKey.ESTADO_KEY_PRESSED_E;
                break;
            case KeyCode.E:
                estado = EstadoKey.ESTADO_KEY_PRESSED_C;
                break;
            default:
                estado = EstadoKey.ESTADO_KEY_PRESSED_NONE;
                break;

        }
    }


    public void spawnBoss()
    {
        //Debug.Log("Spawneando Boss");
        Boss.SetActive(true);
        caravanController.disableCaravan();
        interfaceController.bossHealthBar.gameObject.SetActive(true);
        interfaceController.bossHealthBar.gameObject.GetComponentInChildren<Text>().text = LanguageController.getTitleById(7);
        AudioSource[] audios = GameObject.FindWithTag("Player").GetComponents<AudioSource>();

        foreach (AudioSource audio in audios)
            audio.enabled = false;
    }
}
