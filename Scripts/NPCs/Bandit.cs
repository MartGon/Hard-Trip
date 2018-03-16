using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bandit : Unit
{

    // Combat template characterisitcss
    private const float attackDelay = 1.25f;
    public float attackWindow  = 2f;

    // Incializamos el bandido
    void Start()
    {
        // General characteristics
        id = 1;
        type = CreatureType.CRETAURE_TYPE_HUMANOID;
        faction = Faction.FACTION_ENEMY;
        movementSpeed = ConfigController.banditAgressiveSpeed;
        scale = 8;
        setScale(scale);

        // Combat template characterisitcs
        attackDamage = 15;
        healthPoints = ConfigController.banditAgressiveMaxLife;
        armor = 5;
        attackWindow = 1f;
        //Debug.LogWarning("La duración en frames de la aniamción es " + attackWindow);

        // Current CombatStatus
        combatStatus = CombatStatus.COMBAT_STATUS_IDLE;
        combatAttackDelay = attackDelay;
        combatAttackWindow = (float)attackWindow;
        unitsInAggroRange = new ArrayList();
        unitsInAttackRange = new ArrayList();
        unitsInCombatWith = new ArrayList();
        target = null;
        inCombat = false;

        // Misc
        player = false;
        setAccelerationAndSpeed(30, 25);
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
        return null;
    }

    // Movement Speed
    protected void setAccelerationAndSpeed(float acceleration, float speed)
    {
        NavMeshAgent nav = GetComponent<NavMeshAgent>();

        if (!nav)
            return;

        nav.acceleration = acceleration;
        nav.speed = speed;
    }
}
