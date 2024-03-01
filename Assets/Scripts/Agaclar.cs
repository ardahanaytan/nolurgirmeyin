using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agaclar : Engel
{
    public override int GetGenislik()
    {
        return genislik;
    }
    public override int GetYukseklik()
    {
        return yukseklik;
    }
    public override int GetId()
    {
        return id;
    }

    public override void SetGenislik(int genislik)
    {
        this.genislik = genislik;
    }
    public override void SetYukseklik(int yukseklik)
    {
        this.yukseklik = yukseklik;
    }

    public override void SetId(int id)
    {
        this.id = id;
    }

    public Agaclar(int baslangic_x, int baslangic_y, int yukseklik, int genislik, int id) : base(baslangic_x, baslangic_y, yukseklik, genislik, id)
    {

    }


}
