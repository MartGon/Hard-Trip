using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionPrincessTrigger : MonoBehaviour {

    public CaravanController caravanController;
    public DialogController dialogController;
    public SaveController saveController;
    public Transform targetIzq;
    public Transform targetDcha;
    public string princessText;
    public bool loading;
    public int loadedValue;

    private void OnTriggerEnter(Collider other)
    {

        if (!other.GetComponent<Carriage>())
            return;

        GameObject[] Caravan = GameObject.FindGameObjectsWithTag("carriage");
        GameObject[] Player = GameObject.FindGameObjectsWithTag("Player");

        Player[0].GetComponent<Player>().hardcoreFinishCombat();

        for (int i = 0; i < Caravan.Length; i++)
        {
            Caravan[i].GetComponent<Carriage>().hardcoreFinishCombat();
        }

        int decision = 0;
        if (!loading)
        {
            //llamamos a la prinsesa
            //System.Random rand = new System.Random();
            Debug.Log("El total de pistas es " + ConfigController.clueTotal);
            decision = PrincessController.makeDecision(ConfigController.phase, ConfigController.clueTotal);
            ConfigController.clueTotal = 0;

            saveController.SavePoint(ConfigController.phase, decision, PrincessController.Q_Matrix[0], PrincessController.Q_Matrix[1],
           PrincessController.iterations[0], PrincessController.iterations[1]);

            //ConfigController.phase++;
        }
        else
        {
            decision = loadedValue;
            ConfigController.clueTotal = 0;
        }


        // Desactivamos el flag
        loading = false;
        ConfigController.phase++;
        switch (decision)
        {
            case 1:
                if (targetIzq.GetComponent<PhaseTarget>())
                    EnemyController.SpawnEnemiesByPath(targetIzq.GetComponent<PhaseTarget>().phase, targetIzq.GetComponent<PhaseTarget>().path);
                caravanController.ChangeTarget(targetIzq);
                princessText = LanguageController.getTextById(4) + "\nQ[0](Lobos)= " + PrincessController.Q_Matrix[0] + "\nQ[1](Bandidos)= " + PrincessController.Q_Matrix[1];
                break;
            default:
                if (targetDcha.GetComponent<PhaseTarget>())
                    EnemyController.SpawnEnemiesByPath(targetDcha.GetComponent<PhaseTarget>().phase, targetDcha.GetComponent<PhaseTarget>().path);
                caravanController.ChangeTarget(targetDcha);
                princessText = LanguageController.getTextById(5) + "\nQ[0](Lobos)= " + PrincessController.Q_Matrix[0] + "\nQ[1](Bandidos)= " + PrincessController.Q_Matrix[1];
                break;
        }

        dialogController.princessDialogPopUp(princessText);
        caravanController.StopCaravan();
        this.gameObject.SetActive(false);
    }

    public void load(bool load, int loadedValue)
    {
        this.loading = load;
        this.loadedValue = loadedValue;

    }
}
