using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Karakter : MonoBehaviour
{
    public int id;
    public string ad;
    public Lokasyon l;
    Grid grid;
    public Karakter(int id_, string ad_, Lokasyon l_)
    {
        this.id = id_;
        this.ad = ad_;
        this.l = l_;
    }

    public int getId()
    {
        return id;
    }
    public void setId(int id)
    {
        this.id = id;
    }
    public string getAd()
    {
        return ad;
    }
    public void setAd(string ad)
    {
        this.ad = ad;
    }

    public Lokasyon getLokasyon()
    {
        return l;
    }

    public void setLokasyon(Lokasyon l)
    {
        this.l = l;
    }

    void Start()
    {
        
    }
    // EN KISA YOL
}