using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnGenerator : MonoBehaviour
{
    //karakter
    public GameObject karakterPreFab;
    public GameObject karakterInstance;

    //tum nesneler
    public List<GameObject> nesneler = new List<GameObject>();

    //haraketsiz nesneler
    public GameObject YazAgacPreFab;
    public GameObject YazAgacInstance;

    public GameObject KisAgacPreFab;
    public GameObject KisAgacInstance;

    public GameObject YazKayaPreFab;
    public GameObject YazKayaInstance;

    public GameObject KisKayaPreFab;
    public GameObject KisKayaInstance;

    public GameObject DuvarlarPreFab;
    public GameObject DuvarlarInstance;

    public GameObject YazDagPreFab;
    public GameObject YazDagInstance;

    public GameObject KisDagPreFab;
    public GameObject KisDagInstance;

    //haraketli nesneler

    public GameObject KusPreFab;
    public GameObject KusInstance;

    public GameObject AriPreFab;
    public GameObject AriInstance;




    public InputField boyutInput;
    public int int_boyut;
    public int objId = 2;
    public void BoyutAtama()
    {
        KarakterTemizle();
        string boyut;
        boyut = boyutInput.text;
        int_boyut = int.Parse(boyut);

        Lokasyon karakter_lokasyonu = new Lokasyon(Random.Range(-int_boyut/2, int_boyut/2), Random.Range(-int_boyut/2, int_boyut/2));
        Karakter a = new Karakter(1, "Sefa", karakter_lokasyonu);
        GridArray g = new GridArray(int_boyut,int_boyut);
        print("Karakterin Lokasyonu: " + karakter_lokasyonu.getX() + " " + karakter_lokasyonu.getY());
        g.SetGrid(karakter_lokasyonu.getX(), karakter_lokasyonu.getY(), 1); // 1 -> KARAKTER
        g.printGrid();

       

        float x = karakter_lokasyonu.getX() + 0.5f;
        float y = karakter_lokasyonu.getY() + 0.5f;
        Vector3 randomLokasyon = new Vector3(x, y, 0);


        //karakter clone degerleri guncelleme
        karakterPreFab.GetComponent<Karakter>().ad = a.getAd();
        karakterPreFab.GetComponent<Karakter>().id = a.getId();

        karakterInstance = Instantiate(karakterPreFab, randomLokasyon, Quaternion.identity);


        //engelleri olusturma
        EngelOlusturma();
    }

    public void EngelOlusturma()
    {
        GridArray gridArray = new GridArray();

        int sabit_ust_sinir = 8;
        int agac_sayisi = Random.Range(6, sabit_ust_sinir);
        int dag_sayisi =  Random.Range(3, sabit_ust_sinir);
        int kaya_sayisi = Random.Range(8, sabit_ust_sinir);
        int duvar_sayisi = Random.Range(3, sabit_ust_sinir);

        //Debug.Log(agac_sayisi);
        //Debug.Log(dag_sayisi);
        //Debug.Log(kaya_sayisi);
        //Debug.Log(duvar_sayisi);

        int hareketli_ust_sinir = 4;

        int kus_sayisi = Random.Range(1, hareketli_ust_sinir);
        int arisayisi = Random.Range(2, hareketli_ust_sinir);

        //Debug.Log(kus_sayisi);
        //Debug.Log(arisayisi);

        //SAYI KADAR DONGULER ASAGIDA YAPILACAK

        //agac
        Debug.Log("agac sayisi" + agac_sayisi);
        for(int i = 0; i < agac_sayisi; i++)
        {
            Debug.Log(i);
            int x = Random.Range(-int_boyut/2+3, int_boyut/2-3);
            int y = Random.Range(-int_boyut/2+3, int_boyut/2-3);
            int kontrol_uzunlugu = 3;
            Vector3 randomLokasyon = new Vector3(x, y , 1); //z'ye bakilacak
            Agaclar a = new Agaclar(x,y,5,5,objId);

            //sag sol olayı bakilacak!!!
            while(!IsColliding(randomLokasyon, a.GetYukseklik(), a.GetGenislik(),kontrol_uzunlugu))
            {
                SpawnObject(randomLokasyon,YazAgacPreFab, a.GetYukseklik(),a.GetGenislik());
            }
        }

        //devami...



    }

    public bool IsColliding(Vector3 position, int objectSizeY, int objectSizeX, int kontrol_uzunlugu)
    {
        GridArray grid = new GridArray();
        // Verilen pozisyonun grid içinde olup olmadığını kontrol et
        if (position.x < -int_boyut/2+kontrol_uzunlugu || position.x >= int_boyut/2-kontrol_uzunlugu || position.y < -int_boyut/2+kontrol_uzunlugu || position.y >= int_boyut/2-kontrol_uzunlugu)
        {
            return true; // Pozisyon grid dışında, çakışma var
        }

        // Nesne boyutu kadar bir kare alana denk gelen grid hücrelerini kontrol et
        for (int y = 0; y < objectSizeY; y++)
        {
            for (int x = 0; x < objectSizeX; x++)
            {
                if (grid.GetGrid()[(int)position.y + y, (int)position.x + x] != 0)
                {
                    return true; // Eğer bu hücre doluysa, çakışma var
                }
            }
        }
        return false; // Çakışma yok
    }

    public void SpawnObject(Vector3 spawnPosition, GameObject prefab, int objectSizeY, int objectSizeX)
    {
        
        GameObject newObject = Instantiate(prefab, spawnPosition, Quaternion.identity);
        nesneler.Add(newObject);

        // Nesneye bir kimlik ata ve grid hücrelerini işaretle
        AssignObjectId(newObject, spawnPosition, objectSizeY, objectSizeX);
    }

    public void AssignObjectId(GameObject newObject, Vector3 position, int objectSizeY, int objectSizeX)
    {
        GridArray grid = new GridArray();
        // Nesne boyutu kadar bir kare alana denk gelen grid hücrelerine nesne kimliğini ata
        for (int y = 0; y < objectSizeY; y++)
        {
            for (int x = 0; x < objectSizeX; x++)
            {
                grid.GetGrid()[(int)position.y + y, (int)position.x + x] = objId;
            }
        }

        // Bir sonraki nesne için kimlik değerini artır
        objId++;
    }

    public void KarakterTemizle()
    {
        if(karakterInstance != null) // Check if an instance exists
        {
            Destroy(karakterInstance);
            
        }
        foreach(GameObject nesne in nesneler)
        {
            Destroy(nesne);
        }
    }
}
