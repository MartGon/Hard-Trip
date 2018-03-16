using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceController : MonoBehaviour {

    // Vida del jugador
    public Scrollbar healthBar;
    public int lifeValue;

    // Vida del Boss
    public Scrollbar bossHealthBar;
    public int bossLifeValue;

    // Vida de las caravanas
	public Text[] carriagesText = new Text[3];

    // Total de pistas
    public Text clues;

    // Multiplicador de daño
    public Text damageMultiplier;
    public GameObject visualEffect;
    public Player player;

    // Misc
    public Canvas inventoryCanvas;
	public Canvas princessDialogue;
	public Canvas clueDescription;
	public Canvas adviseCanvas;

	void Awake()
	{		
		for(int i=0 ; i<3 ; i++){
			carriagesText[i].text ="100";
		}
	}

    private void Update()
    {
        clues.text = ConfigController.clueTotal.ToString();
        damageMultiplier.text = player.damageMultiplier.ToString();
        if (player.damageMultiplier >= 1.5f)
            visualEffect.SetActive(true);
        else
            visualEffect.SetActive(false);
    }

 

	//reset character life
	public void resetLife()
    {

		lifeValue = ConfigController.characterMaxLife;
        healthBar.size = ConfigController.characterMaxLife;

    }

	//decrement the healthbar for the character
	public void dealDamageCharacter(int damage)
	{ //Resta a la vida la entrada damage
		lifeValue = lifeValue - damage;
		if (lifeValue <= 0)
		{
			lifeValue = 0;
			Destroy(this.gameObject);
		}
		healthBar.size = lifeValue / 100f;
	}

    //decrement the healthbar for the boss
    public void dealDamageToBoss(int damage)
    { //Resta a la vida la entrada damage
        if(ConfigController.hardMode)
            damage /= 2;
        bossLifeValue = bossLifeValue - damage;
        if (bossLifeValue <= 0)
        {
            bossLifeValue = 0;
            Destroy(this.gameObject);
        }
        bossHealthBar.size = bossLifeValue / 1000f;

    }

    //dechrement de life fot the carriages, should indicate the carriage number
    public void dealDamageCarriage(int objective, int damage)
    {
		int actual = 0;

		Int32.TryParse(carriagesText[objective].text, out actual);

		if(actual>damage){
			carriagesText[objective].text= (actual-damage).ToString();
		}else{
			carriagesText[objective].text= "X";
		}
	}


}
