using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit {

    // Combat template characterisitcss
    private const float attackDelay = 0.35f;
    public float attackWindow = 0.5f;
    public int currentTargetIndex = 0;
    public InterfaceController InterfaceController;
    int swapTarget = 0;

    // Use this for initialization
    void Start()
    {
        // General characteristics
        id = 1;
        type = CreatureType.CRETAURE_TYPE_HUMANOID;
        faction = Faction.FACTION_FRIENDLY;
        movementSpeed = 2;
        scale = 8;
        setScale(scale);

        // Combat template characterisitcs
        attackDamage = 60;
        healthPoints = 100;
        armor = 5;
        attackWindow = 0.5f;

        // Current CombatStatus
        combatStatus = CombatStatus.COMBAT_STATUS_IDLE;
        combatAttackDelay = attackDelay;
        combatAttackWindow = attackWindow;
        unitsInAggroRange = new ArrayList();
        unitsInAttackRange = new ArrayList();
        unitsInCombatWith = new ArrayList();
        target = null;

        // Misc
        player = true;
        InvokeRepeating("checkPlayerDefend", 0f, 0.002f);
        InvokeRepeating("playerSwapTargets", 0f, 0.002f);
        InvokeRepeating("checkPlayerAttack", 0f, 0.002f);
    }

    // Getters
    protected override float getAttackDelay()
    {
        return attackDelay;
    }

    protected override float getCombatAttackWindow()
    {
        return attackWindow;
    }
    // Misc

    protected void checkPlayerAttack()
    {
        if (!inCombat)
            return;

        if (!Input.GetKeyDown(KeyCode.Mouse0))
            return;

        if (combatStatus != CombatStatus.COMBAT_STATUS_ATTACK_READY)
            return;

        if (!getTarget())
            return;

        if (!isInAttackRangeWith(getTarget()))
            return;

        combatStatus = CombatStatus.COMBAT_STATUS_ATTACKING;
    }

    protected void checkPlayerDefend()
    {
        if (!inCombat)
            return;

        GameObject shield = gameObject.transform.Find("Shield").gameObject;

        if (!shield)
            return;

        if (Input.GetKey(KeyCode.Mouse1))
        {

            if (combatStatus != CombatStatus.COMBAT_STATUS_ATTACKING)
            {
                combatStatus = CombatStatus.COMBAT_STATUS_DEFENDING;
                shield.SetActive(true);
                //Debug.Log.Log("Player is defending!");

                if (GameObject.Find("ShieldSound").GetComponent<AudioSource>())
                {
                    GameObject.Find("ShieldSound").GetComponent<AudioSource>().enabled = true;
                }

            }

            return;
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            combatStatus = CombatStatus.COMBAT_STATUS_WAITING_FOR_ATTACK;
            shield.SetActive(false);
            GameObject.Find("ShieldSound").GetComponent<AudioSource>().enabled = false;
            //Debug.Log.Log("Player is not defending anymore!");
            return;
        }

    }

    protected void playerSwapTargets()
    {
        //Debug.Log.LogWarning("Buscando targets");
        if (!inCombat)
            return;

        // No hay más targets posibles
        if (unitsInCombatWith.Count == 1)
            return;

        if (Input.GetKeyDown(KeyCode.Tab))
            swapTarget = 1;

        if (swapTarget == 1)
            if (Input.GetKeyUp(KeyCode.Tab))
                swapTarget = 2;

        if (swapTarget != 2)
            return;

        //Debug.Log.LogWarning("Hay algo conmigo en combate");
        if (getTarget())
        {
            //Debug.Log.LogWarning("Quitando el anterior");
            if (getTarget().gameObject.transform.Find("TargetIndicator"))
                getTarget().gameObject.transform.Find("TargetIndicator").gameObject.SetActive(false);
        }

        // Si es el último, ponemos el primero
        if (unitsInCombatWith.IndexOf(getTarget()) == (unitsInCombatWith.Count - 1))
            setTarget((Unit)unitsInCombatWith[0]);
        else
            setTarget((Unit)unitsInCombatWith[unitsInCombatWith.IndexOf(getTarget())+1]);

        getTarget().gameObject.transform.Find("TargetIndicator").gameObject.SetActive(true);

        swapTarget = 0;

        /*
        //Debug.Log.LogWarning("Poneindo nuevo target");
        try
        {
            setTarget((Unit)unitsInCombatWith[currentTargetIndex]);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            currentTargetIndex = 0;
            return;
        }
        // Activamos el icono de target
       
        currentTargetIndex++;
        // Si es el ultimo de la lista reseteamos el target
        if (currentTargetIndex >= (unitsInCombatWith.Count))
            currentTargetIndex = 0;
            */
    }

    protected override InterfaceController GetInterfaceCotroller()
    {
       return InterfaceController;
    }
}
