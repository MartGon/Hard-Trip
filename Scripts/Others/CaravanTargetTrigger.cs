using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CaravanTargetTrigger : MonoBehaviour
{
    public DialogController dialogController;
    public CaravanController caravanController;
    public Transform target;
    public BossController bossController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Carriage>())
            if (gameObject.GetComponent<PhaseTarget>())
                if (gameObject.GetComponent<PhaseTarget>().phase == 3 && gameObject.GetComponent<PhaseTarget>().path == 3)
                {
                     dialogController.endGameDialog();
                    //bossController.spawnBoss();
                    //CinematicController.startCinematic();
                    this.gameObject.SetActive(false);
                }

        if (other.gameObject.CompareTag("carriage"))
            caravanController.ChangeTarget(target);
        //this.gameObject.SetActive(false);
    }
}
