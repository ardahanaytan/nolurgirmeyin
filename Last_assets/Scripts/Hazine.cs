using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazine : MonoBehaviour
{
    public string tur;
    public int yukseklik;
    public int genislik;
    public int baslangic_x;
    public int baslangic_y;
    public bool silindi_mi;
    public int id;
    public GameObject hazine;

    public string getTur()
    {
        return tur;
    }

    public void setTur(string tur)
    {
        this.tur = tur;
    }

    public int getYukseklik()
    {
        return yukseklik;
    }

    public void setYukseklik(int yukseklik)
    {
        this.yukseklik = yukseklik;
    }

    public int getGenislik()
    {
        return genislik;
    }

    public void setGenislik(int genislik)
    {
        this.genislik = genislik;
    }

    public int getBaslangic_x()
    {
        return baslangic_x;
    }

    public void setBaslangic_x(int baslangic_x)
    {
        this.baslangic_x = baslangic_x;
    }

    public int getBaslangic_y()
    {
        return baslangic_y;
    }

    public void setBaslangic_y(int baslangic_y)
    {
        this.baslangic_y = baslangic_y;
    }

    public bool getSilindi_mi()
    {
        return silindi_mi;
    }

    public void setSilindi_mi(bool silindi_mi)
    {
        this.silindi_mi = silindi_mi;
    }

    public int getId()
    {
        return id;
    }

    public void setId(int id)
    {
        this.id = id;
    }

    public GameObject getHazine()
    {
        return hazine;
    }

    public void setHazine(GameObject hazine)
    {
        this.hazine = hazine;
    }



    public Hazine(int baslangic_x, int baslangic_y, int yukseklik, int genislik, string tur, int id)
    {
        this.baslangic_x = baslangic_x;
        this.baslangic_y = baslangic_y;
        this.yukseklik = yukseklik;
        this.genislik = genislik;
        this.tur = tur;
        this.id = id;
        this.silindi_mi = false;
    }

    public bool IsColliding(Vector3 position, int objectSizeY, int objectSizeX, int kontrol_uzunlugu, bool tek_mi, int int_boyut)
    {
        GridArray grid = new GridArray();
        // Verilen pozisyonun grid içinde olup olmadığını kontrol et
        if (position.x < -int_boyut/2+kontrol_uzunlugu || position.x >= int_boyut/2-kontrol_uzunlugu || position.y < -int_boyut/2+kontrol_uzunlugu || position.y >= int_boyut/2-kontrol_uzunlugu)
        {
            return true; // Pozisyon grid dışında, çakışma var
        }

        //orta kisim kontrolü
        /*
        Debug.Log("*position.x" + position.x + " " + position.x + objectSizeX);
        if(position.x-kontrol_uzunlugu < 0 && position.x + kontrol_uzunlugu > 0)
        {
            return true;
        }
        */


        // Nesne boyutu kadar bir kare alana denk gelen grid hücrelerini kontrol et
        //tek- cift konrtolü lazim

        //negatifi ayarlamak icin
        position.x += int_boyut / 2;
        position.y += int_boyut / 2;


        //tam ortadan bastırıyoruz - bu sol altı bulup oradan ilerlemek icin- sol alt 0,0
        position.x -= kontrol_uzunlugu;
        position.y -= kontrol_uzunlugu; 
        for (int y = 0; y < objectSizeY; y++)
        {
            for (int x = 0; x < objectSizeX; x++)
            {
                if (grid.GetGrid()[(int)position.y + y, (int)position.x + x]!= 0)
                {
                    return true; // Eğer bu hücre doluysa, çakışma var
                }
            }
        }
        return false; // Çakışma yok
    }

    public GameObject SpawnObject(Vector3 spawnPosition, GameObject prefab, int objectSizeY, int objectSizeX, bool tek_mi, int kontrol_uzunlugu, int int_boyut)
    {
        if(tek_mi)
        {
            spawnPosition.x -= 0.5f;
            spawnPosition.y -= 0.5f;
        }
        Debug.Log("spawnPosition" + spawnPosition.x + " " + spawnPosition.y);
        GameObject newObject = Instantiate(prefab, spawnPosition, Quaternion.identity);
        Renderer renderer = newObject.GetComponent<Renderer>();
        if(renderer != null)
        {
            renderer.sortingOrder = 10;
        }
        if(tek_mi)
        {
            spawnPosition.x += 0.5f;
            spawnPosition.y += 0.5f;
        }

        // Nesneye bir kimlik ata ve grid hücrelerini işaretle
        this.AssignObjectId(spawnPosition, objectSizeY, objectSizeX, kontrol_uzunlugu, int_boyut);
        this.hazine = newObject;
        return newObject;
    }

    public void AssignObjectId( Vector3 position, int objectSizeY, int objectSizeX, int kontrol_uzunlugu,int  int_boyut)
    {
        GridArray grid = new GridArray();

        //negatif kısım
        position.x += int_boyut / 2;
        position.y += int_boyut / 2;

        // Nesne boyutu kadar bir kare alana denk gelen grid hücrelerine nesne kimliğini ata
        position.x -= kontrol_uzunlugu;
        position.y -= kontrol_uzunlugu; 
        for (int y = 0; y < objectSizeY; y++)
        {
            for (int x = 0; x < objectSizeX; x++)
            {
                grid.GetGrid()[(int)position.y + y, (int)position.x + x] = this.id;
            }
        }

        
    }

}