using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class Daglar : Engel
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

    public Daglar(int baslangic_x, int baslangic_y, int yukseklik, int genislik, int id) : base(baslangic_x, baslangic_y, yukseklik, genislik, id)
    {

    }
    public override bool IsColliding(Vector3 position, int objectSizeY, int objectSizeX, int kontrol_uzunlugu, bool tek_mi, int int_boyut)
    {
        GridArray grid = new GridArray();
        // Verilen pozisyonun grid içinde olup olmadığını kontrol et
        if (position.x < -int_boyut/2+kontrol_uzunlugu || position.x >= int_boyut/2-kontrol_uzunlugu || position.y < -int_boyut/2+kontrol_uzunlugu || position.y >= int_boyut/2-kontrol_uzunlugu)
        {
            return true; // Pozisyon grid dışında, çakışma var
        }

        //orta kisim kontrolü
        Debug.Log("*position.x" + position.x + " " + position.x + objectSizeX);
        if(position.x-kontrol_uzunlugu < 0 && position.x + kontrol_uzunlugu > 0)
        {
            return true;
        }


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

     public override GameObject SpawnObject(Vector3 spawnPosition, GameObject prefab, int objectSizeY, int objectSizeX, bool tek_mi, int kontrol_uzunlugu, int int_boyut)
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
        return newObject;
    }
    
    public override void AssignObjectId( Vector3 position, int objectSizeY, int objectSizeX, int kontrol_uzunlugu,int  int_boyut)
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