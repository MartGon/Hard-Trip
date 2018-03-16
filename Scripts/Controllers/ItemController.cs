using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour {

    public int clueNum = 0;
    private string description;
    public DialogController dialogController;
    private GameObject[] caravan;

    private void Start()
    {
        GameObject.FindGameObjectsWithTag("carriage");
    }


    public void Clue(int ID)
    {
        switch (ID)
        {
            case 1:
                description = "Quien con bestias anda, a aullar aprende";
                dialogController.cluepopup(description);
                break;
            case 2:
                description = "Cuando el zorro escucha gritar a la liebre siempre llega corriendo, pero no para ayudarla";
                dialogController.cluepopup(description);
                break;
            case 3:
                description = "Si hurtas y das, te librarás";
                dialogController.cluepopup(description);
                break;
            case 4:
                description = "De la noche en la espesura, hasta la nieve es oscura";
                dialogController.cluepopup(description);
                break;
            case 5:
                description = "Cuidate de la niebla, comandante. Podría ocultar lo que ni los propios dioses osan mirar.";
                dialogController.cluepopup(description);
                break;
            case 6:
                description = "La blancura de la nieve hace al cisne negro";
                dialogController.cluepopup(description);
                break;

            default:
                description = "Esta pista no está funcionando como debería";
                break;
        }
        clueNum++;
    }
}
