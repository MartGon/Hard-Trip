using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Unit {

    // Combat template characterisitcss
    private float attackDelay = 1.25f;
    public float attackWindow = 0.75f;
    public bool isAplha = false;
    public bool isBoss = false;
    public InterfaceController interfaceController = null;
    public DialogController dialogController = null;

    // Incializamos el lobo
    void Start ()
    {
        // General characteristics
        id = 1;
        type = CreatureType.CREATURE_TYPE_BEAST;
        faction = Faction.FACTION_ENEMY;
        movementSpeed = ConfigController.wolfSpeed;
        scale = 8;
        setScale(scale);

        // Combat template characterisitcs
        attackDamage = 15;
        healthPoints = 100;
        armor = 5;
        if(isAplha)
        {
            attackDamage = 25;
            armor = 15;
        }
        else if (isBoss)
        {
            attackDamage = 30; 
            healthPoints = 1000;
            armor = 15;
            attackDelay = 1.5f;
        }
        attackWindow = 0.75f;

        // Current CombatStatus
        combatStatus = CombatStatus.COMBAT_STATUS_IDLE;
        combatAttackDelay = attackDelay;
        combatAttackWindow = attackWindow;
        unitsInAggroRange = new ArrayList();
        unitsInAttackRange = new ArrayList();
        unitsInCombatWith = new ArrayList();
        target = null;
        inCombat = false;

        // Misc
        player = false;
        corpseDuration = 1000;
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
        if(!isBoss)
            return null;
        return interfaceController;
    }

    public DialogController GetDialogController()
    {
        if (!isBoss)
            return null;
        return dialogController;
    }
}
