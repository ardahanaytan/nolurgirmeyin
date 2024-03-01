using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnGenerator : MonoBehaviour
{
    public GameObject karakterPreFab;
    public GameObject karakterInstance;

    public InputField boyutInput;
    public void BoyutAtama()
    {
        KarakterTemizle();
        string boyut;
        boyut = boyutInput.text;
        int int_boyut = int.Parse(boyut);

        Lokasyon karakter_lokasyonu = new Lokasyon(Random.Range(-int_boyut/2, int_boyut/2), Random.Range(-int_boyut/2, int_boyut/2));
        Karakter a = new Karakter(1, "Sefa", karakter_lokasyonu);

        float x = karakter_lokasyonu.getX() + 0.5f;
        float y = karakter_lokasyonu.getY() + 0.5f;
        Vector3 randomLokasyon = new Vector3(x, y, 0);


        //karakter clone degerleri guncelleme
        karakterPreFab.GetComponent<Karakter>().ad = a.getAd();
        karakterPreFab.GetComponent<Karakter>().id = a.getId();

        karakterInstance = Instantiate(karakterPreFab, randomLokasyon, Quaternion.identity);  
    }

    public void KarakterTemizle()
    {
        if(karakterInstance != null) // Check if an instance exists
        {
            Destroy(karakterInstance);
        }
    }
}
