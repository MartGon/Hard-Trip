using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogController : MonoBehaviour
{
    public CaravanController caravanController;
    public BossController bossController;
    public GameObject panel;
    public Text title;
    public Text description;
    private GameObject[] buttons;
    public Transform LeftTarget;
    public Transform RightTarget;
    public SaveController SaveController;
    public bool init;
    public int phraseAux = 0;
    private DialogType type = DialogType.DIALOG_TYPE_PRINCESS_START;

    enum DialogType
    {
        DIALOG_TYPE_NONE,
        DIALOG_TYPE_PRINCESS_START,
        DIALOG_TYPE_CLUE,
        DIALOG_TYPE_PRINCESS_PATH_DECISION,
        DIALOG_TYPE_END_GAME,
        DIALOG_TYPE_EXIT_GAME,
        DIALOG_TYPE_TRUE_END_GAME
    }

    public List<string> introDialogue;

    // Use this for initialization
    void Start()
    {

        introDialogue = LanguageController.getIntroById(1);
        buttons = GameObject.FindGameObjectsWithTag("button");
        buttons[0].SetActive(false);
        buttons[1].SetActive(false);
        buttons[0].GetComponentInChildren<Text>().text = LanguageController.getMenuOptionById(0);
        buttons[1].GetComponentInChildren<Text>().text = LanguageController.getMenuOptionById(1);

        if (Flags.getLoad())
        {
            SaveController.LoadSavePoint();
            init = false;
            ConfigController.firstDialog = false;
            buttons[0].SetActive(false);
            buttons[1].SetActive(false);
            panel.SetActive(false);
        }
        else
        {
            StartCoroutine(changePrincessDialogue(introDialogue[0], "avatar"));
            init = true;
        }
    

    }

    // Update is called once per frame
    void Update()
    {

        if (init)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Debug.Log ("incrementing phrase");
                phraseAux++;
                if (phraseAux < introDialogue.Count)
                {

                    //Debug.Log("inside phase");

                    if ((phraseAux % 2) == 1)
                    {
                        StartCoroutine(changePrincessDialogue(introDialogue[phraseAux], "princess"));
                    }
                    else
                    {
                        StartCoroutine(changePrincessDialogue(introDialogue[phraseAux], "avatar"));
                    }

                }
                else
                {

                    init = false;
                    buttons[0].SetActive(true);
                    buttons[1].SetActive(true);
                }

            }


        }
        else
        {
            if (ConfigController.firstDialog)
                return;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (type == DialogType.DIALOG_TYPE_PRINCESS_PATH_DECISION)
                    caravanController.MoveCaravan();

                buttons[0].SetActive(false);
                buttons[1].SetActive(false);
                panel.SetActive(false);

            }
        }
    }

    public IEnumerator changePrincessDialogue(string phrase, string character)
    { //SE PUEDE JUNTAR CON EL OTRO CHANGE

        if (character == "princess")
        {
            title.text = LanguageController.getTitleById(2);
        }
        if (character == "avatar")
        {
            title.text = LanguageController.getTitleById(3);
        }
        int letter = 0;
        description.text = "";
        while (letter < phrase.Length)
        {
            description.text += phrase[letter];
            letter += 1;
            yield return new WaitForSeconds(0.02f);

        }

    }

    public void cluepopup(string clue)
    {// se llama desde la pista.

        title.text = LanguageController.getTitleById(1);
        description.text = clue;
        panel.SetActive(true);
        buttons[0].SetActive(false);
        buttons[1].SetActive(false);
    }



    public void princessDialogPopUp(string text)
    {
        type = DialogType.DIALOG_TYPE_PRINCESS_PATH_DECISION;
        title.text = LanguageController.getTitleById(2);
        description.text = text;
        panel.SetActive(true);
        buttons[0].SetActive(false);
        buttons[1].SetActive(false);
    }

    public void endGameDialog()
    {
        type = DialogType.DIALOG_TYPE_END_GAME;
        title.text = LanguageController.getTitleById(5);
        description.text = LanguageController.getTextById(1);
        panel.SetActive(true);
        buttons[0].SetActive(true);
        buttons[1].SetActive(true);

        ConfigController.endGame = true;
    }

    public void trueEndGameDialog()
    {
        type = DialogType.DIALOG_TYPE_TRUE_END_GAME;
        title.text = LanguageController.getTitleById(6);
        description.text = LanguageController.getTextById(2);
        panel.SetActive(true);
        buttons[0].SetActive(true);
        buttons[1].SetActive(true);

        ConfigController.endGame = true;
    }

    public void exitToMenuDialog()
    {
        type = DialogType.DIALOG_TYPE_EXIT_GAME;
        title.text = LanguageController.getTitleById(4) ;
        description.text = LanguageController.getTextById(3);
        panel.SetActive(true);
        buttons[0].SetActive(true);
        buttons[1].SetActive(true);

    }

    public void hardModeOn()
    {
        type = DialogType.DIALOG_TYPE_PRINCESS_PATH_DECISION;
        title.text = LanguageController.getTitleById(2);
        description.text = LanguageController.getTextById(6);
        panel.SetActive(true);
        buttons[0].SetActive(false);
        buttons[1].SetActive(false);

    }

    public void sayYes()
    {
        switch(type)
        {
            case DialogType.DIALOG_TYPE_PRINCESS_START:
                ConfigController.firstDialog = false;
                leftPath();
                break;
            case DialogType.DIALOG_TYPE_END_GAME:
            case DialogType.DIALOG_TYPE_TRUE_END_GAME:
                panel.SetActive(false);
                buttons[0].SetActive(false);
                buttons[1].SetActive(false);
                SaveController.SavePoint(ConfigController.phase, 0, 0, 0, 0, 0);
                SceneManager.LoadScene("level2");
                break;
            case DialogType.DIALOG_TYPE_EXIT_GAME:
                SceneManager.LoadScene("Menu");
                break;
            default:
                break;

        }
    }

    public void sayNo()
    {
        switch (type)
        {
            case DialogType.DIALOG_TYPE_PRINCESS_START:
                ConfigController.firstDialog = false;
                RightPath();
                break;
            case DialogType.DIALOG_TYPE_END_GAME:
                panel.SetActive(false);
                buttons[0].SetActive(false);
                buttons[1].SetActive(false);
                bossController.spawnBoss();
                LightController.SwapLight(false);
                CinematicController.startCinematic();
                SaveController.SavePoint(ConfigController.phase, 0, 0, 0, 0, 0);
                break;
            case DialogType.DIALOG_TYPE_TRUE_END_GAME:
                panel.SetActive(false);
                buttons[0].SetActive(false);
                buttons[1].SetActive(false);
                LightController.SwapLight(true);
                break;
            case DialogType.DIALOG_TYPE_EXIT_GAME:
                panel.SetActive(false);
                buttons[0].SetActive(false);
                buttons[1].SetActive(false);
                break;
            default:
                break;
        }
    }

    public void leftPath()
    {
        caravanController.ChangeTarget(LeftTarget);
        panel.SetActive(false);
        EnemyController.SpawnEnemiesByPath(1, 1);
    }

    public void RightPath()
    {
        caravanController.ChangeTarget(RightTarget);
        panel.SetActive(false);
        EnemyController.SpawnEnemiesByPath(1, 2);
    }

}
