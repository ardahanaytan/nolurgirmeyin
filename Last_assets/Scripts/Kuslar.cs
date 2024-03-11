using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class Kuslar : Engel
{

    public GameObject kus;
    public int max_y;
    public int min_y;
    //public int gecerli_hedef;
    public Vector3 Yon = Vector3.down; 

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

    public Kuslar(int baslangic_x, int baslangic_y, int yukseklik, int genislik, int id) : base(baslangic_x, baslangic_y, yukseklik, genislik, id)
    {
        this.max_y = (baslangic_y + 5);
        this.min_y = (baslangic_y - 5);
    }
    public override  bool IsColliding(Vector3 position, int objectSizeY, int objectSizeX, int kontrol_uzunlugu, bool tek_mi, int int_boyut)
    {
        int kontrol_uzunlugu_x = 1;
        int kontrol_uzunlugu_y = kontrol_uzunlugu;
        GridArray grid = new GridArray();
        // Verilen pozisyonun grid içinde olup olmadığını kontrol et
        if (position.x < -int_boyut/2+kontrol_uzunlugu_x || position.x >= int_boyut/2-kontrol_uzunlugu_x || position.y < -int_boyut/2+kontrol_uzunlugu_y || position.y >= int_boyut/2-kontrol_uzunlugu_y)
        {
            return true; // Pozisyon grid dışında, çakışma var
        }

        //orta kisim kontrolü
        /*
        Debug.Log("*position.x" + position.x + " " + position.x + objectSizeX);
        if(position.x-kontrol_uzunlugu_x < 0 && position.x + kontrol_uzunlugu_x > 0)
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
        position.x -= kontrol_uzunlugu_x;
        position.y -= kontrol_uzunlugu_y; 
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
        //int kontrol_uzunlugu_x = 1;
        int kontrol_uzunlugu_y = kontrol_uzunlugu;
        if(tek_mi)
        {
            spawnPosition.x -= 0.5f;
            spawnPosition.y -= 0.5f;
        }
        //Debug.Log("spawnPosition" + spawnPosition.x + " " + spawnPosition.y);
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
        this.AssignObjectId(spawnPosition, objectSizeY, objectSizeX, kontrol_uzunlugu_y, int_boyut);
        return newObject;
    }
    public GameObject SpawnAnimal(Vector3 spawnPosition, GameObject prefab, bool tek_mi)
    {
        //Debug.Log("CİKCİKKKKKKKKKKKKKKKKKKKKKKKKKK");
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
            renderer.sortingOrder = 15;
        }
        if(tek_mi)
        {
            spawnPosition.x += 0.5f;
            spawnPosition.y += 0.5f;
        }
        this.kus = newObject;
        return newObject;

    }
    
    public  override void  AssignObjectId( Vector3 position, int objectSizeY, int objectSizeX, int kontrol_uzunlugu, int  int_boyut)
    {
        int kontrol_uzunlugu_x = 1;
        int kontrol_uzunlugu_y = kontrol_uzunlugu;
        GridArray grid = new GridArray();

        //negatif kısım
        position.x += int_boyut / 2;
        position.y += int_boyut / 2;

        // Nesne boyutu kadar bir kare alana denk gelen grid hücrelerine nesne kimliğini ata
        position.x -= kontrol_uzunlugu_x;
        position.y -= kontrol_uzunlugu_y; 
        for (int y = 0; y < objectSizeY; y++)
        {
            for (int x = 0; x < objectSizeX; x++)
            {
                grid.GetGrid()[(int)position.y + y, (int)position.x + x] = this.id;
            }
        }

        
    }

    public Vector3 getYon()
    {
        return Yon;
    }
    
    
    
}