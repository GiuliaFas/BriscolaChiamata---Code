using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Giocatore : MonoBehaviour
{
    public string nome;
    public int posizione;
    public bool flagChiamante=false;
    public bool flagChiamato=false;
    public float probabilitaChiamato;
    public List<Carta> carteInMano = new List<Carta>();
    public List<Carta> prese = new List<Carta>();
    public int punti;
    

    public Giocatore (string aNome, int aPosizione)
    {
        nome = aNome;
        posizione = aPosizione;
    }
    public void AggiornaProbabilità() //aggiorna la probabilità che un giocatore sia o meno il compagno
    {
               
        var vincitore= Partita.VincitoreMano(Partita.cartegiocate);        if (Partita.cartaselezionata.seme == Partita.cartachiamata.seme && Partita.turnoGiocatore[Partita.turnoCorrente] == Partita.listagiocatori.Find(x=>x.flagChiamante==true).posizione - 1 && Partita.cartaselezionata.valore >= 2)
        {
            Partita.listagiocatori.Find(x => x.posizione == Partita.turnoGiocatore[Partita.turnoCorrente]).probabilitaChiamato -= 0.3f;
            print("Probabilità che giocatore " + Partita.listagiocatori.Find(x => x.posizione == Partita.turnoGiocatore[Partita.turnoCorrente]).nome + " è il compagno è diminuita di 0.3");
        } //il giocatore gioca una briscola di valore maggiore di 2  ed è subito prima del chiamante: -0.3
        if (Partita.cartaselezionata.seme == Partita.cartachiamata.seme && Partita.turnoGiocatore[Partita.turnoCorrente] == Partita.listagiocatori.Find(x => x.flagChiamante == true).posizione+1 && Partita.cartaselezionata.valore >= 2)
        {
            Partita.listagiocatori.Find(x => x.posizione == Partita.turnoGiocatore[Partita.turnoCorrente]).probabilitaChiamato += 0.1f;
            print("Probabilità che giocatore " + Partita.listagiocatori.Find(x => x.posizione == Partita.turnoGiocatore[Partita.turnoCorrente]).nome + " è il compagno è aumentata di 0.1");
        } //il giocatore gioca una briscola di valore maggiore di 2 quando è subito dopo il chiamante: +0.1
        if (Partita.cartaselezionata.seme == Partita.cartachiamata.seme && Partita.turnoGiocatore[Partita.turnoCorrente] <= Partita.listagiocatori.Find(x => x.flagChiamante == true).posizione && Partita.cartaselezionata.valore <= 2)
        {
            Partita.listagiocatori.Find(x => x.posizione == Partita.turnoGiocatore[Partita.turnoCorrente]).probabilitaChiamato -= 0.2f;
            print("Probabilità che giocatore " + Partita.listagiocatori.Find(x => x.posizione == Partita.turnoGiocatore[Partita.turnoCorrente]).nome + " è il compagno è diminuita di 0.2");
        }//il giocatore gioca una briscola di valore minore di 2 ed è prima del chiamante: -0.2
        if (Partita.cartaselezionata.seme == Partita.cartachiamata.seme && Partita.turnoGiocatore[Partita.turnoCorrente] >= Partita.listagiocatori.Find(x => x.flagChiamante == true).posizione && Partita.cartaselezionata.valore <= 2)
        {
            Partita.listagiocatori.Find(x => x.posizione == Partita.turnoGiocatore[Partita.turnoCorrente]).probabilitaChiamato += 0.2f;
            print("Probabilità che giocatore " + Partita.listagiocatori.Find(x => x.posizione == Partita.turnoGiocatore[Partita.turnoCorrente]).nome + " è il compagno è aumentata di 0.2");
        }//il giocatore gioca una briscola di valore minore di 2 ed è dopo del chiamante: +0.2
        if (Partita.cartaselezionata.seme == Partita.cartachiamata.seme && Partita.turnoGiocatore[Partita.turnoCorrente] >= Partita.listagiocatori.Find(x => x.flagChiamante == true).posizione && Partita.cartegiocate.ContainsKey(Partita.listagiocatori.Find(x => x.flagChiamante == true).posizione) && Partita.cartegiocate[Partita.listagiocatori.Find(x => x.flagChiamante == true).posizione].seme == Partita.cartachiamata.seme && Partita.cartegiocate[Partita.listagiocatori.Find(x => x.flagChiamante == true).posizione].valore<Partita.cartaselezionata.valore)
        {
            Partita.listagiocatori.Find(x => x.posizione == Partita.turnoGiocatore[Partita.turnoCorrente]).probabilitaChiamato -= 0.5f;
        }//giocatore è dopo il chiamante, il chiamante ha giocato una briscola e il giocatore la supera -0.5
        if (Partita.cartaselezionata.valore >= 10 && Partita.turnoGiocatore[Partita.turnoCorrente] == Partita.listagiocatori.Find(x => x.flagChiamante == true).posizione - 1 )
        {
            Partita.listagiocatori.Find(x => x.posizione == Partita.turnoGiocatore[Partita.turnoCorrente]).probabilitaChiamato += 0.3f;
            print("Probabilità che giocatore " + Partita.listagiocatori.Find(x => x.posizione == Partita.turnoGiocatore[Partita.turnoCorrente]).nome + " è il compagno è aumentata di 0.3");
        }// il giocatore gioca un carico ed è prima del chiamante: +0.3
        if (Partita.cartaselezionata.valore >= 10 && Partita.turnoGiocatore[Partita.turnoCorrente] >= Partita.listagiocatori.Find(x => x.flagChiamante == true).posizione && Partita.cartegiocate.ContainsKey(Partita.listagiocatori.Find(x => x.flagChiamante == true).posizione) && Partita.cartegiocate[Partita.listagiocatori.Find(x => x.flagChiamante == true).posizione].seme == Partita.cartachiamata.seme && Partita.cartegiocate[Partita.listagiocatori.Find(x => x.flagChiamante == true).posizione].figura<6 && Partita.turnoGiocatore[Partita.turnoCorrente]!=5)
        {
            Partita.listagiocatori.Find(x => x.posizione == Partita.turnoGiocatore[Partita.turnoCorrente]).probabilitaChiamato -= 0.1f;
            //print("Probabilità che giocatore " + Partita.listagiocatori.Find(x => x.posizione == Partita.turnoGiocatore[Partita.turnoCorrente]).nome + " è il compagno è diminuita di 0.1");
        }  //giocatore gioca un carico, dopo il chiamante,che ha giocato una briscola minore di 6 -0.1
        if (Partita.cartaselezionata.valore >= 10 && Partita.turnoGiocatore[Partita.turnoCorrente] >= Partita.listagiocatori.Find(x => x.flagChiamante == true).posizione && Partita.cartegiocate.ContainsKey(Partita.listagiocatori.Find(x => x.flagChiamante == true).posizione) && Partita.turnoGiocatore[Partita.listagiocatori.Find(x => x.flagChiamante == true).posizione] < Partita.turnoGiocatore[Partita.turnoCorrente] && Partita.cartegiocate[Partita.listagiocatori.Find(x => x.flagChiamante == true).posizione].seme == Partita.cartachiamata.seme && Partita.cartegiocate[Partita.listagiocatori.Find(x => x.flagChiamante == true).posizione].figura > 6 )
        {
            Partita.listagiocatori.Find(x => x.posizione == Partita.turnoGiocatore[Partita.turnoCorrente]).probabilitaChiamato += 0.3f;
            print("Probabilità che giocatore " + Partita.listagiocatori.Find(x => x.posizione == Partita.turnoGiocatore[Partita.turnoCorrente]).nome + " è il compagno è aumentata di 0.3");
        }//giocatore gioca un carico, dopo il chiamante,che ha giocato una briscola maggiore di 6 +0.3
        if (Partita.cartegiocate.Count > 1 && Partita.cartaselezionata.valore > vincitore.Item1.valore && Partita.cartaselezionata.seme == vincitore.Item1.seme && Partita.turnoGiocatore[Partita.turnoCorrente] == Partita.listagiocatori.Find(x => x.flagChiamante == true).posizione + 1)
        {
            Partita.listagiocatori.Find(x => x.posizione == Partita.turnoGiocatore[Partita.turnoCorrente]).probabilitaChiamato += 0.2f;
            print("Probabilità che giocatore " + Partita.listagiocatori.Find(x => x.posizione == Partita.turnoGiocatore[Partita.turnoCorrente]).nome + " è il compagno è aumentata di 0.2");
        } //giocatore supera la carta sul tavolo ed è dopo il chiamante +0.2
        if (Partita.cartegiocate.Count > 1 && Partita.cartaselezionata.valore > vincitore.Item1.valore && Partita.cartaselezionata.seme == vincitore.Item1.seme && Partita.turnoGiocatore[Partita.turnoCorrente] == Partita.listagiocatori.Find(x => x.flagChiamante == true).posizione - 1)
        {
            Partita.listagiocatori.Find(x => x.posizione == Partita.turnoGiocatore[Partita.turnoCorrente]).probabilitaChiamato -= 0.1f;
            print("Probabilità che giocatore " + Partita.listagiocatori.Find(x => x.posizione == Partita.turnoGiocatore[Partita.turnoCorrente]).nome + " è il compagno è diminuita di 0.2");
        }//giocatore supera la carta sul tavolo ed è prima del chiamante -0.1
        if (Partita.turnoGiocatore[Partita.turnoCorrente] == Partita.turnoGiocatore[4] && Partita.cartegiocate.ContainsKey(Partita.listagiocatori.Find(x => x.flagChiamante == true).posizione) && vincitore.Item1==Partita.cartegiocate[Partita.listagiocatori.Find(x => x.flagChiamante == true).posizione] && Partita.cartaselezionata.valore >= 2)
        {
            Partita.listagiocatori.Find(x => x.posizione == Partita.turnoGiocatore[Partita.turnoCorrente]).probabilitaChiamato += 0.2f;
            print("Probabilità che giocatore " + Partita.listagiocatori.Find(x => x.posizione == Partita.turnoGiocatore[Partita.turnoCorrente]).nome + " è il compagno è aumentata di 0.2");
        } //il giocatore è ultimo di mano, sta vincendo il chiamante, il giocatore gioca una carta maggiore di 2 +0.2
        if (Partita.turnoGiocatore[Partita.turnoCorrente] == Partita.turnoGiocatore[4] && Partita.cartegiocate.ContainsKey(Partita.listagiocatori.Find(x => x.flagChiamante == true).posizione) && vincitore.Item1 == Partita.cartegiocate[Partita.listagiocatori.Find(x => x.flagChiamante == true).posizione] && Partita.cartaselezionata.valore >= 10)
        {
            Partita.listagiocatori.Find(x => x.posizione == Partita.turnoGiocatore[Partita.turnoCorrente]).probabilitaChiamato += 0.5f;
            print("Probabilità che giocatore " + Partita.listagiocatori.Find(x => x.posizione == Partita.turnoGiocatore[Partita.turnoCorrente]).nome + " è il compagno è aumentata di 0.5");
        } //il giocatore è ultimo di mano, sta vincendo il chiamante, il giocatore gioca una carta maggiore di 10 +0.5



    }
    public void AggiornaRanking() //aggiorna il ranking delle carte da giocare, alla fine di ogni turno, quando il bot NON è il compagno Aggio
    {
        var vincitore = Partita.VincitoreMano(Partita.cartegiocate);

        int somma =0;
        if (Partita.cartegiocate.Count!=0 && vincitore.Item1.seme == Partita.cartachiamata.seme)//si assicura di non mettere una briscola se in campo ce ne è già una di valore superiore
        {
            foreach (Carta b in Partita.listagiocatori[4].carteInMano)
            {
                if (b.valore < vincitore.Item1.valore && b.seme == Partita.cartachiamata.seme)
                    b.rank=-0.5f;

            }
        }
        for(int k=0;k<= Partita.cartegiocate.Count-1;k++)
        {
            somma += Partita.cartegiocate[Partita.turnoGiocatore[k]].valore;
        }
        //1  BRISCOLE ANCORA IN GIOCO
        if (Partita.briscoleInGioco.Count >= 8 - Partita.contatoreTurno)
        {
            foreach(Carta c in Partita.listagiocatori[4].carteInMano)
            {
                if (c.seme == Partita.cartachiamata.seme)
                {
                    int k = 0;
                    foreach(Carta b in Partita.briscoleInGioco)
                    {
                        if (b.valore > c.valore)
                            k++;

                    }
                    if (k > 8 - Partita.contatoreTurno)
                        c.rank += 0.1f;
                }
                
            }
        }
        //2 POSIZIONE DEL CHIAMANTE RISPETTO A ME E POSIZIONE ASSOLUTA
        int i = 0;//NUMERO DI CARTE GIOCABILI AL TERMINE DELL'ALBERO DI IF; SE RIMANE UGUALE A ZERO GIOCO LA CARTA NON BRISCOLA CON VALORE PIU' BASSO

        if (Partita.listagiocatori.Find(x => x.flagChiamante == true).posizione == Partita.turnoGiocatore[4])//CHIAMANTE ULTIMO
        {
            if (Partita.listagiocatori[4].posizione == Partita.turnoGiocatore[0])//BOT PRIMO
            {
                foreach (Carta g in Partita.listagiocatori[4].carteInMano)
                {
                    if (g.valore < 2 && g.seme != Partita.cartachiamata.seme) 
                    {//GIOCO UN LISCIO
                        g.rank += 0.05f;
                        i++;
                    }
                }
            }else if (Partita.listagiocatori[4].posizione == Partita.turnoGiocatore[3])//BOT PENULTIMO
            {
                
                if (vincitore.Item1.seme==Partita.cartachiamata.seme && vincitore.Item1.valore < 2)//SE C'E' UNA BRISCOLA < DEL FANTE 
                {
                    if (somma >= 10)
                    {
                        foreach(Carta g in Partita.listagiocatori[4].carteInMano)
                        {
                            if (g.seme == Partita.cartachiamata.seme && g.valore < 3 && g.valore > vincitore.Item1.valore)
                            {//SUPERO AL MASSIMO CON IL FANTE
                                g.rank += 0.05f;
                                i++;
                            }
                        }
                    }
                    else
                    {
                        foreach (Carta g in Partita.listagiocatori[4].carteInMano)
                        {
                            if (g.seme != Partita.cartachiamata.seme && g.valore >= 4)
                            {//CARICO
                                g.rank += 0.05f;
                                i++;
                            }
                        }
                    }
                }
                if (vincitore.Item1.seme != Partita.cartachiamata.seme)//SE NON CI SONO BRISCOLE
                {
                    if (somma < 5)//SE CI SONO MENO DI 5 PUNTI
                    {
                        foreach (Carta g in Partita.listagiocatori[4].carteInMano)
                        {
                            if (Partita.contatoreTurno < 4)
                            {
                                if (g.seme == vincitore.Item1.seme && g.valore > vincitore.Item1.valore && g.valore >= 10)//SUPERO IN SEME CON CARICO
                                {
                                    g.rank += 0.05f;
                                    i++;
                                }
                            }
                            else
                            {
                                if (g.seme == vincitore.Item1.seme && g.valore > vincitore.Item1.valore && g.valore < 10)//SUPERO IN SEME CON FIGURA
                                {
                                    g.rank += 0.05f;
                                    i++;
                                }
                            }
                        }
                    }
                    else//SE CI SONO PIU DI 5 PUNTI
                    {
                        foreach (Carta g in Partita.listagiocatori[4].carteInMano)
                        {
                            if (g.seme == Partita.cartachiamata.seme && g.valore<2)//GIOCO UN BRISCOLINO                                                                   
                            {
                                g.rank += 0.05f;
                                i++;
                            }
                        }
                    }
                }

            }
            else//BOT IN MEZZO
            {
                foreach (Carta g in Partita.listagiocatori[4].carteInMano)
                {
                    if (g.valore < 2)//LISCIO O BRISCOLINA
                    {
                        g.rank += 0.05f;
                        i++;
                    }
                }
            }
        
        }else if(Partita.listagiocatori.Find(x => x.flagChiamante == true).posizione == Partita.turnoGiocatore[0])//CHIAMANTE PRIMO
        {
            if (Partita.listagiocatori[4].posizione == Partita.turnoGiocatore[1])//BOT SECONDO
            {
                if (vincitore.Item1.seme != Partita.cartachiamata.seme)//SE CHIAMANTE NON HA MESSO BRISCOLE METTO CARICO
                {
                    foreach (Carta g in Partita.listagiocatori[4].carteInMano)
                    {
                        if (g.valore >= 10 && g.seme != Partita.cartachiamata.seme)
                        {
                            g.rank += 0.05f;
                            i++;
                        }
                    }
                }
                else//SE CHIAMANTE HA MESSO BRISCOLE METTO SCARTINE/PUNTINI
                {
                    if (vincitore.Item1.valore == 0)
                    {
                        foreach (Carta g in Partita.listagiocatori[4].carteInMano)
                        {
                            if (g.valore < 4 && g.seme != Partita.cartachiamata.seme)
                            {
                                g.rank += 0.05f;
                                i++;
                            }
                        }
                    }
                }
            }
            else if (Partita.listagiocatori[4].posizione == Partita.turnoGiocatore[4])//BOT ULTIMO
            {
                if (vincitore.Item1.seme == Partita.cartachiamata.seme)//SE STA VINCENDO UNA BRISCOLA
                {
                    if (!Partita.listagiocatori.Find(x => x.posizione == vincitore.Item2).flagChiamante && Partita.contatoreTurno>2 && TrovaCompagno(Partita.listagiocatori.Find(x=>x.posizione==vincitore.Item2))==1)
                    {
                        foreach (Carta g in Partita.listagiocatori[4].carteInMano)
                        {
                            if (g.valore > 4 && g.seme != Partita.cartachiamata.seme)
                            {//CARICO
                                g.rank += 0.1f;
                                i++;
                            }
                        }
                    }
                    
                    foreach (Carta g in Partita.listagiocatori[4].carteInMano)
                    {
                        if (g.valore > vincitore.Item1.valore && g.seme == Partita.cartachiamata.seme && g.valore < 3)
                        {//SUPERO CON UNA BRISCOLA < 9
                            g.rank += 0.05f;
                            i++;
                        }
                    }
                }
                else//SE NON CI SONO BRISCOLE
                {
                    foreach (Carta g in Partita.listagiocatori[4].carteInMano)
                    {
                        if (g.valore == 10 && g.seme == vincitore.Item1.seme && g.valore > vincitore.Item1.valore)
                        {//SUPERO IN SEME CON CARTA ALTA O METTO BRISCOLINA
                            g.rank += 4.0f;
                            i++;
                        }
                        if (g.valore > 10 && g.seme == vincitore.Item1.seme && g.valore > vincitore.Item1.valore)
                        {//SUPERO IN SEME CON CARTA ALTA O METTO BRISCOLINA
                            g.rank += 5.0f;
                            i++;
                        }
                        if (g.seme == Partita.cartachiamata.seme && g.valore <= 2)
                        {
                            g.rank += 0.1f;
                            i++;
                        }
                    }
                }
            }
            else//BOT IN MEZZO
            {
                foreach (Carta g in Partita.listagiocatori[4].carteInMano)
                {
                    if (g.valore < 2 && g.seme != Partita.cartachiamata.seme)
                    {
                        g.rank += 0.05f;
                        i++;
                    }
                }
            }
        }
        else//CHIAMANTE IN MEZZO
        {
            if (Partita.listagiocatori[4].posizione==Partita.turnoGiocatore[0])//BOT PRIMO
            {
                int j = 0;
                foreach (Carta g in Partita.listagiocatori[4].carteInMano)
                {
                    if (g.valore >= 10 && g.seme != Partita.cartachiamata.seme)
                        j++;
                }
                if (j >= 2)
                {
                    foreach (Carta g in Partita.listagiocatori[4].carteInMano)
                    {
                        if (g.valore >= 10 && g.seme != Partita.cartachiamata.seme)//SE HO PIU DI 2 CARICHI LO METTO
                        {
                            g.rank += 0.05f;
                            j++;
                            i++;
                        }
                    }
                }
                else
                {
                    foreach (Carta g in Partita.listagiocatori[4].carteInMano)
                    {
                        if (g.valore >= 2 && g.valore < 10 && g.seme != Partita.cartachiamata.seme)// SE NO PUNTICINI
                        {
                            g.rank += 0.05f;
                            i++;
                        }
                    }
                }
            }
            else if (Partita.listagiocatori[4].posizione == Partita.turnoGiocatore[4])//BOT ULTIMO
            {
                if (vincitore.Item1.seme != Partita.cartachiamata.seme)//SE NON VINCE UNA BRISCOLA
                {
                    foreach (Carta g in Partita.listagiocatori[4].carteInMano)
                    {
                        if (g.valore == 10 && g.seme == vincitore.Item1.seme && g.valore > vincitore.Item1.valore)
                        {//SUPERO IN SEME CON CARTA ALTA O METTO BRISCOLINA
                            g.rank += 4.0f;
                            i++;
                        }
                        if (g.valore > 10 && g.seme == vincitore.Item1.seme && g.valore > vincitore.Item1.valore)
                        {//SUPERO IN SEME CON CARTA ALTA O METTO BRISCOLINA
                            g.rank += 5.0f;
                            i++;
                        }
                        if (g.seme == Partita.cartachiamata.seme && g.valore <= 2)
                        {
                            g.rank += 0.1f;
                            i++;
                        }
                    }
                }
                else//SE VINCE UNA BRISCOLA
                {
                    if (!Partita.listagiocatori.Find(x => x.posizione == vincitore.Item2).flagChiamante && Partita.contatoreTurno > 2 && TrovaCompagno(Partita.listagiocatori.Find(x => x.posizione == vincitore.Item2)) == 1)//SE STA VINCENDO UN GIOCATORE POCO PROBABILE CHE SIA IL COMPAGNO
                    {
                        foreach (Carta g in Partita.listagiocatori[4].carteInMano)
                        {
                            if (g.seme != Partita.cartachiamata.seme && g.figura >= 10)
                            {
                                g.rank += 0.1f;
                                i++;
                            }
                        }
                    }
                       
                    if (somma >= 10)//SE CI SONO PIU DI 10 PUNTI 
                    {
                        if (vincitore.Item1.valore == 0)//SE VINCE UN LISCIO
                        {
                            foreach (Carta g in Partita.listagiocatori[4].carteInMano)
                            {
                                if (g.seme == vincitore.Item1.seme && g.figura > vincitore.Item1.figura && g.valore <2)//SUPERO CON LISCIO
                                {
                                    g.rank += 0.05f;
                                    i++;
                                }
                            }
                        }
                        else
                        {
                            foreach (Carta g in Partita.listagiocatori[4].carteInMano)
                            {
                                if (g.seme == vincitore.Item1.seme && g.valore > vincitore.Item1.valore && g.valore <= 3)//SUPERO AL MASSIMO CON CAVALLO
                                {
                                    g.rank += 0.05f;
                                    i++;
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (Carta g in Partita.listagiocatori[4].carteInMano)
                        {
                            if (g.seme != Partita.cartachiamata.seme && g.valore <2)//LISCIO
                            {
                                g.rank += 0.05f;
                                i++;
                            }
                        }
                    }
                }
            }
            else//BOT IN MEZZO
            {
                if (Partita.turnoGiocatore.Find(x=>x == 5)<Partita.turnoGiocatore.Find(x => x == Partita.listagiocatori.Find(x => x.flagChiamante == true).posizione))//BOT PRIMA DEL CHIAMANTE
                {
                    if (vincitore.Item1.seme != Partita.cartachiamata.seme)//SE NON VINCE UNA BRISCOLA
                    {
                        foreach (Carta g in Partita.listagiocatori[4].carteInMano)
                        {
                            if (g.valore > vincitore.Item1.valore && g.seme == vincitore.Item1.seme)//SUPERO IN SEME
                            {
                                g.rank += 0.05f;
                                i++;
                            }
                        }
                        foreach (Carta g in Partita.listagiocatori[4].carteInMano)
                        {
                            if (g.seme == Partita.cartachiamata.seme && g.valore < 2)//METTO BRISCOLINA
                            {
                                g.rank += 0.05f;
                                i++;
                            }
                        }
                    }
                    else//SE VINCE BRISCOLA
                    {
                        if (somma >= 10)//SE CI SONO PIU DI 10 PUNTI 
                        {
                            foreach (Carta g in Partita.listagiocatori[4].carteInMano)
                            {
                                if (g.valore > vincitore.Item1.valore && g.seme == vincitore.Item1.seme && g.figura > vincitore.Item1.figura && g.valore <10)//SUPERO
                                {
                                    g.rank += 0.05f;
                                    i++;
                                }
                            }
                        }
                    }
                }
                else//BOT DOPO IL CHIAMANTE
                {
                    if (Partita.cartegiocate.ContainsKey(Partita.listagiocatori.Find(x => x.flagChiamante == true).posizione) && vincitore.Item1 == Partita.cartegiocate[Partita.listagiocatori.Find(x => x.flagChiamante == true).posizione])// SE STA PRENDENDO IL CHIAMANTE
                    {
                        foreach (Carta g in Partita.listagiocatori[4].carteInMano)
                        {
                            if (g.valore == 0 && g.seme != Partita.cartachiamata.seme)//LISCIO
                            {
                                g.rank += 0.05f;
                                i++;
                            }
                        }
                    }
                    else
                    {
                        foreach (Carta g in Partita.listagiocatori[4].carteInMano)
                        {
                            if (g.valore >= 3 && g.seme != Partita.cartachiamata.seme)// METTO PUNTI 
                            {
                                g.rank += 0.05f;
                                i++;
                            }
                        }
                    }

                }
            }         
        }

        if (i==0)
            foreach(Carta c in carteInMano)
            {
                if (c.seme != Partita.cartachiamata.seme)
                    c.rank += 0.05f;
            }
    }
    public void AggiornaRankingChiamato()//aggiorna il ranking delle carte da giocare, alla fine di ogni turno, quando il bot è il compagno
    {
        int somma = 0;
        for (int k = 0; k <= Partita.cartegiocate.Count - 1; k++)//somma valore carte in tavola
        {
            somma += Partita.cartegiocate[Partita.turnoGiocatore[k]].valore;
        }
        var vincitore = Partita.VincitoreMano(Partita.cartegiocate);
        int i = 0;//NUMERO DI CARTE GIOCABILI AL TERMINE DELL'ALBERO DI IF; SE RIMANE UGUALE A ZERO GIOCO LA CARTA NON BRISCOLA CON VALORE PIU' BASSO
        if (Partita.cartegiocate.Count != 0 && vincitore.Item1.seme == Partita.cartachiamata.seme)//si assicura di non mettere una briscola se in campo ce ne è già una di valore superiore
        {
            foreach (Carta b in Partita.listagiocatori[4].carteInMano)
            {
                if (b.valore < vincitore.Item1.valore && b.seme == Partita.cartachiamata.seme)
                    b.rank = -0.5f;

            }
        }
        for (int k = 0; k <= Partita.cartegiocate.Count - 1; k++)
        {
            somma += Partita.cartegiocate[Partita.turnoGiocatore[k]].valore;
        }
        //1  BRISCOLE ANCORA IN GIOCO
        if (Partita.briscoleInGioco.Count >= 8 - Partita.contatoreTurno)
        {
            foreach (Carta c in Partita.listagiocatori[4].carteInMano)
            {
                if (c.seme == Partita.cartachiamata.seme)
                {
                    int k = 0;
                    foreach (Carta b in Partita.briscoleInGioco)
                    {
                        if (b.valore > c.valore)
                            k++;

                    }
                    if (k > 8 - Partita.contatoreTurno)
                        c.rank += 0.1f;
                }

            }
        }

        if (Partita.contatoreTurno <= 3)
        {
            if (Partita.listagiocatori[4].posizione == Partita.turnoGiocatore[4])//SE BOT ULTIMO
            {
                if (somma >= 10&&somma<16)
                {
                    foreach(Carta c in carteInMano)
                    {
                        if (c != Partita.cartachiamata && c.seme == vincitore.Item1.seme && c.valore < 2 && c.valore>=vincitore.Item1.valore && c.figura > vincitore.Item1.figura)
                        {
                            c.rank += 0.05f;
                            i++;
                        }
                    }
                }else if (somma >= 16)
                {
                    foreach (Carta c in carteInMano)
                    {
                        if (c != Partita.cartachiamata && c.seme == vincitore.Item1.seme && c.valore < 10 && c.valore >= vincitore.Item1.valore && c.figura > vincitore.Item1.figura)
                        {
                            c.rank += 0.05f;
                            i++;
                        }
                    }
                }
                else
                {
                    foreach (Carta c in carteInMano)
                    {
                        if (c.seme!=Partita.cartachiamata.seme)
                        {
                            c.rank += 0.05f;
                            i++;
                        }
                    }
                }
            }   
        }
        else//TURNO > 3
        {

            if (Partita.listagiocatori.Find(x => x.flagChiamante == true).posizione == Partita.turnoGiocatore[4])//CHIAMANTE ULTIMO
            {
                if (Partita.listagiocatori[4].posizione == Partita.turnoGiocatore[0])//BOT PRIMO
                {
                    foreach (Carta g in Partita.listagiocatori[4].carteInMano)
                    {
                        if (g.valore > 2 && g.seme != Partita.cartachiamata.seme)
                        {//GIOCO UN CARICO
                            g.rank += 0.05f;
                            i++;
                        }
                        if (g.valore < 2 && g.seme == Partita.cartachiamata.seme)
                        {//GIOCO UNA BRISCOLINA
                            g.rank += 0.05f;
                            i++;
                        }
                    }
                }
                else if (Partita.listagiocatori[4].posizione == Partita.turnoGiocatore[3])//BOT PENULTIMO
                {

                    if (vincitore.Item1.seme == Partita.cartachiamata.seme && vincitore.Item1.valore < 2)//SE C'E' UNA BRISCOLA < DEL FANTE 
                    {
                        if (somma >= 10 && Partita.contatoreTurno>5) //SE SOMMA MAGGIORE DI 10 E TURNO MAGGIORE DI 5, CARICO
                        {
                            foreach (Carta g in Partita.listagiocatori[4].carteInMano)
                            {
                                if (g.seme != Partita.cartachiamata.seme && g.valore > 3)
                                {//CARICO
                                    g.rank += 0.05f;
                                    i++;
                                }
                            }
                        }
                        else
                        {
                            foreach (Carta g in Partita.listagiocatori[4].carteInMano)
                            {
                                if (g.seme != Partita.cartachiamata.seme && g.valore < 2)
                                {//LISCIO
                                    g.rank += 0.05f;
                                    i++;
                                }
                            }
                        }
                    }
                    if (vincitore.Item1.seme != Partita.cartachiamata.seme)//SE NON CI SONO BRISCOLE
                    {
                        foreach (Carta g in Partita.listagiocatori[4].carteInMano)
                        {
                            if (g.seme == vincitore.Item1.seme && somma + g.valore >= 14)
                            {//SUPERO SE IN TAVOLO CI SONO PIU' DI 14 PUNTI
                                g.rank += 0.05f;
                                i++;
                            }else if(g.seme == vincitore.Item1.seme && somma + g.valore < 14)
                            {
                                g.rank += 0.05f;
                                i++;
                            }
                        }
                    }

                }
                else//BOT IN MEZZO
                {
                    if (Partita.contatoreTurno < 5)
                    {
                        foreach (Carta g in Partita.listagiocatori[4].carteInMano)
                        {
                            if (g.valore <= 2)
                            {
                                g.rank += 0.05f; //METTO SCARTINA
                                i++;
                            }
                        }
                    }
                    else
                    {
                        foreach (Carta g in Partita.listagiocatori[4].carteInMano)
                        {
                            if (g.valore > 3 && g.seme != Partita.cartachiamata.seme) //METTO CARICO
                            {
                                g.rank += 0.05f;
                                i++;
                            }
                        }
                    }
                }

            }
            else if (Partita.listagiocatori.Find(x => x.flagChiamante == true).posizione == Partita.turnoGiocatore[0])//CHIAMANTE PRIMO
            {
                if (Partita.listagiocatori[4].posizione == Partita.turnoGiocatore[1])//BOT SECONDO
                {
                    if (vincitore.Item1.seme != Partita.cartachiamata.seme)//SE CHIAMANTE NON HA MESSO BRISCOLE LA METTO IO
                    {
                        foreach (Carta g in Partita.listagiocatori[4].carteInMano)
                        {
                            if (g.valore < 3 && g.seme == Partita.cartachiamata.seme)
                            {
                                g.rank += 0.05f;
                                i++;
                            }
                        }
                    }
                    else//SE CHIAMANTE HA MESSO BRISCOLE METTO SCARTINE/PUNTINI
                    {
                      
                            foreach (Carta g in Partita.listagiocatori[4].carteInMano)
                            {
                                if (g.valore < 3 && g.seme != Partita.cartachiamata.seme)
                                {
                                    g.rank += 0.05f;
                                    i++;
                                }
                            }
                        
                    }
                }
                else if (Partita.listagiocatori[4].posizione == Partita.turnoGiocatore[4])//BOT ULTIMO
                {
                    if (vincitore.Item1.seme == Partita.cartachiamata.seme)//SE STA VINCENDO UNA BRISCOLA
                    {
                        
                        if(vincitore.Item2 == Partita.listagiocatori.Find(x => x.flagChiamante == true).posizione)//SE STA VINCENDO IL CHIAMANTE
                        {
                            foreach (Carta g in Partita.listagiocatori[4].carteInMano)
                            {
                                if (g.valore >= 10 && g.seme != Partita.cartachiamata.seme)//CARICO
                                {
                                    g.rank += 0.05f;
                                    i++;
                                }
                            }
                        }                        
                        else//SE NON VINCE IL CHIAMANTE
                        {
                            if (somma < 15)//SE CI SONO MENO DI 15 PUNTI IN CAMPO
                            {
                                foreach (Carta g in Partita.listagiocatori[4].carteInMano)
                                {
                                    if (g.valore < 2 && g.seme != Partita.cartachiamata.seme)//LISCIO
                                    {
                                        g.rank += 0.05f;
                                        i++;
                                    }
                                }
                            }
                            else
                            {
                                foreach (Carta g in Partita.listagiocatori[4].carteInMano)
                                {
                                    if (g.valore > vincitore.Item1.valore && g.figura> vincitore.Item1.figura)//SUPERO
                                    {
                                        g.rank += 0.05f;
                                        i++;
                                    }
                                }
                            }
                        }
                    }
                    else//SE NON CI SONO BRISCOLE
                    {
                        foreach (Carta g in Partita.listagiocatori[4].carteInMano)
                        {
                            if (g.valore >= 10 && g.seme == vincitore.Item1.seme)
                            {//SUPERO IN SEME CON CARTA ALTA
                                g.rank += 5.0f;
                                i++;
                            }
                            if (g.seme == Partita.cartachiamata.seme && g.valore < 2 && somma>=13)
                            {
                                g.rank += 0.1f;
                                i++;
                            }
                        }
                    }
                }
                else//BOT IN MEZZO
                {
                    if(vincitore.Item2 == Partita.listagiocatori.Find(x => x.flagChiamante == true).posizione)//SE PRENDE IL CHIAMANTE
                    {
                        if (vincitore.Item1.seme == Partita.cartachiamata.seme)//CON BRISCOLA
                        {
                            foreach (Carta g in Partita.listagiocatori[4].carteInMano)
                            {
                                if (g.valore >= 2 && g.valore < 10 && g.seme!=Partita.cartachiamata.seme)//METTO PUNTI
                                {
                                    g.rank += 0.05f;
                                    i++;
                                }
                            }
                        }
                        else//SENZA BRISCOLA
                        {
                            foreach (Carta g in Partita.listagiocatori[4].carteInMano)
                            {
                                if (g.valore < 4 &&g.seme == Partita.cartachiamata.seme)//METTO BRISCOLA
                                {
                                    g.rank += 0.05f;
                                    i++;
                                }
                            }
                        }
                    }
                }
            }
            else//CHIAMANTE IN MEZZO
            {
                if (Partita.listagiocatori[4].posizione == Partita.turnoGiocatore[0])//BOT PRIMO
                {
                    foreach (Carta g in Partita.listagiocatori[4].carteInMano)
                    {
                        if (g.valore < 2 )//METTO BRISCOLINA O LISCIO
                        {
                            g.rank += 0.05f;
                            i++;
                        }
                    }
                }
                else if (Partita.listagiocatori[4].posizione == Partita.turnoGiocatore[4])//BOT ULTIMO
                {
                    if (vincitore.Item1.seme != Partita.cartachiamata.seme)//SE NON STA VINCENDO UNA BRISCOLA
                    {
                        foreach (Carta g in Partita.listagiocatori[4].carteInMano)
                        {
                            if (g.valore > vincitore.Item1.valore && g.seme == vincitore.Item1.seme && g.seme != Partita.cartachiamata.seme)
                            {
                                g.rank += 0.2f;
                                i++;
                            }
                            if (g.valore <= 2 && g.seme == Partita.cartachiamata.seme)
                            {
                                g.rank += 0.05f;
                                i++;
                            }
                        }
                    }
                    else//SE VINCE UNA BRISCOLA
                    {
                         if (vincitore.Item2 == Partita.listagiocatori.Find(x => x.flagChiamante == true).posizione)//SE STA PRENDENDO IL CHIAMANTE
                         {
                            foreach (Carta g in Partita.listagiocatori[4].carteInMano)
                            {
                                if (g.valore >= 10)//CARICO
                                {
                                    g.rank += 0.05f;
                                    i++;
                                }
                            }
                        }
                        else//SE NON PRENDE IL CHIAMANTE
                        {
                            if (somma > 10)//SE CI SONO PIU DI 10 PUNTI
                            {
                                foreach (Carta g in Partita.listagiocatori[4].carteInMano)
                                {
                                    if (g.valore <= 3 && g.seme == Partita.cartachiamata.seme)//PRENDO CON BRISCOLA SOTTO IL CAVALLO
                                    {
                                        g.rank += 0.05f;
                                        i++;
                                    }
                                }
                            }
                            else//SE CI SONO MENO DI 10 PUNTI
                            {
                                foreach (Carta g in Partita.listagiocatori[4].carteInMano)
                                {
                                    if (g.valore < 2 && g.seme != Partita.cartachiamata.seme)//LISCIO
                                    {
                                        g.rank += 0.05f;
                                        i++;
                                    }
                                }
                            } 
                        }
                    }
                }
                else//BOT IN MEZZO
                {
                    if (Partita.turnoGiocatore.Find(x => x == 5) < Partita.turnoGiocatore.Find(x => x == Partita.listagiocatori.Find(x => x.flagChiamante == true).posizione))//BOT PRIMA DEL CHIAMANTE
                    {
                        foreach (Carta g in Partita.listagiocatori[4].carteInMano)
                        {
                            if (g.valore < 2 && g.seme != Partita.cartachiamata.seme)//LISCIO
                            {
                                g.rank += 0.05f;
                                i++;
                            }
                        }
                    }
                    else//BOT DOPO IL CHIAMANTE
                    {
                        if (Partita.cartegiocate.ContainsKey(Partita.listagiocatori.Find(x => x.flagChiamante == true).posizione) && vincitore.Item1 == Partita.cartegiocate[Partita.listagiocatori.Find(x => x.flagChiamante == true).posizione])//SE PRENDE IL CHIAMANTE
                        {
                            if (vincitore.Item1.seme == Partita.cartachiamata.seme)//SE PRENDE CON BRISCOLA
                            {
                                foreach (Carta g in Partita.listagiocatori[4].carteInMano)
                                {
                                    if (g.valore < 4 && g.seme != Partita.cartachiamata.seme)//METTO PUNTICINI O LISCIO
                                    {
                                        g.rank += 0.05f;
                                        i++;
                                    }
                                }
                            }
                            else
                            {
                                foreach (Carta g in Partita.listagiocatori[4].carteInMano)
                                {
                                    if (g.valore < 4 && g.seme == Partita.cartachiamata.seme)//METTO BRISCOLA
                                    {
                                        g.rank += 0.05f;
                                        i++;
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (Carta g in Partita.listagiocatori[4].carteInMano)
                            {
                                if (g.valore < 4 && g.seme == Partita.cartachiamata.seme)//METTO BRISCOLA
                                {
                                    g.rank += 0.05f;
                                    i++;
                                }
                            }
                        }

                    }
                }
            }



        }
       
    }
    public int TrovaCompagno(Giocatore gioc) // ordina i giocatori da quello con più probabilità a quello con meno di essere il compagno
    {
        int posi = 1;
        foreach (Giocatore g in Partita.listagiocatori)
        {
            
            if (!(g.flagChiamante&&gioc.posizione==g.posizione&&g.posizione==5))
            {
                if (gioc.probabilitaChiamato > g.probabilitaChiamato)
                {
                    posi++;
                }
            }
        }
        return posi;
    }
    public void GiocaCarta() //fa giocare la carta al bot
    {

        if (Partita.turnoGiocatore[0]==5)
        {
            
            List<GameObject> carte = GameObject.FindGameObjectsWithTag("card").ToList<GameObject>();
            foreach (GameObject c in carte)
            {
                c.AddComponent<Rigidbody2D>();
                c.GetComponent<Rigidbody2D>().AddForce(new Vector2(100 * (GameObject.FindWithTag("Gio1").transform.position.x - c.transform.position.x), 100 * (GameObject.FindWithTag("Gio1").transform.position.y - c.transform.position.y)));
            }
        }
               

        List<Carta> carteDaGiocare = new List<Carta>();
        Carta carta=carteInMano[0];

        //TRA LE CARTE IN MANO GIOCA QUELLA CON IL RANK PIU' ALTO
        float max = 0;
        foreach(Carta g in carteInMano)
        {
            if (g.rank > max)
            {
                max = g.rank;
            }
        }
        foreach (Carta g in carteInMano)
        {
            if (g.rank >= max)
            {
                carteDaGiocare.Add(g);
            }
        }

        ////TRA LE CARTE IN CARTEDAGIOCARE GIOCA QUELLA CON IL VALORE PIU' BASSO
        float min = 1000;
        
        foreach (Carta g in carteDaGiocare)
        {
            if (g.valore < min)
            {
                min = g.valore;
            }
        }
        for (int i = carteDaGiocare.Count-1; i >= 0; i--)
        {
            if (carteDaGiocare[i].valore > min)
                carteDaGiocare.RemoveAt(i);
        }


        ////TRA LE CARTE IN CARTEDAGIOCARE GIOCA QUELLA CON IL NUMERO PIU' BASSO
        min = 100;
        foreach (Carta g in carteDaGiocare)
        {
            if (g.figura < min)
            {
                min = g.figura;
            }
        }
        foreach (Carta g in carteDaGiocare)
        {
            if (g.figura == min)
            {
                carta = g;
            }
        }



        carteInMano.Remove(carta);
        Partita.cartaselezionata = carta;

        
        Vector3 pos = new Vector3(0, 0, 0);
        Vector3 offset = new Vector3(0, 2.5f,-1);
        GameObject o = GameObject.Instantiate(new GameObject(), pos + offset, Quaternion.identity);
        o.gameObject.tag = "card";
        o.AddComponent<SpriteRenderer>();
        o.GetComponent<SpriteRenderer>().sprite = InserimentoCarta.TrovaCarta(Partita.cartaselezionata);
        o.GetComponent<Transform>().localScale = new Vector2(1.6f, 1.6f);

        print("ho giocato la carta: " + carta.figura + " di " + carta.seme);
        foreach(Carta c in carteInMano)
        {
            c.rank = 0;
        }
        Partita.cartegiocate.Add(Partita.turnoGiocatore[Partita.turnoCorrente], Partita.cartaselezionata);
        Partita.turnoCorrente++;
        Partita.AggiornaTurno();

    }

}


