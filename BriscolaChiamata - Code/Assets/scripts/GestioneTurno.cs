using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class GestioneTurno : MonoBehaviour
{
    

    void Start()//crea i 5 giocatori e gli assegna il nome alla postazione dei giocatori sullo schermo
 {
        Partita.turnoGiocatore.Add(1);
        Partita.turnoGiocatore.Add(2);
        Partita.turnoGiocatore.Add(3);
        Partita.turnoGiocatore.Add(4);
        Partita.turnoGiocatore.Add(5);

        foreach (Giocatore g in Partita.listagiocatori)
        {
            if (g.posizione == 1)
                GameObject.FindWithTag("Gio1").GetComponent<Text>().text = g.nome;
            else if (g.posizione == 2)
                GameObject.FindWithTag("Gio2").GetComponent<Text>().text = g.nome;
            else if (g.posizione == 3)
                GameObject.FindWithTag("Gio3").GetComponent<Text>().text = g.nome;
            else if (g.posizione == 4)
                GameObject.FindWithTag("Gio4").GetComponent<Text>().text = g.nome;
        }
        GameObject.FindWithTag("inserimentocarta").GetComponent<Text>().text = ("Il giocatore " + GameObject.FindWithTag("Gio1").GetComponent<Text>().text + " inserisca la carta da giocare (valore, seme):");
        if (Partita.cartaselezionata != default(Carta))
        {
            GameObject.FindWithTag("figuraCarta").GetComponent<InputField>().text = Partita.cartaselezionata.figura.ToString();
            GameObject.FindWithTag("semeCarta").GetComponent<InputField>().text = Partita.cartaselezionata.seme;
        }

        
    }
    public static void NuovoTurno()//stampa a schermo a chi tocca
    {

        if (Partita.turnoGiocatore[Partita.turnoCorrente]==1)
            GameObject.FindWithTag("inserimentocarta").GetComponent<Text>().text = ("Il giocatore " + GameObject.FindWithTag("Gio1").GetComponent<Text>().text + " inserisca la carta da giocare (valore, seme):");
        if (Partita.turnoGiocatore[Partita.turnoCorrente] == 2)
            GameObject.FindWithTag("inserimentocarta").GetComponent<Text>().text = ("Il giocatore " + GameObject.FindWithTag("Gio2").GetComponent<Text>().text + " inserisca la carta da giocare (valore, seme):");
        if (Partita.turnoGiocatore[Partita.turnoCorrente] == 3)
            GameObject.FindWithTag("inserimentocarta").GetComponent<Text>().text = ("Il giocatore " + GameObject.FindWithTag("Gio3").GetComponent<Text>().text + " inserisca la carta da giocare (valore, seme):");
        if (Partita.turnoGiocatore[Partita.turnoCorrente] == 4)
            GameObject.FindWithTag("inserimentocarta").GetComponent<Text>().text = ("Il giocatore " + GameObject.FindWithTag("Gio4").GetComponent<Text>().text + " inserisca la carta da giocare (valore, seme):");
        if (Partita.turnoGiocatore[Partita.turnoCorrente] == 5)
            GameObject.FindWithTag("inserimentocarta").GetComponent<Text>().text = ("Il bot sta giocando");
    }

    private void Update()//distrugge le carte quando escono dalla visuale del giocatore durante il turno
    {
        List<GameObject> carte = GameObject.FindGameObjectsWithTag("card").ToList<GameObject>();
        if (carte.Count != 0)           
        {
            foreach (GameObject g in carte)
            {
                Vector3 pos = Camera.main.WorldToViewportPoint(g.transform.position);
                if (pos.x < 0 || pos.x > 1 || pos.y < 0 || pos.y > 1)
                    Destroy(g);
            }
        }
    }


}
