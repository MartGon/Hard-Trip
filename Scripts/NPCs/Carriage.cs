using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carriage : Unit
{

    // Combat template characterisitcss
    private const int attackDelay = 800;
    private const int attackWindow = 30;
    public int carriageId = 0;
    public Unit mytarget;
    public InterfaceController InterfaceController;

    // Use this for initialization
    void Awake()
    {
        // General characteristics
        id = carriageId;
        //Debug.Log("El id de esta caravana es: " + id);
        type = CreatureType.CREATURE_TYPE_MISC;
        faction = Faction.FACTION_FRIENDLY;
        movementSpeed = ConfigController.carriageSpeed;
        scale = 8;
        //setScale();

        // Combat template characterisitcs
        attackDamage = 0;
        healthPoints = ConfigController.carriageMaxLife;
        armor = 5;

        // Current CombatStatus
        combatStatus = CombatStatus.COMBAT_STATUS_IDLE;
        combatAttackDelay = attackDelay;
        unitsInAggroRange = new ArrayList();
        unitsInAttackRange = new ArrayList();
        unitsInCombatWith = new ArrayList();
        target = null;
        inCombat = false;

        // Misc
        player = false;
    }

    private void FixedUpdate()
    {
        mytarget = getTarget();
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

    protected override InterfaceController GetInterfaceCotroller()
    {
        return InterfaceController;
    }
}
