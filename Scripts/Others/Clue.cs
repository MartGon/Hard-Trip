using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clue : MonoBehaviour {

    public int ID;
    public ItemController itemController;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            itemController.Clue(ID);
            this.gameObject.SetActive(false);
            ConfigController.clueTotal++;
        }
    }
}
