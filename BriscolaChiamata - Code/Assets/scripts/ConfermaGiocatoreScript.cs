using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConfermaGiocatoreScript : MonoBehaviour
{
   public void CheckGiocatore() //creazione dei giocatori e caricamento scena Scelta della carta
    {
        

            Partita.AggiungiGiocatori(new Giocatore(GameObject.FindWithTag("Gio1").GetComponent<InputField>().text, 1));
            Partita.AggiungiGiocatori(new Giocatore(GameObject.FindWithTag("Gio2").GetComponent<InputField>().text, 2));
            Partita.AggiungiGiocatori(new Giocatore(GameObject.FindWithTag("Gio3").GetComponent<InputField>().text, 3));
            Partita.AggiungiGiocatori(new Giocatore(GameObject.FindWithTag("Gio4").GetComponent<InputField>().text, 4));
            Partita.AggiungiGiocatori(new Giocatore("bot", 5));
            SceneManager.LoadScene("ScegliCarta");
        

         
    }

}
