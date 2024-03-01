using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

[RequireComponent(typeof(Grid))]
[RequireComponent(typeof(Tilemap))]

public class GridManager : MonoBehaviour
{
    Tilemap tilemap;
    Grid grid;
    [SerializeField] TileBase tileBase;
    [SerializeField] TileBase tileBase2;

    public InputField boyutInput;
    public int mapBoyut;

    //TESTTTT

    //ENGELLEEEEEEEEEEEEEEEEEEEEEEEEER

    
    
    private int[,] grid_dizi;
    private int objId = 2;



    //TEST SONU

    // Start is called before the first frame update
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        grid = GetComponent<Grid>();
        grid.Init(100);
        UpdateTilemap();


    }

    void UpdateTilemap()
    {
        for(int x = 0; x < grid.pixel; x++)
        {
            for(int y = 0; y < grid.pixel; y++)
            {
                if(x>=grid.pixel/2)
                {
                    tilemap.SetTile(new Vector3Int(x - (grid.pixel / 2), y - (grid.pixel / 2), 0), tileBase);
                }
                else
                {
                    tilemap.SetTile(new Vector3Int(x - (grid.pixel / 2), y - (grid.pixel / 2), 0), tileBase2);
                }
            }
        }
    }

    void UpdateMap(int mapBoyut)
    {
        //test
        Debug.Log(mapBoyut);
        
        
        tilemap = GetComponent<Tilemap>();
        grid = GetComponent<Grid>();
        grid.Init(mapBoyut);
        UpdateTilemap();


        //EngelBaslangic();
        
        

    }

    void EngelBaslangic()
    {
        grid_dizi = new int[getMapBoyut(), getMapBoyut()];

        // Engelleri ekleme
        EngelEkle();

    }


    void EngelEkle()
    {
        // hangi engelden ne kadar vs bilgileri burada oalcak


        
        //foreach(GameObject engel in engelListesi)
        //{
        //    // burada engel türünü ayırt etmemiz lazim ama nasiiiiiiiiiiiiiiiiiil - ona gore object size boyutu ile gonderilecek
        //    Vector3 spawnPos = GetRandomPosition(/*int objectSize*/);
        //}

        

        //SON
        
        //burada teker teker engeller eklenecek

        // Random engel ekleme
        //engel türüne göre parantez içi engel boyutu gönderilecek
        

        // çakışma var mı
        //...
    }

    Vector3 GetRandomPosition(/*int objectSize*/)
    {
        //OBJ BOYUTU NASIL YAPACAĞIZ AAaaaasdhasdhasdh


        int x = Random.Range(0, getMapBoyut() /* - objectSize */); 
        // Nesne boyutunu da göz önünde bulundurarak mapSize içinde rastgele bir x değeri seçelim
        int y = Random.Range(0, getMapBoyut()/* - objectSize */);
        // Nesne boyutunu da göz önünde bulundurarak mapSize içinde rastgele bir y değeri seçelim
        return new Vector3(x, y, 0f); // Z eksenini 0 olarak sabit tutuyoruz çünkü 2D ortamdayız
    }

    public void BoyutAtama()
    {
        MapiTemizle();
        string boyut;
        boyut = boyutInput.text;
        int int_boyut = int.Parse(boyut);
        UpdateMap(int_boyut);
    }
    void MapiTemizle()
    {
        tilemap.ClearAllTiles();
    }
    public int getMapBoyut()
    {
        return mapBoyut;
    }
}