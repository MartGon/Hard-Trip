using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

/*
 *  Hard Trip
 *  Cavitec Games (2017-2018)
 *  Developed by Defu
 * 
 *  En esta clase se modela cualquier unidad o entidad que pueda entrar en combate.
 *  El combate se modela en la función Update de aquí, es lo mejor para evitar problemas de escalabilidad
 *  Los métodos dentro de cada grupo están ordenados alfabeticamente
 *  
 */

public abstract class Unit : MonoBehaviour
{
    // Faction types
    public enum Faction
    {
        FACTION_FRIENDLY = 0,           // Tomamos siempre como referencia el jugador, por lo que un ejemplo de friendly sería la caravan
        FACTION_ENEMY,                  // Enemigos agresivos, bandidos o lobos
        FACTION_NEUTRAL,                // Los bandidos negociadores serían neutrales en un principio.
    }

    // Creature type, puede ser útil para los looteos
    public enum CreatureType
    {
        CREATURE_TYPE_BEAST,            // Lobos
        CRETAURE_TYPE_HUMANOID,         // Bandidos
        CREATURE_TYPE_MISC              // Caravana
    }

    // Diferentes estados durante el combate
    public enum CombatStatus
    {
        COMBAT_STATUS_IDLE = 0,
        COMBAT_STATUS_ATTACKING,
        COMBAT_STATUS_ATTACK_READY,
        COMBAT_STATUS_WAITING_FOR_ATTACK,
        COMBAT_STATUS_DEFENDING,
        COMBAT_STATUS_STUNNED,
        COMBAT_STATUS_DEAD,
        COMBAT_STATUS_CORPSE
    }

    // General characteristics
    protected Faction faction;
    protected int id;
    protected float movementSpeed;
    protected float scale;
    protected CreatureType type;

    // Combat template characterisitcs
    public int attackDamage;           // Daño base de nuestros atques
    public int healthPoints;           // Nuestros puntos de vida
    protected int armor;                  // Reducción de daño al recibir un golpe
    
    // Current Combat characteristics
    protected CombatStatus combatStatus;
    protected int corpseDuration;
    protected float combatAttackDelay;      // Este valor cambia durante el combate, al llegar a cero se ataca y se resetea al valor de plantilla
    protected float combatAttackWindow;   // Tiempo que dura el ataque
    protected ArrayList unitsInAggroRange;
    protected ArrayList unitsInCombatWith = new ArrayList();
    protected ArrayList unitsInAttackRange;
    protected bool inCombat;
    protected Unit target;
    public float stunnedTime = 0;

    // Entrenamiento de la AI
    public float combatTotalTime = 0;
    public float lastTotalCombatTime = 0;
    public bool startCountingTime = false;

    public int damageDoneToPlayer = 0;
    public int damageDoneToCarriage = 0;
    public int damageRecevied = 0;

    // Misc
    protected bool player;
    protected int updateFrames;

    // Cosas rápidas para el boss
    protected Vector3 startingPosition;
    protected Quaternion startingRotation;

    // Enhanced Combat
    public float damageMultiplier = 1;
    public float attackSpeedMultiplier = 1;
    public float reducingStacksTime = 2f;

    // Getters
    public int getArmor()
    {
        return armor;
    }

    // Con este método cogemos el tiempo entre ataques, definido en la clase hija
    protected abstract float getAttackDelay();

    protected abstract float getCombatAttackWindow();

    public int getId()
    {
        return id;
    }

    protected abstract InterfaceController GetInterfaceCotroller();

    public Unit getClosestUnitInAggroRange()
    {
        // Si está vacío devolvemos null
        if (unitsInAggroRange.Count == 0)
            return null;

        return (Unit)unitsInAggroRange[0];
    }

    public CombatStatus getCombatStatus()
    {
        return combatStatus;
    }

    public Faction getFaction()
    {
        return faction;
    }

    public int getHealthPoints()
    {
        return healthPoints;
    }

    public Unit getTarget()
    {
        return target;
    }

    public ArrayList getUnitsInAttackRange()
    {
        return unitsInAttackRange;
    }

    public ArrayList getUnitsInAggroRange()
    {
        return unitsInAggroRange;
    }

    // Setters

    public void setCombatStatus(CombatStatus combatStatus)
    {
        this.combatStatus = combatStatus;
    }

    public void setHealthPoints(int health)
    {
        this.healthPoints = health;
    }

    public void setScale(float scale_f)
    {
        this.scale = scale_f;
        float scale_x = gameObject.transform.localScale.x * scale;
        float scale_y = gameObject.transform.localScale.y * scale;
        float scale_z = gameObject.transform.localScale.z * scale;
        gameObject.transform.localScale.Set(scale_x, scale_y, scale_z);
    }

    public void setTarget(Unit target)
    {
        this.target = target;
    }

    // Combat

    public void addUnitInAttackRange(Unit victim)
    {
        unitsInAttackRange.Add(victim);
    }

    public void addUnitInAggroRange(Unit victim)
    {
        unitsInAggroRange.Add(victim);
    }

    public void attackUnit(Unit victim)
    {
        //Debug.Log(gameObject.name + " Ataca a " + victim.gameObject.name);

        // Si no se está defencdiendo, le dañamos
        if (victim.getCombatStatus() != Unit.CombatStatus.COMBAT_STATUS_DEFENDING)
            dealDamageToUnit(victim);

        // Aumentamos el multiplicador de daño
        if (this is Player && ConfigController.enhancedCombatMode)
        {
            damageMultiplier += 0.1f;
            attackSpeedMultiplier -= 0.1f;
        }
        playAttackSound();
    }

    public void chaseUnit(Unit victim)
    {
        if (player || !victim)
            return;

        ////Debug.Log(name + ": Persiguiendo a " + victim.name);

        setCurrentAnimation("run");

        NavMeshAgent nav = gameObject.GetComponentInParent<NavMeshAgent>();

        nav.enabled = true;
        nav.SetDestination(victim.gameObject.transform.position);
    }

    public void dealDamageToUnit(Unit victim)
    {
        if(victim.getCombatStatus() == CombatStatus.COMBAT_STATUS_DEAD)
        {
            //Debug.Log("Unidad muerta: " + victim.name + "No se le puede dañar");
            return;
        }

        int dmg = Mathf.RoundToInt(attackDamage * damageMultiplier) - victim.getArmor();

        //Debug.Log("El daño del ataque es" + dmg);

        if (victim.reduceHealthPointsBy(dmg) <= 0)
            victim.setCombatStatus(Unit.CombatStatus.COMBAT_STATUS_DEAD);

        if (victim.isPlayer())
        {
            victim.GetInterfaceCotroller().dealDamageCharacter(dmg);
            damageDoneToPlayer += dmg;

            Debug.Log("Reducioendo daños");
            victim.damageMultiplier -= (victim.damageMultiplier - 1)/2;
            victim.attackSpeedMultiplier *= 2;
            if (victim.damageMultiplier < 1)
                victim.damageMultiplier = 1;
            if (victim.attackSpeedMultiplier > 1)
                victim.attackSpeedMultiplier = 1;
        }
        else if(victim is Carriage)
        {
            //Debug.Log("Funcion Alex de caravanas, el objetivo es " + victim.getId());
            victim.GetInterfaceCotroller().dealDamageCarriage(victim.getId(), dmg);
        }
        else if(victim is Wolf && ((Wolf)victim).isBoss)
        {
            //Debug.Log("Bajando la vida al Boss " + victim.getId());
            victim.GetInterfaceCotroller().dealDamageToBoss(dmg);
        }

        //Debug.Log("La salud restante de " + victim.gameObject.name + " es " + victim.getHealthPoints());
    }

    public void enterInCombatWith(Unit victim)
    {
       // if (type == CreatureType.CREATURE_TYPE_MISC)
         //   GetComponent<NavMeshAgent>().enabled = false;

        if (unitsInCombatWith.Contains(victim))
        {
            //Debug.Log(gameObject.name + ": Ya estoy en combate con: " + victim.name) ;
            return;
        }
            
        if(!inCombat && !player)
        {
            //Debug.Log(gameObject.name + ": Mi nuevo target es " + victim.name);
            setTarget(victim);
        }
        else if(isPlayer() && target == null)
        {
            setTarget(victim);
            if (victim.gameObject.transform.Find("TargetIndicator"))
                victim.gameObject.transform.Find("TargetIndicator").gameObject.SetActive(true);
        }

        inCombat = true;
        startCountingTime = true;
        unitsInCombatWith.Add(victim);
        //Debug.Log(gameObject.name + ": Acabo de entrar en combate, preparando mi ataque");
        setCombatStatus(CombatStatus.COMBAT_STATUS_WAITING_FOR_ATTACK);
    }

    public void exitCombatWith(Unit victim)
    {
        unitsInCombatWith.Remove(victim);
        //Debug.Log(gameObject.name + ": Acabo de salir de combate con " + victim.name);

        // No quedan más enemigos contra los que combatir, salimos de combate
        if (unitsInCombatWith.Count == 0)
            finishCombat();
    }

    public void finishCombat()
    {
        // Reseteamos valores importantes
        inCombat = false;
        target = null;
        reducingStacksTime = 2f;
        

       // if(type == CreatureType.CREATURE_TYPE_MISC)
         //   GetComponent<NavMeshAgent>().enabled = true;

        if(type != CreatureType.CREATURE_TYPE_MISC)
            stopChasing();

        //Debug.Log(gameObject.name + ": Salgo de combate, esperando a más enemigos");

        if(combatStatus != CombatStatus.COMBAT_STATUS_CORPSE)
            setCombatStatus(CombatStatus.COMBAT_STATUS_IDLE);

        setCurrentAnimation("idle");

        if(player)
            resetHealthPoints();

    }

    public void handleDeath()
    {
        if (player)
            SceneManager.LoadScene("GameOver");
        else if(this is Carriage && id == 1)
            SceneManager.LoadScene("GameOver");

        if (this is Wolf && ((Wolf)this).isBoss)
        {
            ((Wolf)this).GetDialogController().trueEndGameDialog();
            GameObject.Find("VictorySound").GetComponent<AudioSource>().enabled = true;
            AudioSource[] audios = ((Wolf)this).GetComponents<AudioSource>();

            foreach (AudioSource audio in audios)
                audio.enabled = false;
        }

        startCountingTime = false;
        lastTotalCombatTime += combatTotalTime;
        combatTotalTime = 0;

        //finishCombat();
        combatStatus = CombatStatus.COMBAT_STATUS_CORPSE;
        setCurrentAnimation("dead");

        foreach (Unit unit in unitsInCombatWith)
            unit.exitCombatWith(this);

        // Desactivamos el icono de target
        if (gameObject.transform.Find("TargetIndicator"))
            gameObject.transform.Find("TargetIndicator").gameObject.SetActive(false);

        // Updateamos la matriz de la AI
        if (this is Carriage)
            return;

        string enemytype = "";
        if (this is Wolf)
            enemytype = "wolf";
        else if(this is Bandit)
            enemytype = "agressiveBandit";
        PrincessController.updateQMatrix(attackDamage, attackDamage, attackDamage, combatAttackDelay + combatAttackWindow, 0.85f, lastTotalCombatTime, damageDoneToPlayer, damageDoneToCarriage, damageRecevied, enemytype);
    }

    public void handleLosingTarget()
    {
        // Si seguimos en combate tras perder el target, elegimos otro nuevo
        if (inCombat)
            setTarget(getClosestUnitInAggroRange());
    }

    public void handleUnitDeath(Unit victim)
    {
        // Eliminamos a esa unidad de todas nuestras listas
        exitCombatWith(victim);
        removeUnitInAggroRange(victim);
        removeUnitInAttackRange(victim);
    }

    public bool isInAttackRangeWith(Unit unit)
    {
        if (unitsInAttackRange.Contains(unit))
            return true;
        return false;
    }

    public int reduceHealthPointsBy(int damage)
    {
       return healthPoints -= damage;
    }

    public void reduceCombatAttackDelayBy(int frames)
    {
        //combatAttackDelay = combatAttackDelay -  frames;
        combatAttackDelay -= Time.deltaTime;
    }

    public void removeUnitInAttackRange(Unit victim)
    {
        unitsInAttackRange.Remove(victim);
    }

    public void removeUnitInAggroRange(Unit victim)
    {
        unitsInAggroRange.Remove(victim);
    }

    public void resetCombatAttackDelay()
    {
        combatAttackDelay = getAttackDelay() + ( Random.value - 0.5f);
        //Debug.Log("Delay: " + combatAttackDelay);
        if (isPlayer())
            combatAttackDelay = getAttackDelay() * attackSpeedMultiplier;
    }

    public void resetAttackWindow()
    {
        combatAttackWindow = getCombatAttackWindow();
    }

    public void resetHealthPoints()
    {
        healthPoints = 100;
        //Debug.Log(name + ":Resetando vida de ");
        GetInterfaceCotroller().resetLife();
    }

    public void stopChasing()
    {
        if (player)
            return;

        NavMeshAgent nav = gameObject.GetComponentInParent<NavMeshAgent>();

        if (nav.enabled)
        {
            // Deshabilitamos el movimiento
            ////Debug.Log("Parando la persecución");
            nav.enabled = false;
            setCurrentAnimation("idle");
        }
        else if(target)
        {
            // Miramos a nuestro objetivo
            Vector3 posicionRelativa = target.gameObject.transform.position - gameObject.transform.position;
            Quaternion rotacion = Quaternion.LookRotation(posicionRelativa);
            transform.SetPositionAndRotation(transform.position, rotacion);
        }

    }

    public void updateAttackStatus(int framesOffset)
    {
        switch(combatStatus)
        {
            case CombatStatus.COMBAT_STATUS_ATTACKING:

                ////Debug.Log(gameObject.name + ": En ventana de ataque!");
                setCurrentAnimation("attack");

                //combatAttackWindow -= framesOffset;
                combatAttackWindow -= Time.deltaTime;

                ////Debug.Log("El combatAttackWindows es " + combatAttackWindow);

                if (combatAttackWindow <= 0)
                {
                    attackUnit(target);

                    resetCombatAttackDelay();
                    resetAttackWindow();
                    combatStatus = CombatStatus.COMBAT_STATUS_WAITING_FOR_ATTACK;
                }

                break;

            case CombatStatus.COMBAT_STATUS_WAITING_FOR_ATTACK:

                setCurrentAnimation("idle");
                reduceCombatAttackDelayBy(framesOffset);

                ////Debug.Log(gameObject.name + ": Mi timer se reduce en " + framesOffset + ", me quedan " + combatAttackDelay + " para poder volver a atacar");

                // Si es menor o igual que 0, nuestro ataque está listo. Reseteamos el timer
                if (combatAttackDelay <= 0)
                {
                    combatStatus = CombatStatus.COMBAT_STATUS_ATTACK_READY;
                    //Debug.Log(gameObject.name + ": Mi siguiente ataque está listo!");
                }

                break;

            case CombatStatus.COMBAT_STATUS_ATTACK_READY:

                break;

            case CombatStatus.COMBAT_STATUS_STUNNED:
                setCurrentAnimation("damage");
                stunnedTime -= Time.deltaTime;
                if (stunnedTime < 0)
                    setCombatStatus(CombatStatus.COMBAT_STATUS_WAITING_FOR_ATTACK);
                break;

            default:
                break;
        }
    }

    public void updateMultipliers()
    {
        reducingStacksTime -= Time.deltaTime;

        if(reducingStacksTime < 0)
        {
            damageMultiplier -= 0.1f;
            attackSpeedMultiplier += 0.1f;
            if (damageMultiplier < 1)
                damageMultiplier = 1;
            if (attackSpeedMultiplier > 1)
                attackSpeedMultiplier = 1;
            reducingStacksTime = 2f;
        }
    }

    // Misc

    public bool isPlayer()
    {
        return player;
    }

    public bool isInCombat()
    {
        return inCombat;
    }

    public void hardcoreFinishCombat()
    {
        target = null;
        unitsInAttackRange = new ArrayList();
        unitsInCombatWith = new ArrayList();
        inCombat = false;
        combatStatus = CombatStatus.COMBAT_STATUS_IDLE;
        //Debug.Log(name + ": Abandonando combate por fuerza bruta");
    }

    // Handling animations
    public void setCurrentAnimation(string animation)
    {
        Animator animator = gameObject.GetComponent<Animator>();
        bool exists = false;

        if (animator)
        {
            // cancelamos todas las demás
            foreach (AnimatorControllerParameter parameter in animator.parameters)
            {
                animator.SetBool(parameter.name, false);
                if (parameter.name == animation) 
                    exists = true;
            }
            ////Debug.Log(name + ": Poniendo la animación " + animation);
            if(exists)
                animator.SetBool(animation, true);
        }
 
    }

    public bool hasAnimationFinished(string animation)
    {
        Animator animator = gameObject.GetComponent<Animator>();

        if (!animator)
            return true;


        if (animator.GetCurrentAnimatorStateInfo(0).IsName(animation))
            return false;
        
        return true;
    }

    // Handling Sounds
    public void playAttackSound()
    {
        if (isPlayer())
        {
            GameObject.Find("CombatSound").GetComponent<AudioSource>().enabled = false;
            GameObject.Find("CombatSound").GetComponent<AudioSource>().enabled = true;
        }
        else if(this is Bandit)
        {
            if (!GetComponent<AudioSource>())
                return;
            GetComponent<AudioSource>().mute = false;
            GetComponent<AudioSource>().enabled = false;
            GetComponent<AudioSource>().enabled = true;
        }else if (this is Wolf)
        {
            if (!GetComponent<AudioSource>())
                return;

            GetComponent<AudioSource>().mute = false;
            GetComponent<AudioSource>().enabled = false;
            GetComponent<AudioSource>().enabled = true;
        }
    }

    // Funcion princiapl
    public void Update()
    {
        updateFrames++;
        if (updateFrames == ConfigController.combatUpdateFrames)
        {
        OldupdateUnit(updateFrames);
        updateFrames = 0;
        }
    }

    public void OldupdateUnit(int frameOffset)
    {

        // Si estamos muertos, desaparecemos
        if (combatStatus == CombatStatus.COMBAT_STATUS_DEAD)
        {
            //Debug.Log(this.gameObject.name + "Ha muerto");
            // Animación de muerte y tras X tiempo desaparecer
            handleDeath();
            return;
        }

        // Manejamos el tiempo de los cadáveres
        if(combatStatus == CombatStatus.COMBAT_STATUS_CORPSE)
        {
            corpseDuration-= frameOffset;
            if (corpseDuration <= 0)
            {
                gameObject.SetActive(false);
                //Debug.Log("Se ha desactivado + " + name);
            }
                
            return;
        }

        // Si somos la caravana, no hacemos nada más
        if (type == CreatureType.CREATURE_TYPE_MISC)
            return;

        

        // Si estamos en combate, comprobamos como va
        if(inCombat)
        {
            // Actualizamos nuestro timer
            updateAttackStatus(frameOffset);
            combatTotalTime += Time.deltaTime;

            if (!target && isPlayer())
            {
                setTarget((Unit)unitsInCombatWith[0]);
                if (target.gameObject.transform.Find("TargetIndicator"))
                    target.gameObject.transform.Find("TargetIndicator").gameObject.SetActive(true);
                return;
            }

            if (getHealthPoints() < 0)
                combatStatus = CombatStatus.COMBAT_STATUS_DEAD;

            if (combatStatus == CombatStatus.COMBAT_STATUS_ATTACKING)
                return;

            // Si nuestro target está muerto, cambiamos al más cercano
            if (target.getCombatStatus() == CombatStatus.COMBAT_STATUS_DEAD ||
                target.getCombatStatus() == CombatStatus.COMBAT_STATUS_CORPSE)
            {
                handleUnitDeath(target);
                handleLosingTarget();
            }

            // Si estamos dentro del rango nos detenemos
            if (isInAttackRangeWith(target))
            {
                if (!isPlayer())
                    stopChasing();

                // Si nuestro ataque está listo, atacamos
                if (combatStatus == CombatStatus.COMBAT_STATUS_ATTACK_READY && !player)
                    combatStatus = CombatStatus.COMBAT_STATUS_ATTACKING;
            }
            // Si no lo estamos, perseguimos al target
            else if (!isPlayer() && getCombatStatus() != CombatStatus.COMBAT_STATUS_STUNNED/*&& type != CreatureType.CREATURE_TYPE_MISC*/)
                chaseUnit(target);
        }
        else if (player)
            updateMultipliers();
    }

    public void updateUnit(int frameOffset)
    {
        // Si estamos muertos, desaparecemos
        if (combatStatus == CombatStatus.COMBAT_STATUS_DEAD)
        {
            //Debug.Log(this.gameObject.name + "Ha muerto");
            // Animación de muerte y tras X tiempo desaparecer
            gameObject.SetActive(false);
            return;
        }







    }
}
