using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfigController : MonoBehaviour {

    // Valores de configuración
	public static int characterMaxLife = 100;
	public static int carriageMaxLife = 100;
	public static int banditAgressiveMaxLife = 100;
	public static int wolfMaxLife = 100;
	public static int characterSpeed = 5;
	public static int carriageSpeed = 3;
	public static int wolfSpeed = 4;
	public static int banditAgressiveSpeed= 4;
    public static LanguageController.Language lenguaje = LanguageController.Language.LANGUAGE_ENGLISH;

    // Flags
    public static int phase = 1;
    public static bool firstDialog = true;
    public static bool endGame = true;
    public static bool inCinematic = false;
    public static bool hardMode = false;
    public static bool enhancedCombatMode = true;

    // Misc
    public DialogController dialogController;
    public static int clueTotal = 0;

    // Technical values
    public static int combatUpdateFrames = 1;

    private void Update()
    {
        if (firstDialog)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            dialogController.exitToMenuDialog();
        }

    }
    // Use this for initialization
    void Awake ()
    {
        firstDialog = true;
        endGame = true; //TODO cambiar a false
        lenguaje = (LanguageController.Language)Flags.getLenguage();
	}

}
