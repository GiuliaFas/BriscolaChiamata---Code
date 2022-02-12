using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickCarta8: MonoBehaviour
{
    //si attiva quando un giocatore mette le sue 8 carta e le clicca 
    public List<Carta> CarteSottrazione = new List<Carta>(); 

    public void Start()//cambia scena tra un giocatore che deve scegliere le 8 carte senza distruggerle
    {
        if (Partita.cartachiamata != default(Carta))
            SceneManager.UnloadSceneAsync("TurnoGioco");
        Partita.listaCarte = UnityEngine.Object.FindObjectsOfType<GameObject>().ToList<GameObject>();
        for (int i = 0; i < Partita.listaCarte.Count; i++)
            if (Partita.listaCarte[i].tag == "event" || Partita.listaCarte[i].tag == "MainCamera" || Partita.listaCarte[i].tag == "Player" || Partita.listaCarte[i].tag == "Bottone")
                Partita.listaCarte.Remove(Partita.listaCarte[i]);
        if (Partita.cartachiamata != default(Carta))
            GameObject.FindWithTag("Player").GetComponent<Text>().text = "Il giocatore " + Partita.listagiocatori.Find(x => x.posizione == Partita.turnoGiocatore[Partita.turnoCorrente]).nome + " inserisca le sue carte ";
        else
            GameObject.FindWithTag("Player").GetComponent<Text>().text = "Il giocatore " + Partita.listagiocatori[0].nome + " inserisca le sue carte ";
    }

    public void ConfermaCarte()//controlla che le carte selezionate siano corrette con una serie di check, assegna le carte al bot e carica la scena dell'asta
    {
        int c = 0;
        bool ok = true;
        if (Partita.cartachiamata==default(Carta)) {
            foreach (GameObject g in Partita.listaCarte)
            {
                if (g.GetComponent<SpriteRenderer>().color == new Color(1f, 1f, 1f, 0.4f))
                {
                    g.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                    c++;
                }
            }
            if (c != 8)
            {
                print("non hai selezionato 8 carte");
                ok = false;
                for (int j = 0; j < c; j++)
                {
                    CarteSottrazione.RemoveAt(CarteSottrazione.Count - 1);
                }
            }
            else
            {
                if (CarteSottrazione.GroupBy(x => new { x.figura, x.seme })
                   .Where(x => x.Skip(1).Any()).Any())
                {
                    print("carta selezionata non valida");
                    ok = false;
                    for (int j = 0; j < 8; j++)
                    {
                        
                        CarteSottrazione.RemoveAt(CarteSottrazione.Count - 1);
                    }

                    
                }
            }


            if (ok) {
                

                if (CarteSottrazione.Count == 8)
                    GameObject.FindWithTag("Player").GetComponent<Text>().text = "Il giocatore " + Partita.listagiocatori[1].nome + " inserisca le sue carte";
                else if (CarteSottrazione.Count == 16)
                    GameObject.FindWithTag("Player").GetComponent<Text>().text = "Il giocatore " + Partita.listagiocatori[2].nome + " inserisca le sue carte";
                else if (CarteSottrazione.Count == 24)
                    GameObject.FindWithTag("Player").GetComponent<Text>().text = "Il giocatore " + Partita.listagiocatori[3].nome + " inserisca le sue carte";
                else if (CarteSottrazione.Count == 32)
                {
                    List<Carta> Mazzo = new List<Carta>();

                    for (int j = 1; j < 11; j++)
                        Mazzo.Add(new Carta(j, "bastoni"));

                    for (int j = 1; j < 11; j++)
                        Mazzo.Add(new Carta(j, "ori"));

                    for (int j = 1; j < 11; j++)
                        Mazzo.Add(new Carta(j, "spade"));

                    for (int j = 1; j < 11; j++)
                        Mazzo.Add(new Carta(j, "coppe"));


                    for (int j = Mazzo.Count - 1; j >= 0; j--)
                    {
                        for (int i = 0; i < CarteSottrazione.Count; i++)
                        {
                            if (Mazzo[j].figura == CarteSottrazione[i].figura && Mazzo[j].seme == CarteSottrazione[i].seme)
                            {
                                Mazzo.Remove(Mazzo[j]);
                                break;

                            }
                        }
                    }
                    Mazzo.TrimExcess();

                    Partita.AggiungiCarteBot(Mazzo);

                    Partita.listaCarte.Clear();
                    CarteSottrazione.Clear();
                    SceneManager.LoadScene("Asta");
                }
                

                
            }
        }
        

    }

    void Update()//click delle carte nella scena di selezione
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.transform.gameObject.GetComponent<SpriteRenderer>().color == new Color(1, 1, 1, 1))
                {
                    hit.transform.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.4f);
                    string carta = hit.transform.gameObject.tag;
                    string[] valori=carta.Split(' ');
                    int figura =int.Parse(valori[0]);
                    string seme = valori[1];
                    CarteSottrazione.Add(new Carta(figura, seme));
                }
                else
                {
                    hit.transform.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                    string carta = hit.transform.gameObject.tag;
                    string[] valori = carta.Split(' ');
                    int figura = int.Parse(valori[0]);
                    string seme = valori[1];                  
                    CarteSottrazione.Remove(CarteSottrazione.Find(x => x.figura == figura && x.seme == seme));
                }
            }
        }
    }

}
