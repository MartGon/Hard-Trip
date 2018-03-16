using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LanguageController : MonoBehaviour
{
    public enum Language
    {
        LANGUAGE_SPANISH,
        LANGUAGE_ENGLISH,
        LANGUAGE_GALICIAN
    }

    public struct introStruct
    {
        public int id;
        public Language lenguaje;
        public List<String> texto;
    }

    public struct textStruct
    {
        public int id;
        public Language lenguaje;
        public String texto;
    }

    public static List<introStruct> introList = new List<introStruct>();
    public static List<textStruct> titleList = new List<textStruct>();
    public static List<textStruct> textoList = new List<textStruct>();

    public void Awake()
    {
        // Creamos las intros para el primer nivel

        introStruct introStruct = new introStruct();

        // Creamos la intro en Spanish
        introStruct.id = 1;
        introStruct.texto = new List<string>(new string[] {"Princesa, hemos llegado al desfiladero de brenner.\nDicen que en este lugar ya han perecido muchas otras expediciones.\nPreparaos para lo peor mi señora.",
                                    "No tengo miedo alguno. Se que vos me protegeréis. Muchos otros me han guardado, pero nunca tan bien como vos.",
                                    "Roguémosle al Señor, y avancemos. ¿Qué camino prefiere vuestra merced?",
                                "Creo que el mejor camino para seguir será el izquierdo. ¿Que opinais vos?"});
        introStruct.lenguaje = Language.LANGUAGE_SPANISH;
        introList.Add(introStruct);

        //Creamos la intro en Inglés
        introStruct = new introStruct();

        introStruct.id = 1;
        introStruct.texto = new List<string>(new string[] {"<Inglés>Princesa, hemos llegado al desfiladero de brenner.\nDicen que en este lugar ya han perecido muchas otras expediciones.\nPreparaos para lo peor mi señora.",
                                    "No tengo miedo alguno. Se que vos me protegeréis. Muchos otros me han guardado, pero nunca tan bien como vos.",
                                    "Roguémosle al Señor, y avancemos. ¿Qué camino prefiere vuestra merced?",
                                "Creo que el mejor camino para seguir será el izquierdo. ¿Que opinais vos?</Inglés>"});
        introStruct.lenguaje = Language.LANGUAGE_ENGLISH;
        introList.Add(introStruct);

        // Creamos los string con los textos normales

        //      Primero los titulos de los diálogos
        textStruct titleStruct = new textStruct();

        // Pista
        titleStruct.id = 1;
        titleStruct.lenguaje = Language.LANGUAGE_SPANISH;
        titleStruct.texto = "PISTA";
        titleList.Add(titleStruct);

        titleStruct = new textStruct();
        titleStruct.id = 1;
        titleStruct.lenguaje = Language.LANGUAGE_ENGLISH;
        titleStruct.texto = "CLUE";
        titleList.Add(titleStruct);

        // Princesa
        titleStruct = new textStruct();
        titleStruct.id = 2;
        titleStruct.lenguaje = Language.LANGUAGE_SPANISH;
        titleStruct.texto = "PRINCESA";
        titleList.Add(titleStruct);

        titleStruct = new textStruct();
        titleStruct.id = 2;
        titleStruct.lenguaje = Language.LANGUAGE_ENGLISH;
        titleStruct.texto = "PRINCESS";
        titleList.Add(titleStruct);

        // Yo
        titleStruct = new textStruct();
        titleStruct.id = 3;
        titleStruct.lenguaje = Language.LANGUAGE_SPANISH;
        titleStruct.texto = "YO";
        titleList.Add(titleStruct);

        titleStruct = new textStruct();
        titleStruct.id = 3;
        titleStruct.lenguaje = Language.LANGUAGE_ENGLISH;
        titleStruct.texto = "ME";
        titleList.Add(titleStruct);

        // Menu
        titleStruct = new textStruct();
        titleStruct.id = 4;
        titleStruct.lenguaje = Language.LANGUAGE_SPANISH;
        titleStruct.texto = "MENÚ";
        titleList.Add(titleStruct);

        titleStruct = new textStruct();
        titleStruct.id = 4;
        titleStruct.lenguaje = Language.LANGUAGE_ENGLISH;
        titleStruct.texto = "MENU";
        titleList.Add(titleStruct);

        // Fin de juego
        titleStruct = new textStruct();
        titleStruct.id = 5;
        titleStruct.lenguaje = Language.LANGUAGE_SPANISH;
        titleStruct.texto = "FIN DE JUEGO";
        titleList.Add(titleStruct);

        titleStruct = new textStruct();
        titleStruct.id = 5;
        titleStruct.lenguaje = Language.LANGUAGE_ENGLISH;
        titleStruct.texto = "THE END";
        titleList.Add(titleStruct);

        // Verdadero fin de juego
        titleStruct = new textStruct();
        titleStruct.id = 6;
        titleStruct.lenguaje = Language.LANGUAGE_SPANISH;
        titleStruct.texto = "VERDADERO FIN DE JUEGO";
        titleList.Add(titleStruct);

        titleStruct = new textStruct();
        titleStruct.id = 6;
        titleStruct.lenguaje = Language.LANGUAGE_ENGLISH;
        titleStruct.texto = "THE TRUE END";
        titleList.Add(titleStruct);

        // Boss Name
        titleStruct = new textStruct();
        titleStruct.id = 7;
        titleStruct.lenguaje = Language.LANGUAGE_SPANISH;
        titleStruct.texto = "Gran Lobo Espadachín";
        titleList.Add(titleStruct);

        titleStruct = new textStruct();
        titleStruct.id = 7;
        titleStruct.lenguaje = Language.LANGUAGE_ENGLISH;
        titleStruct.texto = "Great Wolf SwordMaster";
        titleList.Add(titleStruct);

        // Ahora los propios textos
        textStruct textoStruct = new textStruct();

        // Texto de ganar
        textoStruct.id = 1;
        textoStruct.lenguaje = Language.LANGUAGE_SPANISH;
        textoStruct.texto = "¡Enhorabuena! Has superado el nivel. Puedes seguir explorando el mapa si quieres. ¿Ir al siguiente nivel?";
        textoList.Add(textoStruct);

        textoStruct = new textStruct();
        textoStruct.id = 1;
        textoStruct.lenguaje = Language.LANGUAGE_ENGLISH;
        textoStruct.texto = "Congrats! You have beaten the level. You can keep exploring the map if you like. Go to the next level?";
        textoList.Add(textoStruct);

        // Texto verdadero de ganar
        textoStruct.id = 2;
        textoStruct.lenguaje = Language.LANGUAGE_SPANISH;
        textoStruct.texto = "¡Enhorabuena! Has superado el verdadero desafío.Ya no tenemos nada más que ofrecerte ¿Ir al siguiente nivel?";
        textoList.Add(textoStruct);

        textoStruct = new textStruct();
        textoStruct.id = 2;
        textoStruct.lenguaje = Language.LANGUAGE_ENGLISH;
        textoStruct.texto = "Congrats! You have beaten the true challenge. We do not have any more to offer. Go to the next level?";
        textoList.Add(textoStruct);

        // texto que sale al pulsar esc
        textoStruct.id = 3;
        textoStruct.lenguaje = Language.LANGUAGE_SPANISH;
        textoStruct.texto = "¿Volver al menú principal?";
        textoList.Add(textoStruct);

        textoStruct = new textStruct();
        textoStruct.id = 3;
        textoStruct.lenguaje = Language.LANGUAGE_ENGLISH;
        textoStruct.texto = "Go back to the main menu?";
        textoList.Add(textoStruct);

        // Princesa elige el camino el camino de la izquierda
        textoStruct.id = 4;
        textoStruct.lenguaje = Language.LANGUAGE_SPANISH;
        textoStruct.texto = "Creo que la mejor opción será elegir el camino de la izquierda";
        textoList.Add(textoStruct);

        textoStruct = new textStruct();
        textoStruct.id = 4;
        textoStruct.lenguaje = Language.LANGUAGE_ENGLISH;
        textoStruct.texto = "I think the best path would be the one of the left-side";
        textoList.Add(textoStruct);

        // Princesa elige el camino el camino de la derecha
        textoStruct.id = 5;
        textoStruct.lenguaje = Language.LANGUAGE_SPANISH;
        textoStruct.texto = "Creo que la mejor opción será elegir el camino de la derecha";
        textoList.Add(textoStruct);

        textoStruct = new textStruct();
        textoStruct.id = 5;
        textoStruct.lenguaje = Language.LANGUAGE_ENGLISH;
        textoStruct.texto = "I think the best path would be the one of the right-side";
        textoList.Add(textoStruct);

        // Princesa elige el camino el camino de la derecha
        textoStruct.id = 6;
        textoStruct.lenguaje = Language.LANGUAGE_SPANISH;
        textoStruct.texto = "Mode díficil activado. Espero que sepas lo que haces";
        textoList.Add(textoStruct);

        textoStruct = new textStruct();
        textoStruct.id = 6;
        textoStruct.lenguaje = Language.LANGUAGE_ENGLISH;
        textoStruct.texto = "Hard Mode On. I hope you know what you are doing.";
        textoList.Add(textoStruct);
    }

    public static String getTitleById(int id)
    {
        foreach(textStruct title in titleList)
        {
            if (title.id == id && ConfigController.lenguaje == title.lenguaje)
                return title.texto;
        }

        return "String not found";
    }

    public static String getTextById(int id)
    {
        foreach (textStruct text in textoList)
        {
            if (text.id == id && ConfigController.lenguaje == text.lenguaje)
                return text.texto;
        }

        return "String not found";
    }

    public static List<String> getIntroById(int id)
    {
        foreach (introStruct intro in introList)
        {
            //Debug.Log("El id es " + id + " el idioma es " + intro.lenguaje);
            if (intro.id == id && ConfigController.lenguaje == intro.lenguaje)
                return intro.texto;
        }

        return new List<string>(new string[] { "No", "se", "encontro", "la string" });
    }

    public static String getMenuOptionById(int id)
    {
        switch(id)
        {
            case 0:

                if (ConfigController.lenguaje == Language.LANGUAGE_ENGLISH)
                    return "No";
                else
                    return "No";
            case 1:

                if (ConfigController.lenguaje == Language.LANGUAGE_ENGLISH)
                    return "Yes";
                else
                    return "Sí";
            default:

                return "Not found";
        }
    }
}

