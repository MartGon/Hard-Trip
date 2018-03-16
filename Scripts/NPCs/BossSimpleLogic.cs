using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSimpleLogic : MonoBehaviour {

    public GameObject iceAreaOdd;
    public GameObject iceAreaEven;
    public GameObject bossWall;
    public GameObject AlphaWolf1;
    public GameObject AlphaWolf2;
    public Unit Boss;
    private float restTime = 15f;
    public bool isFinished = false;
    public bool isHealthSet = false;

	// Use this for initialization
	void Start ()
    {
        Boss = GetComponentInParent<Wolf>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!Boss)
            return;

        bossWall.SetActive(true);

        if (!ConfigController.hardMode)
            return;

        if (!isHealthSet)
        {
            Boss.setHealthPoints(2000);
            isHealthSet = true;
        }


        if (Boss.getHealthPoints() < 1500 && Boss.getHealthPoints() > 1000)
        {
            if (AlphaWolf1.GetComponent<Unit>().getCombatStatus() != Unit.CombatStatus.COMBAT_STATUS_CORPSE)
                AlphaWolf1.SetActive(true);
            
            if (AlphaWolf2.GetComponent<Unit>().getCombatStatus() != Unit.CombatStatus.COMBAT_STATUS_CORPSE)
                AlphaWolf2.SetActive(true);

            AlphaWolf2.GetComponent<Unit>().enterInCombatWith(Boss.getTarget());
            AlphaWolf1.GetComponent<Unit>().enterInCombatWith(Boss.getTarget());

            /*
            if (Boss.getCombatStatus() != Unit.CombatStatus.COMBAT_STATUS_STUNNED && restTime != -1)
            {
                Boss.stunnedTime = restTime;
                Boss.setCombatStatus(Unit.CombatStatus.COMBAT_STATUS_STUNNED);
                restTime = -1;
                AlphaWolf1.GetComponent<Unit>().setHealthPoints(30);
                AlphaWolf2.GetComponent<Unit>().setHealthPoints(30);
            }
            */

        }
        else if(Boss.getHealthPoints() < 1000 && Boss.getHealthPoints() > 500)
        {
            Boss.GetComponentInChildren<AggroRange>(true).gameObject.SetActive(true);
            iceAreaOdd.SetActive(true);
        }
        else if(Boss.getHealthPoints() < 500)
        {
            iceAreaEven.SetActive(true);
        }
        else if(Boss.getHealthPoints() <= 0)
        {
            iceAreaOdd.SetActive(false);
            iceAreaEven.SetActive(false);
        }
	}
}
