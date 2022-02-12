using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using UnityEditor;

public class FinePartita : MonoBehaviour
{
    private static AudioSource source;
    // Start is called before the first frame update
    void Start()//stampa i vincitori e i punti alla fine della partita
    {

        foreach (Giocatore g in Partita.listagiocatori)
        {
            foreach (Carta c in g.prese)
            {
                g.punti = g.punti + c.valore;
            }
        }
        List<Giocatore> squadra1 = new List<Giocatore>();
        List<Giocatore> squadra2 = new List<Giocatore>();
        foreach (Giocatore g in Partita.listagiocatori)
        {
            if (g.flagChiamato == true || g.flagChiamante == true)
                squadra1.Add(g);
            else 
                squadra2.Add(g);
        }
        int puntis1 = 0;
        int puntis2 = 0;//inizializza variabili che calcolano il punteggio
        foreach (Giocatore g in squadra1)
            puntis1 += g.punti;
        foreach (Giocatore g in squadra2)
            puntis2 += g.punti;

        if (puntis1 >= Partita.puntVittoria)//stampa i nomi dei vincitori
        {
            GameObject.FindWithTag("NomeVincitore").GetComponent<Text>().text = "Squadra chiamante";
            GameObject.FindWithTag("PuntiVincitore").GetComponent<Text>().text = puntis1.ToString();
        }
        else
        {
            GameObject.FindWithTag("NomeVincitore").GetComponent<Text>().text = "Squadra avversaria";
            GameObject.FindWithTag("PuntiVincitore").GetComponent<Text>().text = puntis2.ToString();
        }

        
        
    }
    private void Update()//grafica di fine partita
    {
        if (Input.GetKey("mouse 0"))
        {

            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 2.0f;
            GameObject g = GameObject.Instantiate(new GameObject(), pos, Quaternion.identity);
            g.AddComponent<SpriteRenderer>();
            g.GetComponent<SpriteRenderer>().sprite = InserimentoCarta.TrovaCarta(Partita.cartaselezionata);
            g.AddComponent<Rigidbody2D>();
            g.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1000));
            source.PlayOneShot(GetComponent<AudioSource>().clip);
        }
    }


}
