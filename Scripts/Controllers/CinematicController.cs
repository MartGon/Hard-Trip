using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CinematicController : MonoBehaviour
{
    public Camera camera1;
    public Camera camera2;
    public Camera camera3;
    public Camera camera4;
    public Camera cameraPlayer;
    public GameObject boss;
    public GameObject player;
    public GameObject playerPosition;
    public GameObject HUD;
    public static bool start = false;
    public static bool done = false;
    private const float fixedTime = 1.8f;
    public float time = 1.8f;
    public AnimationPhase aPhase = AnimationPhase.ANIMATION_PHASE_1;

    public enum AnimationPhase
    {
        ANIMATION_PHASE_1,
        ANIMATION_PHASE_2,
        ANIMATION_PHASE_3,
        ANIMATION_PHASE_4,
        ANIMATION_PHASE_END
    }

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!start)
            return;

        if (done)
            return;

        // Preparativos
        cameraPlayer.gameObject.SetActive(false);
        ConfigController.inCinematic = true;

        // Desactivamos el Aggro del Jefe
        GameObject aggroRange = boss.GetComponentInChildren<AggroRange>(true).gameObject;
        aggroRange.SetActive(false);
        HUD.SetActive(false);

        // Movemos al jugador a la posición
        player.SetActive(false);
        player.transform.position = playerPosition.transform.position;
        player.transform.rotation = new Quaternion(0, 180, 0, 0);
        player.SetActive(true);

        switch (aPhase)
        {
            case AnimationPhase.ANIMATION_PHASE_1:
                camera1.gameObject.SetActive(true);
                time -= Time.deltaTime;
                if (time < 0)
                {
                    camera2.gameObject.SetActive(true);
                    camera1.gameObject.SetActive(false);
                    aPhase = AnimationPhase.ANIMATION_PHASE_2;
                    time = fixedTime;
                }
                break;
            case AnimationPhase.ANIMATION_PHASE_2:
                time -= Time.deltaTime;
                if (time < 0)
                {
                    camera3.gameObject.SetActive(true);
                    camera2.gameObject.SetActive(false);
                    aPhase = AnimationPhase.ANIMATION_PHASE_3;
                    time = fixedTime;
                }
                break;
            case AnimationPhase.ANIMATION_PHASE_3:
                time -= Time.deltaTime;
                if (time < 0)
                {
                    camera4.gameObject.SetActive(true);
                    camera3.gameObject.SetActive(false);
                    aPhase = AnimationPhase.ANIMATION_PHASE_4;
                    time = fixedTime;
                }
                break;
            case AnimationPhase.ANIMATION_PHASE_4:
                time -= Time.deltaTime;
                if (time < 0)
                {
                    camera4.gameObject.SetActive(false);
                    aPhase = AnimationPhase.ANIMATION_PHASE_END;
                    start = false;
                    time = fixedTime;
                    cameraPlayer.gameObject.SetActive(true);
                    aggroRange.SetActive(true);
                    ConfigController.inCinematic = false;
                    HUD.SetActive(true);
                }
                break;
            default:
                break;

        }
	}

    public static void startCinematic()
    {
        start = true;
        done = false;
    }

}
