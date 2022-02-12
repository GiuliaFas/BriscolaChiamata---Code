using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConfermaAsta : MonoBehaviour
{
   
    public void Conferma()//trova il giocatore che ha chiamato e gli mette il flagChiamante=TRUE, controlla che le informazioni dell'asta e dei nomi dei giocatori siano corrette
                          {

        bool ok1 =true; // variabili booleane, se tutte rimangono true allora le informazioni inserite sono corrette
        bool ok2 = true;
        bool ok3 = true;


        ok1 = int.TryParse(GameObject.FindWithTag("punteggiochiamato").GetComponent<InputField>().text, out _);//controlla che nel campo punteggio ci siano solo numeri;
        if (ok1)
        {
            Partita.puntVittoria = System.Int16.Parse(GameObject.FindWithTag("punteggiochiamato").GetComponent<InputField>().text);
            if (Partita.puntVittoria < 61 || Partita.puntVittoria > 120)//controlla se il valore nel punteggio chiamato è corretto
                ok1 = false;
        }

        if (GameObject.FindWithTag("nomeChiamante").GetComponent<InputField>().text != Partita.listagiocatori[0].nome && GameObject.FindWithTag("nomeChiamante").GetComponent<InputField>().text != Partita.listagiocatori[1].nome && GameObject.FindWithTag("nomeChiamante").GetComponent<InputField>().text != Partita.listagiocatori[2].nome && GameObject.FindWithTag("nomeChiamante").GetComponent<InputField>().text != Partita.listagiocatori[3].nome)// controlla che il nome del chiamante esista
            ok3 = false;

        foreach (Giocatore x in Partita.listagiocatori)// controlla che il giocatore chiamato sia effettivamente uno dei giocatori
        {
            if (x.nome == GameObject.FindWithTag("nomeChiamante").GetComponent<InputField>().text)
            {
                x.flagChiamante = true;
                
            }
        }
        ok2 = int.TryParse(GameObject.FindWithTag("figurachiamata").GetComponent<InputField>().text, out _);//controlla che nel campo punteggio ci siano solo numeri
        if (ok2)
        {
            Partita.cartachiamata = new Carta(System.Int16.Parse(GameObject.FindWithTag("figurachiamata").GetComponent<InputField>().text), GameObject.FindWithTag("semechiamato").GetComponent<Dropdown>().options[GameObject.FindWithTag("semechiamato").GetComponent<Dropdown>().value].text);
            if (Partita.cartachiamata.figura < 1 || Partita.cartachiamata.figura > 10)//controlla che la figura chiamata esista
                ok2 = false;
        }

        if (ok1 && ok2 && ok3)//se tutte le operazioni sono andate a buon fine, salva gli input
        {
            foreach (Giocatore g in Partita.listagiocatori)
                if (g.flagChiamante)
                    for (int i = 1; i < 11; i++)
                        {
                            Partita.briscoleInGioco.Add(new Carta(i, Partita.cartachiamata.seme));
                        }


            foreach (Giocatore g in Partita.listagiocatori)
            {
                foreach (Carta x in g.carteInMano)
                {
                    if (x.figura == Partita.cartachiamata.figura && x.seme == Partita.cartachiamata.seme)
                    {
                        g.flagChiamato = true;
                    }

                }
            }

            SceneManager.LoadScene("TurnoGioco");
        }
        else
        {
            GameObject.FindWithTag("ErroreAsta").GetComponent<Text>().text = "Informazioni errate, inserirle nuovamente";
            print("informazioni inserite non valide, inserirle di nuovo");
            GameObject.FindWithTag("figurachiamata").GetComponent<InputField>().text = null;
            GameObject.FindWithTag("punteggiochiamato").GetComponent<InputField>().text = null;
            GameObject.FindWithTag("nomeChiamante").GetComponent<InputField>().text = null;
        }
    }
}
