using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceDamageArea : MonoBehaviour
{

    // Use this for initialization
    public bool areaActive = false;
    public float timeToActive = 3f;
    public float timeToDamage = 1f;
    public int damage = 10;
    float currentTimeToDamage = 0;

    public ArrayList unitsInside = new ArrayList();

    void Start()
    {
        currentTimeToDamage = timeToDamage;
    }

    // Update is called once per frame
    void Update()
    {
        if (!areaActive)
        {
            timeToActive -= Time.deltaTime;

            if (timeToActive < 0)
                areaActive = true;
            return;
        }
        else
            currentTimeToDamage -= Time.deltaTime;            

        if (currentTimeToDamage < 0)
        {
            foreach (Unit victim in unitsInside)
            {
                Wolf wolf = new Wolf();
                wolf.attackDamage = damage;
                wolf.dealDamageToUnit(victim);
            }
            currentTimeToDamage = timeToDamage;
        }

    }

    private void OnTriggerStay(Collider other)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Unit unit = other.GetComponent<Unit>();

        if (unit == null)
            return;

        if (unit.getFaction() == Unit.Faction.FACTION_FRIENDLY)
            unitsInside.Add(unit);
    }

    private void OnTriggerExit(Collider other)
    {
        Unit unit = other.GetComponent<Unit>();

        if (unit == null)
            return;

        if (unit.getFaction() == Unit.Faction.FACTION_FRIENDLY)
            unitsInside.Remove(unit);
    }

}