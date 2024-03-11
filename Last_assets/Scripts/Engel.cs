using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Engel : MonoBehaviour
{
    public int yukseklik;
    public int genislik;
    public int id;
    public int[][] engel_dizisi;
    public int baslangic_x;
    public int baslangic_y;
    //hangi boyutta olma durumuna göre özel resim basacağımızdan buraya ekstra bir değişken gerecek

    public abstract int GetGenislik();
    public abstract int GetYukseklik();
    public abstract int GetId();

    public abstract void SetGenislik(int genislik);
    public abstract void SetYukseklik(int yukseklik);
    public abstract void SetId(int id);

    public Engel(int baslangic_x, int baslangic_y, int yukseklik, int genislik, int id)
    {
        this.baslangic_x = baslangic_x;
        this.baslangic_y = baslangic_y;
        this.yukseklik = yukseklik;
        this.genislik = genislik;
        this.id = id;

    }

    public abstract bool IsColliding(Vector3 position, int objectSizeY, int objectSizeX, int kontrol_uzunlugu, bool tek_mi, int int_boyut);
    public abstract GameObject SpawnObject(Vector3 spawnPosition, GameObject prefab, int objectSizeY, int objectSizeX, bool tek_mi, int kontrol_uzunlugu, int int_boyut);
    public abstract void AssignObjectId( Vector3 position, int objectSizeY, int objectSizeX, int kontrol_uzunlugu,int  int_boyut);


}
