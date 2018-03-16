using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincessController : MonoBehaviour {

	private static int damageVsPlayer;
	private static int damageVsCarriage;
	private static int damageVsEnemy;
	private static int combatTime;
	private static int enemyCadence;

	public static float[] Q_Matrix = new float[2];

	private static float[] alphaValues = { 0.5f, 0.2f, 0.1f, 0.5f };

	public static int[] iterations = { 0, 0 };

	public static int[,] ways = {{0,1},{1,0}};
    
                                        // Daño base                                // Cadencias                            // Daño hecho al final del combate                          // Tipo de enemigos
	public static void updateQMatrix(int damageVsP, int damageVsC, int damageVsE, float enemyCad, float playerCad, float time, int damageDealVsPlayer, int damageDealVsCarriage, int damageDealVsEnemy, string enemyType){

		int actualEnemy=0;
		float maxPuntuation = 0;
		float minPuntuation = 0;
		float actualPuntuation = 0;
		float weightedValue = 0;

		switch (enemyType) {
		case "wolf":
			actualEnemy = 0;
			break;
		case "agressiveBandit":
			actualEnemy = 1;
			break;
		default:
			break;
		}

        //Debug.Log.Log("Los daños son " + damageVsP + " " + damageVsC + " " + damageVsE);
        //Debug.Log.Log("Las cadencias son " + enemyCad + " " + playerCad);
        //Debug.Log.Log("Los tiempos son " + time);
        //Debug.Log.Log("Los daños hechos son " + damageDealVsPlayer + " " + damageDealVsCarriage + " " + damageVsEnemy);
        //Debug.Log.Log("La matriz antes de nada es " + Q_Matrix[actualEnemy]);

		//Calculate MaxPuntuation
		maxPuntuation = (damageVsP/enemyCad) + (2*damageVsC/enemyCad);

		//Calculate MinPuntuation
		minPuntuation = -damageVsE/playerCad;

		//Calculate ActualPuntuation
		actualPuntuation = (damageDealVsPlayer+2*damageDealVsCarriage-damageDealVsEnemy)/time;

		//EscalateValue
		weightedValue = ((actualPuntuation-minPuntuation)*100)/(maxPuntuation-minPuntuation);

		//the real update of the array
		Q_Matrix[actualEnemy]= Q_Matrix[actualEnemy]+alphaValues[iterations[actualEnemy]]*(weightedValue-Q_Matrix[actualEnemy]);

		if(iterations[actualEnemy]!=3){
			iterations[actualEnemy]++;
		}

        //Debug.Log.Log("La matriz ahora es " + Q_Matrix[actualEnemy]);

	}

	public static int makeDecision(int phase, int numberOfClues){

		int probability = 80-numberOfClues*40;

		System.Random rnd = new System.Random ();
		int rand = rnd.Next (1, 100);

		int enemyL = ways [phase - 1, 0];
		int enemyR = ways [phase - 1, 1];

		if (rand <= probability) {
			if(Q_Matrix[enemyL]<Q_Matrix[enemyR]){
				return 2;
			}else{
				return 1;
			}
		} else {
			if(Q_Matrix[enemyL]>Q_Matrix[enemyR]){
				return 2;
			}else{
				return 1;
			}
		}

	}

    public static void loadAI(float Qwolf, float Qbandit, int Iterations0, int Iterations1)
    {
        Q_Matrix[0] = Qwolf;
        Q_Matrix[1] = Qbandit;
        iterations[0] = Iterations0;
        iterations[1] = Iterations1;
    }

 
}
