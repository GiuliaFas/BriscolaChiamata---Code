using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class InserimentoCarta : MonoBehaviour
{
    private static AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    public static void InserisciCarta()//permette di inserire le carte da tastiera durante il gioco e le stampa sul tavolo, aggiunge la carta alla mano di chi l'ha giocata e fa partire Aggiornaturnogiocatore
    {
        GameObject.FindGameObjectWithTag("CartaBot").GetComponent<Text>().text = "";
        if (Partita.turnoGiocatore[Partita.turnoCorrente] == Partita.turnoGiocatore[0])//fa partire le carte verso il vincitore
        {
            if (Partita.turnoGiocatore[0] == 1)
            {
                List<GameObject> carte = GameObject.FindGameObjectsWithTag("card").ToList<GameObject>();
                foreach (GameObject c in carte)
                {
                    c.AddComponent<Rigidbody2D>();
                    c.GetComponent<Rigidbody2D>().AddForce(new Vector2(100 * (GameObject.FindWithTag("Gio1").transform.position.x - c.transform.position.x), 100 * (GameObject.FindWithTag("Gio1").transform.position.y - c.transform.position.y)));
                }
            }
            else if (Partita.turnoGiocatore[0] == 2)
            {
                List<GameObject> carte = GameObject.FindGameObjectsWithTag("card").ToList<GameObject>();
                foreach (GameObject c in carte)
                {
                    c.AddComponent<Rigidbody2D>();
                    c.GetComponent<Rigidbody2D>().AddForce(new Vector2(100 * (GameObject.FindWithTag("Gio2").transform.position.x - c.transform.position.x), 100 * (GameObject.FindWithTag("Gio2").transform.position.y - c.transform.position.y)));
                }
            }
            else if(Partita.turnoGiocatore[0] == 3)
            {
                List<GameObject> carte = GameObject.FindGameObjectsWithTag("card").ToList<GameObject>();
                foreach (GameObject c in carte)
                {
                    c.AddComponent<Rigidbody2D>();
                    c.GetComponent<Rigidbody2D>().AddForce(new Vector2(100 * (GameObject.FindWithTag("Gio3").transform.position.x - c.transform.position.x), 100 * (GameObject.FindWithTag("Gio3").transform.position.y - c.transform.position.y)));
                }
            }
            else if (Partita.turnoGiocatore[0] == 4)
            {
                List<GameObject> carte = GameObject.FindGameObjectsWithTag("card").ToList<GameObject>();
                foreach (GameObject c in carte)
                {
                    c.AddComponent<Rigidbody2D>();
                    c.GetComponent<Rigidbody2D>().AddForce(new Vector2(100 * (GameObject.FindWithTag("Gio4").transform.position.x - c.transform.position.x), 100 * (GameObject.FindWithTag("Gio4").transform.position.y - c.transform.position.y)));
                }
            }
        }
    

        if (System.Int16.TryParse(GameObject.FindWithTag("figuraCarta").GetComponent<InputField>().text, out short num) && System.Int16.Parse(GameObject.FindWithTag("figuraCarta").GetComponent<InputField>().text) >= 1 && System.Int16.Parse(GameObject.FindWithTag("figuraCarta").GetComponent<InputField>().text) <= 10)//?
        {
            Partita.cartaselezionata = new Carta(System.Int16.Parse(GameObject.FindWithTag("figuraCarta").GetComponent<InputField>().text), GameObject.FindWithTag("semeCarta").GetComponent<Dropdown>().options[GameObject.FindWithTag("semeCarta").GetComponent<Dropdown>().value].text);

            if (Partita.cartaselezionata == Partita.cartachiamata)
            {
                Partita.chiamato = true;
                Partita.listagiocatori[Partita.turnoGiocatore[Partita.turnoCorrente]].flagChiamato = true;
                foreach (Giocatore g in Partita.listagiocatori)
                {
                    if (!g.flagChiamato)
                        g.probabilitaChiamato = 0;
                }
            }
            //aggiungere la carta giocata a cartegiocate con la posizione del giocatore data da Partita.turnoGiocatore[Partita.turnoCorrente]
            Partita.cartegiocate.Add(Partita.turnoGiocatore[Partita.turnoCorrente], Partita.cartaselezionata);

            GameObject.FindWithTag("figuraCarta").GetComponent<InputField>().text = null;


            print("Il giocatore " + Partita.listagiocatori.Find(x => x.posizione == Partita.turnoGiocatore[Partita.turnoCorrente]).nome + " ha giocato la carta: " + Partita.cartaselezionata.figura + " di " + Partita.cartaselezionata.seme);

            //stampare carta sul tavolo

            if (Partita.listagiocatori.Find(x => x.posizione == Partita.turnoGiocatore[Partita.turnoCorrente]).nome == Partita.listagiocatori[0].nome)
            {


                Vector3 pos = new Vector3(0, 0, 0);
                Vector3 offset = new Vector3(3, 1.5f, -1);
                GameObject g = GameObject.Instantiate(new GameObject(), pos + offset, Quaternion.identity);
                g.gameObject.tag = "card";
                g.AddComponent<SpriteRenderer>();
                g.GetComponent<SpriteRenderer>().sprite = TrovaCarta(Partita.cartaselezionata);
                g.GetComponent<Transform>().localScale = new Vector2(1.6f, 1.6f);
                source.Play();
            }
            else if (Partita.listagiocatori.Find(x => x.posizione == Partita.turnoGiocatore[Partita.turnoCorrente]).nome == Partita.listagiocatori[1].nome)
            {

                Vector3 pos = new Vector3(0, 0, 0);
                Vector3 offset = new Vector3(3, -2, -1);
                GameObject g = GameObject.Instantiate(new GameObject(), pos + offset, Quaternion.identity);
                g.gameObject.tag = "card";
                g.AddComponent<SpriteRenderer>();
                g.GetComponent<SpriteRenderer>().sprite = TrovaCarta(Partita.cartaselezionata);
                g.GetComponent<Transform>().localScale = new Vector2(1.6f, 1.6f);
                source.Play();
            }
            else if (Partita.listagiocatori.Find(x => x.posizione == Partita.turnoGiocatore[Partita.turnoCorrente]).nome == Partita.listagiocatori[2].nome)
            {

                Vector3 pos = new Vector3(0, 0, 0);
                Vector3 offset = new Vector3(-3, -2, -1);
                GameObject g = GameObject.Instantiate(new GameObject(), pos + offset, Quaternion.identity);
                g.gameObject.tag = "card";
                g.AddComponent<SpriteRenderer>();
                g.GetComponent<SpriteRenderer>().sprite = TrovaCarta(Partita.cartaselezionata);
                g.GetComponent<Transform>().localScale = new Vector2(1.6f, 1.6f);
                source.Play();
            }
            else if (Partita.listagiocatori.Find(x => x.posizione == Partita.turnoGiocatore[Partita.turnoCorrente]).nome == Partita.listagiocatori[3].nome)
            {

                Vector3 pos = new Vector3(0, 0, 0);
                Vector3 offset = new Vector3(-3, 1.5f, -1);
                GameObject g = GameObject.Instantiate(new GameObject(), pos + offset, Quaternion.identity);
                g.gameObject.tag = "card";
                g.AddComponent<SpriteRenderer>();
                g.GetComponent<SpriteRenderer>().sprite = TrovaCarta(Partita.cartaselezionata);
                g.GetComponent<Transform>().localScale = new Vector2(1.6f, 1.6f);
                source.Play();
            }
            else
            {

            }

            if (Partita.cartaselezionata.seme == Partita.cartachiamata.seme)
                Partita.briscoleInGioco.Remove(Partita.cartaselezionata);

            if (!Partita.chiamato && !Partita.listagiocatori.Find(x => x.posizione == Partita.turnoGiocatore[Partita.turnoCorrente]).flagChiamante && !Partita.listagiocatori[4].flagChiamato)
            {
                Partita.listagiocatori[4].AggiornaProbabilità();
            }




            //aggiunge carta giocata alla mano di chi l'ha giocata
            if (Partita.turnoGiocatore[Partita.turnoCorrente] != 5)
                Partita.listagiocatori.Find(x => x.posizione == Partita.turnoGiocatore[Partita.turnoCorrente]).carteInMano.Add(Partita.cartaselezionata);

            //aggiorna contatore posizione
            Partita.turnoCorrente++;

            Partita.AggiornaTurno();//aggiornare turno del giocatore
        }
    }

    public static UnityEngine.Sprite TrovaCarta(Carta c )//data una carta in input ti restituisce il suo sprite
    {
        switch (c.seme)
        {
            case "bastoni":
                switch (c.figura)
                {
                    case 1:
                        return Resources.Load<Sprite>("01_basto");
                        break;
                    case 2:
                        return Resources.Load<Sprite>("02_basto");
                        break;
                    case 3:
                        return Resources.Load<Sprite>("03_basto");
                        break;
                    case 4:
                        return Resources.Load<Sprite>("04_basto");
                        break;
                    case 5:
                        return Resources.Load<Sprite>("05_basto");
                        break;
                    case 6:
                        return Resources.Load<Sprite>("06_basto");
                        break;
                    case 7:
                        return Resources.Load<Sprite>("07_basto");
                        break;
                    case 8:
                        return Resources.Load<Sprite>("08_basto");
                        break;
                    case 9:
                        return Resources.Load<Sprite>("09_basto");
                        break;
                    case 10:
                        return Resources.Load<Sprite>("10_basto");
                        break;
                    default:
                        return null;
                        break;
                }
                break;
            case "spade":
                switch (c.figura)
                {
                    case 1:
                        return Resources.Load<Sprite>("01_spade");
                        break;
                    case 2:
                        return Resources.Load<Sprite>("02_spade");
                        break;
                    case 3:
                        return Resources.Load<Sprite>("03_spade");
                        break;
                    case 4:
                        return Resources.Load<Sprite>("04_spade");
                        break;
                    case 5:
                        return Resources.Load<Sprite>("05_spade");
                        break;
                    case 6:
                        return Resources.Load<Sprite>("06_spade");
                        break;
                    case 7:
                        return Resources.Load<Sprite>("07_spade");
                        break;
                    case 8:
                        return Resources.Load<Sprite>("08_spade");
                        break;
                    case 9:
                        return Resources.Load<Sprite>("09_spade");
                        break;
                    case 10:
                        return Resources.Load<Sprite>("10_spade");
                        break;
                    default:
                        return null;
                        break;
                }
                break;
            case "ori":
                switch (c.figura)
                {
                    case 1:
                        return Resources.Load<Sprite>("01_denar");
                        break;
                    case 2:
                        return Resources.Load<Sprite>("02_denar");
                        break;
                    case 3:
                        return Resources.Load<Sprite>("03_denar");
                        break;
                    case 4:
                        return Resources.Load<Sprite>("04_denar");
                        break;
                    case 5:
                        return Resources.Load<Sprite>("05_denar");
                        break;
                    case 6:
                        return Resources.Load<Sprite>("06_denar");
                        break;
                    case 7:
                        return Resources.Load<Sprite>("07_denar");
                        break;
                    case 8:
                        return Resources.Load<Sprite>("08_denar");
                        break;
                    case 9:
                        return Resources.Load<Sprite>("09_denar");
                        break;
                    case 10:
                        return Resources.Load<Sprite>("10_denar");
                        break;
                    default:
                        return null;
                        break;
                }
                break;
            case "coppe":
                switch (c.figura)
                {
                    case 1:
                        return Resources.Load<Sprite>("01_coppe");
                        break;
                    case 2:
                        return Resources.Load<Sprite>("02_coppe");
                        break;
                    case 3:
                        return Resources.Load<Sprite>("03_coppe");
                        break;
                    case 4:
                        return Resources.Load<Sprite>("04_coppe");
                        break;
                    case 5:
                        return Resources.Load<Sprite>("05_coppe");
                        break;
                    case 6:
                        return Resources.Load<Sprite>("06_coppe");
                        break;
                    case 7:
                        return Resources.Load<Sprite>("07_coppe");
                        break;
                    case 8:
                        return Resources.Load<Sprite>("08_coppe");
                        break;
                    case 9:
                        return Resources.Load<Sprite>("09_coppe");
                        break;
                    case 10:
                        return Resources.Load<Sprite>("10_coppe");
                        break;
                    default:
                        return null;
                        break;
                }
                break;
            default:
                return null;
                break;
        }

        
    }
}
