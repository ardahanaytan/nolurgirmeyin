using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
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
    //public GameObject YazAgacInstance;

    public GameObject KisAgacPreFab;
    //public GameObject KisAgacInstance;

    public GameObject YazKayaPreFab;
    //public GameObject YazKayaInstance;

    public GameObject KisKayaPreFab;
    //public GameObject KisKayaInstance;

    public GameObject DuvarlarPreFab;
    //public GameObject DuvarlarInstance;

    public GameObject YazDagPreFab;
    //public GameObject YazDagInstance;

    public GameObject KisDagPreFab;
    //public GameObject KisDagInstance;

    //haraketli nesneler

    public GameObject KusPreFab;
    public GameObject KusPre;
    //public GameObject KusInstance;

    public GameObject AriPreFab;
    public GameObject AriPre;
    //public GameObject AriInstance;

    public GameObject AltinSandikPreFab;
    public GameObject GumusSandikPreFab;
    public GameObject BakirSandikPreFab;
    public GameObject ZumrutSandikPreFab;

    public int gumus_sandik_no;
    public int altin_sandik_no;
    public int bakir_sandik_no;
    public int zumrut_sandik_no;

    private List<Kuslar> kuslarList = new List<Kuslar>();
    private List<Arilar> arilarList = new List<Arilar>();
    public static List<Hazine> hazineList = new List<Hazine>();

    public GameObject kamera;


    public float hiz = 100f;

    public GameOverScreen gameOverScreen;

    public InputField boyutInput;
    public int int_boyut;
    public int objId = 2;

    public SpawnGenerator()
    {
    }

    public List<Hazine> getHazineList()
    {
        return hazineList;
    }
    public void BoyutAtama()
    {
        Temizle();
        string boyut;
        boyut = boyutInput.text;
        int_boyut = int.Parse(boyut);

        Lokasyon karakter_lokasyonu = new Lokasyon(Random.Range(-int_boyut/2, int_boyut/2), Random.Range(-int_boyut/2, int_boyut/2));
        Karakter a = new Karakter(1, "Sefa", karakter_lokasyonu);
        GridArray g = new GridArray(int_boyut,int_boyut);
        print("Karakterin Lokasyonu: " + karakter_lokasyonu.getX() + " " + karakter_lokasyonu.getY());
        g.SetGrid(karakter_lokasyonu.getX(), karakter_lokasyonu.getY(), 1); // 1 -> KARAKTER
        //g.printGrid();

       

        float x = karakter_lokasyonu.getX() + 0.5f;
        float y = karakter_lokasyonu.getY() + 0.5f;
        Vector3 randomLokasyon = new Vector3(x, y, 0);


        //karakter clone degerleri guncelleme
       //karakterPreFab.GetComponent<Karakter>().getAd() = a.getAd();
       //karakterPreFab.GetComponent<Karakter>().id = a.getId();

        karakterInstance = Instantiate(karakterPreFab, randomLokasyon, Quaternion.identity);
        kamera.GetComponent<CameraSystem>().player = karakterInstance;
        a.setInstance(karakterInstance);

        //deneme
        //Vector3 randomLokasyon2 = new Vector3(1, 1, 10);
        //GameObject YazAgacInstance1 = Instantiate(YazAgacPreFab,randomLokasyon2 , Quaternion.identity);
        //nesneler.Add(YazAgacInstance1);


        //engelleri olusturma
        EngelOlusturma();
        //g.printGrid();
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

        int sandik_ust_sinir = 8;
        int altin_sandik_sayisi = Random.Range(5, sandik_ust_sinir);
        int gumus_sandik_sayisi = Random.Range(5, sandik_ust_sinir);
        int bakir_sandik_sayisi = Random.Range(5, sandik_ust_sinir);
        int zumrut_sandik_sayisi = Random.Range(5, sandik_ust_sinir);

        //SAYI KADAR DONGULER ASAGIDA YAPILACAK
        int deneme_sayisi = 1000;
        //agac
        Debug.Log("agac sayisi" + agac_sayisi);
        for(int i = 0; i < agac_sayisi; i++)
        {

            int kontrol_uzunlugu = 3;
            int yer_bulundu_mu = 0;
            bool tek_mi = true;
            for(int s = 0;s < deneme_sayisi;s++)
            {
                //Debug.Log("deneme" + s);
                int x = Random.Range(-int_boyut/2+kontrol_uzunlugu, int_boyut/2-kontrol_uzunlugu);
                int y = Random.Range(-int_boyut/2+kontrol_uzunlugu, int_boyut/2-kontrol_uzunlugu);
                Agaclar a = new Agaclar(x,y,5,5,objId);
                //Debug.Log("deneme" + s + "=>" + x + " " + y);
                Vector3 randomLokasyon = new Vector3(x, y , 1); //z'ye bakilacak
                if(!a.IsColliding(randomLokasyon, a.GetYukseklik(), a.GetGenislik(),kontrol_uzunlugu, tek_mi, int_boyut))
                {
                    yer_bulundu_mu = 1;
                    Debug.Log("Yer bulundu" + randomLokasyon.x + " " + randomLokasyon.y);
                    if(randomLokasyon.x < 0)
                    {
                        GameObject engel = a.SpawnObject(randomLokasyon,KisAgacPreFab, a.GetYukseklik(),a.GetGenislik(), tek_mi, kontrol_uzunlugu, int_boyut);
                        nesneler.Add(engel);
                        objId++;
                    }
                    else
                    {
                        GameObject engel1 = a.SpawnObject(randomLokasyon,YazAgacPreFab, a.GetYukseklik(),a.GetGenislik(), tek_mi, kontrol_uzunlugu, int_boyut);
                        nesneler.Add(engel1);
                        objId++;
                    }
                    break;
                }
                
            }
            if(yer_bulundu_mu == 0)
            {
                Debug.Log("Yer bulunamadi");
                //Temizle();
                ObjeTemizle();
                gameOverScreen.Setup();

                
                return;
            }
        }
        
        //dag
        Debug.Log("dag sayisi" + dag_sayisi);
        for(int i = 0; i < dag_sayisi; i++)
        {
            int kontrol_uzunlugu = 8;
            int yer_bulundu_mu = 0;
            bool tek_mi = true;
            for(int s = 0; s < deneme_sayisi ; s++)
            {
                //Debug.Log("deneme" + s);
                int x = Random.Range(-int_boyut/2+kontrol_uzunlugu, int_boyut/2-kontrol_uzunlugu);
                int y = Random.Range(-int_boyut/2+kontrol_uzunlugu, int_boyut/2-kontrol_uzunlugu);
                Daglar d = new Daglar(x,y,15,15,objId);
                //Debug.Log("deneme" + s + "=>" + x + " " + y);
                Vector3 randomLokasyon = new Vector3(x, y , 1); //z'ye bakilacak
                if(!d.IsColliding(randomLokasyon, d.GetYukseklik(), d.GetGenislik(),kontrol_uzunlugu, tek_mi, int_boyut))
                {
                    yer_bulundu_mu = 1;
                    Debug.Log("Yer bulundu" + randomLokasyon.x + " " + randomLokasyon.y);
                    if(randomLokasyon.x < 0)
                    {
                        GameObject engel = d.SpawnObject(randomLokasyon,KisDagPreFab, d.GetYukseklik(),d.GetGenislik(), tek_mi, kontrol_uzunlugu, int_boyut);
                        nesneler.Add(engel);
                        objId++;
                    }
                    else
                    {
                        GameObject engel1 = d.SpawnObject(randomLokasyon,YazDagPreFab, d.GetYukseklik(),d.GetGenislik(), tek_mi, kontrol_uzunlugu, int_boyut);
                        nesneler.Add(engel1);
                        objId++;
                    }
                    break;
                }
            }
            if(yer_bulundu_mu == 0)
            {
                Debug.Log("Yer bulunamadi");
                
                //Temizle();
                ObjeTemizle();
                gameOverScreen.Setup();
                
               return;
            }
        }
        

        //kaya
        Debug.Log("kaya sayisi" + kaya_sayisi);
        for(int i = 0; i < kaya_sayisi;i++)
        {
            int kontrol_uzunlugu = 1;
            int yer_bulundu_mu = 0;
            bool tek_mi = false;
            for(int s = 0; s < deneme_sayisi; s++)
            {
                //Debug.Log("deneme" + s);
                int x = Random.Range(-int_boyut/2+kontrol_uzunlugu, int_boyut/2-kontrol_uzunlugu);
                int y = Random.Range(-int_boyut/2+kontrol_uzunlugu, int_boyut/2-kontrol_uzunlugu);
                Kayalar k = new Kayalar(x,y,2,2,objId);
                //Debug.Log("deneme" + s + "=>" + x + " " + y);
                Vector3 randomLokasyon = new Vector3(x, y , 1); //z'ye bakilacak
                if(!k.IsColliding(randomLokasyon, k.GetYukseklik(), k.GetGenislik(),kontrol_uzunlugu, tek_mi, int_boyut))
                {
                    yer_bulundu_mu = 1;
                    Debug.Log("Yer bulundu" + randomLokasyon.x + " " + randomLokasyon.y);
                    if(randomLokasyon.x < 0)
                    {
                        GameObject engel = k.SpawnObject(randomLokasyon,KisKayaPreFab, k.GetYukseklik(),k.GetGenislik(), tek_mi, kontrol_uzunlugu, int_boyut);
                        nesneler.Add(engel);
                        objId++;
                    }
                    else
                    {
                        GameObject engel1 = k.SpawnObject(randomLokasyon,YazKayaPreFab, k.GetYukseklik(),k.GetGenislik(), tek_mi, kontrol_uzunlugu, int_boyut);
                        nesneler.Add(engel1);
                        objId++;
                    }
                    break;
                }
            }
            if(yer_bulundu_mu == 0)
            {
                Debug.Log("Yer bulunamadi");
                //Temizle();
                ObjeTemizle();
                gameOverScreen.Setup();
               
                return;
            }
            
        }
        
        
        //duvar
        Debug.Log("duvar sayisi" + duvar_sayisi);
        for(int i = 0; i < duvar_sayisi; i++)
        {
            int kontrol_uzunlugu = 5;
            int yer_bulundu_mu = 0;
            bool tek_mi = true;
            for(int s = 0; s < deneme_sayisi; s++)
            {
                //Debug.Log("deneme" + s);
                int x = Random.Range(-int_boyut/2+kontrol_uzunlugu, int_boyut/2-kontrol_uzunlugu);
                int y = Random.Range(-int_boyut/2+1, int_boyut/2-1);
                Duvarlar d = new Duvarlar(x,y,1,10,objId);
                //Debug.Log("deneme" + s + "=>" + x + " " + y);
                Vector3 randomLokasyon = new Vector3(x, y , 1); //z'ye bakilacak
                if(!d.IsColliding(randomLokasyon, d.GetYukseklik(), d.GetGenislik(),kontrol_uzunlugu, tek_mi, int_boyut))
                {
                    yer_bulundu_mu = 1;
                    Debug.Log("Yer bulundu" + randomLokasyon.x + " " + randomLokasyon.y);
                    GameObject engel = d.SpawnObject(randomLokasyon,DuvarlarPreFab, d.GetYukseklik(),d.GetGenislik(), tek_mi, kontrol_uzunlugu, int_boyut);
                    nesneler.Add(engel);
                    //gridArray.printGrid();
                    objId++;
                    break;
                }
            }
            if(yer_bulundu_mu == 0)
            {
                Debug.Log("Yer bulunamadi");
                //Temizle();
                ObjeTemizle();
                gameOverScreen.Setup();
                
                return;
            }
        }
        //kus
        Debug.Log("kus sayisi" + kus_sayisi);
        for(int i = 0; i < kus_sayisi; i++)
        {
            int kontrol_uzunlugu_x = 1;
            int kontrol_uzunlugu_y = 6;
            int yer_bulundu_mu = 0;
            bool tek_mi = false;
            for(int s = 0; s < deneme_sayisi; s++)
            {
                //Debug.Log("deneme" + s);
                int x = Random.Range(-int_boyut/2+kontrol_uzunlugu_x, int_boyut/2-kontrol_uzunlugu_x);
                int y = Random.Range(-int_boyut/2+kontrol_uzunlugu_y, int_boyut/2-kontrol_uzunlugu_y);
                //Arilar a = new Arilar(x,y,12,2,objId);
                Kuslar k = new Kuslar(x,y,12,2,objId);
                
                //Debug.Log("deneme" + s + "=>" + x + " " + y);
                Vector3 randomLokasyon = new Vector3(x, y , 1); //z'ye bakilacak
                if(!k.IsColliding(randomLokasyon, k.GetYukseklik(), k.GetGenislik(),kontrol_uzunlugu_y, tek_mi, int_boyut))
                {
                    kuslarList.Add(k);
                    yer_bulundu_mu = 1;
                    Debug.Log("Yer bulundu" + randomLokasyon.x + " " + randomLokasyon.y);
                    GameObject engel = k.SpawnObject(randomLokasyon,KusPreFab, k.GetYukseklik(),k.GetGenislik(), tek_mi, kontrol_uzunlugu_y, int_boyut);
                    nesneler.Add(engel);
                    GameObject hayvan = k.SpawnAnimal(randomLokasyon,KusPre, tek_mi);
                    nesneler.Add(hayvan);
                    //gridArray.printGrid();
                    objId++;
                    break;
                }
            }
            if(yer_bulundu_mu == 0)
            {
                Debug.Log("Yer bulunamadi");
                //Temizle();
                ObjeTemizle();
                gameOverScreen.Setup();
                
                return;
            }
        }

        //ari
        Debug.Log("ari sayisi" + arisayisi);
        for(int i = 0; i < arisayisi; i++)
        {
            int kontrol_uzunlugu_x = 4;
            int kontrol_uzunlugu_y = 1;
            int yer_bulundu_mu = 0;
            bool tek_mi = false;
            for(int s = 0; s < deneme_sayisi; s++)
            {
                //Debug.Log("deneme" + s);
                int x = Random.Range(-int_boyut/2+kontrol_uzunlugu_x, int_boyut/2-kontrol_uzunlugu_x);
                int y = Random.Range(-int_boyut/2+kontrol_uzunlugu_y, int_boyut/2-kontrol_uzunlugu_y);
                Arilar a = new Arilar(x,y,2,8,objId);
                
                //Debug.Log("deneme" + s + "=>" + x + " " + y);
                Vector3 randomLokasyon = new Vector3(x, y , 1); //z'ye bakilacak
                if(!a.IsColliding(randomLokasyon, a.GetYukseklik(), a.GetGenislik(),kontrol_uzunlugu_x, tek_mi, int_boyut))
                {
                    arilarList.Add(a);
                    yer_bulundu_mu = 1;
                    Debug.Log("Yer bulundu" + randomLokasyon.x + " " + randomLokasyon.y);
                    GameObject engel = a.SpawnObject(randomLokasyon,AriPreFab, a.GetYukseklik(),a.GetGenislik(), tek_mi, kontrol_uzunlugu_x, int_boyut);
                    nesneler.Add(engel);
                    GameObject hayvan = a.SpawnAnimal(randomLokasyon,AriPre, tek_mi);
                    nesneler.Add(hayvan);
                    
                    //gridArray.printGrid();
                    objId++;
                    break;
                }
            }
            if(yer_bulundu_mu == 0)
            {
                Debug.Log("Yer bulunamadi");
                //Temizle();
                ObjeTemizle();
                gameOverScreen.Setup();
                
                return;
            }

        }

        //sandiklar

        //gumus sandik
        Debug.Log("gumus sandik sayisi" + gumus_sandik_sayisi);
        for(int i = 0; i < gumus_sandik_sayisi; i++)
        {
            int kontrol_uzunlugu = 1;
            int yer_bulundu_mu = 0;
            bool tek_mi = false;
            for(int s = 0; s < deneme_sayisi; s++)
            {
                int x = Random.Range(-int_boyut/2+kontrol_uzunlugu, int_boyut/2-kontrol_uzunlugu);
                int y = Random.Range(-int_boyut/2+kontrol_uzunlugu, int_boyut/2-kontrol_uzunlugu);
                Hazine gumus = new Hazine(x,y,2,2,"gumus",objId);
                Vector3 randomLokasyon = new Vector3(x, y , 1); //z'ye bakilacak
                if(!gumus.IsColliding(randomLokasyon, gumus.getGenislik(), gumus.getYukseklik(),kontrol_uzunlugu, tek_mi, int_boyut))
                {
                    yer_bulundu_mu = 1;
                    Debug.Log("Yer bulundu" + randomLokasyon.x + " " + randomLokasyon.y);
                    GameObject hazine = gumus.SpawnObject(randomLokasyon,GumusSandikPreFab, gumus.getYukseklik(),gumus.getGenislik(), tek_mi, kontrol_uzunlugu, int_boyut);
                    //nesneler.Add(hazine);
                    hazineList.Add(gumus);
                    //objId++;
                    break;
                }
            }
            if(yer_bulundu_mu == 0)
            {
                Debug.Log("Yer bulunamadi");
                //Temizle();
                ObjeTemizle();
                gameOverScreen.Setup();
                
                return;
            }
        }
        gumus_sandik_no = objId;
        gridArray.setGumusSandikNo(gumus_sandik_no);
        objId++;

        //altin sandik
        Debug.Log("altin sandik sayisi" + altin_sandik_sayisi);
        for(int i = 0; i < altin_sandik_sayisi; i++)
        {
            int kontrol_uzunlugu = 1;
            int yer_bulundu_mu = 0;
            bool tek_mi = false;
            for(int s = 0; s < deneme_sayisi; s++)
            {
                int x = Random.Range(-int_boyut/2+kontrol_uzunlugu, int_boyut/2-kontrol_uzunlugu);
                int y = Random.Range(-int_boyut/2+kontrol_uzunlugu, int_boyut/2-kontrol_uzunlugu);
                Hazine altin = new Hazine(x,y,2,2,"altin",objId);
                Vector3 randomLokasyon = new Vector3(x, y , 1); //z'ye bakilacak
                if(!altin.IsColliding(randomLokasyon, altin.getGenislik(), altin.getYukseklik(),kontrol_uzunlugu, tek_mi, int_boyut))
                {
                    yer_bulundu_mu = 1;
                    Debug.Log("Yer bulundu" + randomLokasyon.x + " " + randomLokasyon.y);
                    GameObject hazine = altin.SpawnObject(randomLokasyon,AltinSandikPreFab, altin.getYukseklik(),altin.getGenislik(), tek_mi, kontrol_uzunlugu, int_boyut);
                    //nesneler.Add(hazine);
                    hazineList.Add(altin);
                    //objId++;
                    break;
                }
            }
            if(yer_bulundu_mu == 0)
            {
                Debug.Log("Yer bulunamadi");
                //Temizle();
                ObjeTemizle();
                gameOverScreen.Setup();
                
                return;
            }
        }
        altin_sandik_no = objId;
        gridArray.setAltinSandikNo(altin_sandik_no);
        objId++;

        //bakir sandik
        Debug.Log("bakir sandik sayisi" + bakir_sandik_sayisi);
        for(int i = 0; i < bakir_sandik_sayisi; i++)
        {
            int kontrol_uzunlugu = 1;
            int yer_bulundu_mu = 0;
            bool tek_mi = false;
            for(int s = 0; s < deneme_sayisi; s++)
            {
                int x = Random.Range(-int_boyut/2+kontrol_uzunlugu, int_boyut/2-kontrol_uzunlugu);
                int y = Random.Range(-int_boyut/2+kontrol_uzunlugu, int_boyut/2-kontrol_uzunlugu);
                Hazine bakir = new Hazine(x,y,2,2,"bakir",objId);
                Vector3 randomLokasyon = new Vector3(x, y , 1); //z'ye bakilacak
                if(!bakir.IsColliding(randomLokasyon, bakir.getGenislik(), bakir.getYukseklik(),kontrol_uzunlugu, tek_mi, int_boyut))
                {
                    yer_bulundu_mu = 1;
                    Debug.Log("Yer bulundu" + randomLokasyon.x + " " + randomLokasyon.y);
                    GameObject hazine = bakir.SpawnObject(randomLokasyon,BakirSandikPreFab, bakir.getYukseklik(),bakir.getGenislik(), tek_mi, kontrol_uzunlugu, int_boyut);
                    //nesneler.Add(hazine);
                    hazineList.Add(bakir);
                    //objId++;
                    break;
                }
            }
            if(yer_bulundu_mu == 0)
            {
                Debug.Log("Yer bulunamadi");
                //Temizle();
                ObjeTemizle();
                gameOverScreen.Setup();
                
                return;
            }
        }
        bakir_sandik_no = objId;
        gridArray.setBakirSandikNo(bakir_sandik_no);
        objId++;

        //zumrut sandik
        /*
        Debug.Log("zumrut sandik sayisi" + zumrut_sandik_sayisi);
        for(int i = 0; i < zumrut_sandik_sayisi; i++)
        {
            int kontrol_uzunlugu = 1;
            int yer_bulundu_mu = 0;
            bool tek_mi = false;
            for(int s = 0; s < deneme_sayisi; s++)
            {
                int x = Random.Range(-int_boyut/2+kontrol_uzunlugu, int_boyut/2-kontrol_uzunlugu);
                int y = Random.Range(-int_boyut/2+kontrol_uzunlugu, int_boyut/2-kontrol_uzunlugu);
                Hazine zumrut = new Hazine(x,y,2,2,"zumrut",objId);
                Vector3 randomLokasyon = new Vector3(x, y , 1); //z'ye bakilacak
                if(!zumrut.IsColliding(randomLokasyon, zumrut.getGenislik(), zumrut.getYukseklik(),kontrol_uzunlugu, tek_mi, int_boyut))
                {
                    yer_bulundu_mu = 1;
                    Debug.Log("Yer bulundu" + randomLokasyon.x + " " + randomLokasyon.y);
                    GameObject hazine = zumrut.SpawnObject(randomLokasyon,ZumrutSandikPreFab, zumrut.getYukseklik(),zumrut.getGenislik(), tek_mi, kontrol_uzunlugu, int_boyut);
                    //nesneler.Add(hazine);
                    hazineList.Add(zumrut);
                    //objId++;
                    break;
                }
            }
            if(yer_bulundu_mu == 0)
            {
                Debug.Log("Yer bulunamadi");
                //Temizle();
                ObjeTemizle();
                gameOverScreen.Setup();
                
                return;
            }
        }
        zumrut_sandik_no = objId;
        gridArray.setZumrutSandikNo(zumrut_sandik_no);
        objId++;
        */

        if(hazinePathFinder() == false)
        {
            // pathfinder oyun sonu ekrani

        }


        GridArray.herSeyHazirMi = true;
    }

    public void ObjeTemizle() // kamera olayi icin - game over olayi
    {
        foreach(Hazine h in hazineList)
        {
            if(h.silindi_mi == false)
            {
                h.setSilindi_mi(true);
                Destroy(h.getHazine());
            }
        }

        foreach(GameObject nesne in nesneler)
        {
            Destroy(nesne);
        }
        nesneler.Clear();


        //listeleri temizle
        kuslarList.Clear();
        arilarList.Clear();
    }

    public void Temizle()
    {
        if(karakterInstance != null) // Check if an instance exists
        {
            Destroy(karakterInstance);
            
        }
        foreach(Hazine h in hazineList)
        {
            if(h.silindi_mi == false)
            {
                h.setSilindi_mi(true);
                Destroy(h.getHazine());
            }
        }
        foreach(GameObject nesne in nesneler)
        {
            Destroy(nesne);
        }
        nesneler.Clear();

        //listeleri temizle
        kuslarList.Clear();
        arilarList.Clear();
    }

    void Update()
    {
        GridArray gridArray = new GridArray();
        if(gridArray.getHazir())
        {
            foreach (Kuslar kus in kuslarList)
            {
                GameObject kusPrefab = kus.kus;
                kusPrefab.transform.position += kus.getYon()*30 * 0.1f * Time.deltaTime;
                if(kusPrefab.transform.position.y <= kus.min_y)
                {
                    kus.Yon = Vector3.up;
                }
                else if(kusPrefab.transform.position.y >= kus.max_y)
                {
                    kus.Yon = Vector3.down;
                }
            }
            foreach(Arilar ari in arilarList)
            {
                GameObject ariPrefab = ari.ari;
                ariPrefab.transform.position += ari.GetYon()*30 * 0.1f * Time.deltaTime;
                if(ariPrefab.transform.position.x <= ari.min_x)
                {
                    ari.Yon = Vector3.right;
                }
                else if(ariPrefab.transform.position.x >= ari.max_x)
                {
                    ari.Yon = Vector3.left;
                }
            }

            if(!boyutInput.IsActive())
            {
                KarakterHareket();   
            }
        }
        
    }
    
    void KarakterHareket()
    {
        Karakter k = new Karakter();
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //karakterInstance.transform.Translate(Vector3.left);
            //karakterInstance.transform.position += Vector3.left;
            k.SolaGit(gumus_sandik_no, altin_sandik_no, bakir_sandik_no, zumrut_sandik_no);
        
        }
        else if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            //karakterInstance.transform.Translate(Vector3.right);
            k.SagaGit(gumus_sandik_no, altin_sandik_no, bakir_sandik_no, zumrut_sandik_no);
        }
        else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            //karakterInstance.transform.Translate(Vector3.down);
            k.AsagiGit(gumus_sandik_no, altin_sandik_no, bakir_sandik_no, zumrut_sandik_no);
        }
        else if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            //karakterInstance.transform.Translate(Vector3.up);
            k.YukariGit(gumus_sandik_no, altin_sandik_no, bakir_sandik_no, zumrut_sandik_no);
        }
    }

    bool isCoinOrPath(int y,int x)
    {
        GridArray gridArray = new GridArray();
        int[,] grid = gridArray.GetGrid();
        if(grid[y,x] == 0 || grid[y,x] == gridArray.getGumusSandikNo() || grid[y,x] == gridArray.getAltinSandikNo() || grid[y,x] == gridArray.getBakirSandikNo() || grid[y,x] == gridArray.getZumrutSandikNo())
        {
            return true;
        }
        return false;

    }

    int[,] pathfinderRecursive(int[,] grid, int y, int x)
    {
        /*// kontrol ama gerek yok gibi
        if(x < 0 || y < 0 || x >= int_boyut || y >= int_boyut)
        {
            return grid;
        }
        */

        if(grid[y,x] != -27)
        {
            grid[y,x] = -27;
        }

        if(y-1 >= 0 && isCoinOrPath(y-1,x) && grid[y-1,x] != -27)
        {
            grid[y-1,x] = -27;
            pathfinderRecursive(grid,y-1,x);
        }
        if(x+1 < int_boyut && isCoinOrPath(y,x+1) && grid[y,x+1] != -27)
        {
            grid[y,x+1] = -27;
            pathfinderRecursive(grid,y,x+1);
        }
        if(y+1 < int_boyut && isCoinOrPath(y+1,x) && grid[y+1,x] != -27)
        {
            grid[y+1,x] = -27;
            pathfinderRecursive(grid,y+1,x);
        }
        if(x-1 >= 0 && isCoinOrPath(y,x-1) && grid[y,x-1] != -27)
        {
            grid[y,x-1] = -27;
            pathfinderRecursive(grid,y,x-1);
        }

        return grid;
    }

    bool hazinePathFinder()
    {
        GridArray gridArray = new GridArray();
        int[,] grid = gridArray.GetGrid();

        //copy grid

        int[,] gridCopy = new int[int_boyut,int_boyut];
        for(int i = 0; i < int_boyut; i++)
        {
            for(int j = 0; j < int_boyut; j++)
            {
                gridCopy[i,j] = grid[i,j];
            }
        }

        Karakter k = new Karakter();
        int x = k.getLokasyon().getX() + int_boyut/2;
        int y = k.getLokasyon().getY() + int_boyut/2;

        int[,] ret = pathfinderRecursive(gridCopy,y,x);

        /*
        //yazdirma
        for(int i = 0; i < int_boyut; i++)
        {
            string str = "";
            for(int j = 0; j < int_boyut; j++)
            {
                str += ret[i,j].ToString() + " ";
            }
            Debug.Log(str);
        }
        */

        //hazine kontrolu
       


        return true;
    }

    
}
