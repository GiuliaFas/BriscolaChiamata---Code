using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carta 
{
    public string seme;
    public int figura;
    public int valore;
    public float rank;
    public Carta(int num,string seme)//crea una carta
    {
        this.seme = seme;
        figura = num;

        if (num == 1)
            valore = 11;
        else if (num == 3)
            valore = 10;
        else if (num == 10)
            valore = 4;
        else if (num == 9)
            valore = 3;
        else if (num == 8)
            valore = 2;
        else
            valore = 0;

    }

}
