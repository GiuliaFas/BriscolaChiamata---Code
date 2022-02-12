using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;


public class Partita : MonoBehaviour
{
    public static List<GameObject> listaCarte;
    public static List<Giocatore> listagiocatori = new List<Giocatore>();
    public static Carta cartachiamata;
    public static bool chiamato = false;
    public static int puntVittoria;
    public static Carta cartaselezionata;//l'ultima carta giocata
    public static Dictionary<int,Carta> cartegiocate = new Dictionary<int, Carta>();// rendere questa lista una dictionary<int,carta> che associa ad ogni carta la posizione del giocatore che l'ha giocata
    public static List<int> turnoGiocatore = new List<int>(); //lista di posizioni il cui ordine determina l'ordine di gioco
    public static int turnoCorrente = 0; //contatore mano corrente(1-5) indica il numero di giocatori che hanno giocato in questa mano, se == 5 si finisce la mano e si comincia la successiva
    public static int contatoreTurno = 0;//contatore turni(0-7) indica il turno corrente, se == 7 viene chiamata la funzione fine partita
    public static List<Carta> briscoleInGioco = new List<Carta>();

    //classe che gestisce tutta la partita

    public static void AggiungiGiocatori(Giocatore g)//aggiunge i giocatori alla lista giocatori
    {
        listagiocatori.Add(g);
    }
    public static void AggiungiCarteBot(List<Carta> carte)//assegna al bot le carte che ha in mano
    {
        Partita.listagiocatori[4].carteInMano.AddRange(carte);
    }
    public static void AggiornaTurno()//controlla che un turno sia finito o meno e da la possibilità di giocare al giocatore giusto
    {
        bool timerReached = false;
        float timer = 0;
        //se turnogiocatore[i+1] == 5 -->  è il turno del bot ---> chiamare funzione partita.bot.GiocaCarta()
        if (Partita.turnoCorrente == 5)
            Partita.FineMano();

        //se turnogiocatore[i+1]==null --> è finita la mano --> chiamare funzione Partita.FineMano()
        else if (Partita.turnoGiocatore[Partita.turnoCorrente] == 5)
        {
            if (Partita.listagiocatori[4].flagChiamato)
            {
                Partita.listagiocatori[4].AggiornaRankingChiamato();
            }
            else
            {
                Partita.listagiocatori[4].AggiornaRanking();
            }
            Partita.listagiocatori[4].GiocaCarta();
        }
        //se turnogiocatore[i+1]!=5 -->  prossimo giocatore  ---> chiamare funzione GestioneTurno.NuovoTurno()
        else
            GestioneTurno.NuovoTurno();
    }
    public static void FineMano()//assegna carte e punti al vincitore e gestisce la fine della mano, facendo partire le carte versp il vincitore
    {

        
        Carta cartavincente = cartegiocate[turnoGiocatore[0]];
        int posizionevincitore= turnoGiocatore[0];

        var vincitore = VincitoreMano(cartegiocate);
        cartavincente = vincitore.Item1;
        posizionevincitore = vincitore.Item2;
        // assegna carte prese al giocatore vincente della mano
        listagiocatori.Find(x => x.posizione == posizionevincitore).prese.AddRange(cartegiocate.Values);

        if (posizionevincitore == listagiocatori[4].posizione)//nel caso in cui vinca il bot, mostra la carta con la quale ha preso e fa partire l'animazione delle carte
        {
            GameObject.FindGameObjectWithTag("CartaBot").GetComponent<Text>().text = "Il BOT ha preso con la carta: " + cartavincente.figura + " di " + cartavincente.seme;
                List<GameObject> carte = GameObject.FindGameObjectsWithTag("card").ToList<GameObject>();
                foreach (GameObject c in carte)
                {
                    c.AddComponent<Rigidbody2D>();
                    c.GetComponent<Rigidbody2D>().AddForce(new Vector2(100 * (GameObject.FindWithTag("BOT").transform.position.x - c.transform.position.x), 100 * (GameObject.FindWithTag("BOT").transform.position.y - c.transform.position.y)));
                }
        }


        cartegiocate.Clear();//libero la variabile cartegiocate con la fine della mano
        contatoreTurno++;//aumento il numero dei turni giocati

        // riordina la lista delle posizioni di gioco
        List<int> a= new List<int>();
        
        a.AddRange(turnoGiocatore.GetRange(turnoGiocatore.IndexOf(posizionevincitore), turnoGiocatore.Count - turnoGiocatore.IndexOf(posizionevincitore)));
        a.AddRange(turnoGiocatore.GetRange(0, turnoGiocatore.IndexOf(posizionevincitore)));
        turnoGiocatore.Clear();
        turnoGiocatore.AddRange(a);
        turnoCorrente = 0;
        print("Ha preso la mano il giocatore : " + listagiocatori.Find(x => x.posizione == posizionevincitore).nome + " con la carta " + cartavincente.figura + " di " + cartavincente.seme);
        if (contatoreTurno == 8)
        {
            FinePartita();

        }
        else
        {
            if (posizionevincitore == 5)
            {
                if (Partita.listagiocatori[4].flagChiamato)
                {
                    Partita.listagiocatori[4].AggiornaRankingChiamato();
                }
                else
                {
                    Partita.listagiocatori[4].AggiornaRanking();
                }
                Partita.listagiocatori[4].GiocaCarta();
            }
            else
            {
                GameObject.FindWithTag("inserimentocarta").GetComponent<Text>().text = ("Il giocatore " + listagiocatori.Find(x => x.posizione == posizionevincitore).nome + " inserisca la carta da giocare (valore, seme):");            }
        }
        

    }
    public static Tuple<Carta,int> VincitoreMano(Dictionary<int,Carta> cartegiocate)//definisce chi ha vinto il turno
    {
        if (cartegiocate.Count != 0)
        {
            Carta cartavincente = cartegiocate[turnoGiocatore[0]];
            int posizionevincitore = turnoGiocatore[0];
            if (cartegiocate.Count != 1)
            {
                for (int i = 0; i <= Partita.cartegiocate.Count - 1; i++)
                {
                    if (cartavincente.seme == cartegiocate[turnoGiocatore[i]].seme)
                    {
                        if (cartavincente.valore < cartegiocate[turnoGiocatore[i]].valore)
                        {
                            cartavincente = cartegiocate[turnoGiocatore[i]];
                            posizionevincitore = turnoGiocatore[i];
                        }
                        else if (cartavincente.valore == cartegiocate[turnoGiocatore[i]].valore)
                        {
                            if (cartavincente.figura < cartegiocate[turnoGiocatore[i]].figura)
                            {
                                cartavincente = cartegiocate[turnoGiocatore[i]];
                                posizionevincitore = turnoGiocatore[i];
                            }
                        }
                    }
                    else if (cartegiocate[turnoGiocatore[i]].seme == Partita.cartachiamata.seme)
                    {
                        cartavincente = cartegiocate[turnoGiocatore[i]];
                        posizionevincitore = turnoGiocatore[i];
                    }

                }
            }
            return new Tuple<Carta, int>(cartavincente, posizionevincitore);
        }
        else
        {
            return new Tuple<Carta, int>(new Carta(2,"coppe"), 1);
        }
    }
    public static void FinePartita()//carica la scena finale
    {
        SceneManager.LoadScene("FinePartita");
        
    }
}
       

