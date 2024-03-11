using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Karakter : MonoBehaviour
{
    public static int id;
    public static string ad;
    public static Lokasyon l;
    public Grid grid;

    public static GridArray gridArray;

    public static GameObject instance;


    public Karakter()
    {
        
    }
    public Karakter(int id_, string ad_, Lokasyon l_)
    {
        id = id_;
        ad = ad_;
        l = l_;
        gridArray = new GridArray();
    }

    public int getId()
    {
        return id;
    }
    public void setId(int id_)
    {
        id = id_;
    }
    public string getAd()
    {
        return ad;
    }
    public void setAd(string ad_)
    {
        ad = ad_;
    }

    public Lokasyon getLokasyon()
    {
        return l;
    }

    public void setLokasyon(Lokasyon l_)
    {
        l = l_;
    }

    void Start()
    {
        
    }
    public GameObject getInstance()
    {
        return instance;
    }

    public void setInstance(GameObject instance_)
    {
        instance = instance_;
    }


    public void SolaGit(int gumus_sandik_no, int altin_sandik_no, int bakir_sandik_no, int zumrut_sandik_no)
    {
        SpawnGenerator spawnGenerator = new SpawnGenerator();
        //kenar kontrolü
        int temp_x = l.x + gridArray.getSize() / 2;
        if(temp_x <= 0)
        {
            return;
        }

        //buraya koşul durumları eklenecek - duvar var mı? hazine var mı? gibi
        // 0 degil mi kontrolu
        //Debug.Log(l.x + l.y);

        //Debug.Log(gumus_sandik_no + " " + altin_sandik_no + " " + bakir_sandik_no + " " + zumrut_sandik_no);
        
        //hazine
        //Debug.Log(gridArray.getOne(l.x-1,l.y));
        if(gridArray.getOne(l.x-1,l.y) == gumus_sandik_no)
        {
            Debug.Log("gumus sandik silme" + spawnGenerator.getHazineList().Count);
            foreach(Hazine h in spawnGenerator.getHazineList())
            {
                
                if(h.baslangic_x == l.x-1 && (h.baslangic_y == l.y || h.baslangic_y-1 == l.y))
                {
                    h.setSilindi_mi(true);
                    Destroy(h.getHazine());
                    gridArray.SetGrid(l.x, l.y, 0);
                    instance.transform.position += Vector3.left;
                    l.x -= 1;
                    gridArray.SetGrid(l.x, l.y, 1);
                    gridArray.SetGridSqaure(h.baslangic_x-1, h.baslangic_y -1, h.baslangic_x ,h.baslangic_y, 0);
                    break;

                }
            }
        }
        else if(gridArray.getOne(l.x-1,l.y) == altin_sandik_no)
        {
            Debug.Log("altin sandik silme" + spawnGenerator.getHazineList().Count);
            foreach(Hazine h in spawnGenerator.getHazineList())
            {
                
                
                if(h.baslangic_x == l.x-1 && (h.baslangic_y == l.y || h.baslangic_y-1 == l.y))
                {
                    h.setSilindi_mi(true);
                    Destroy(h.getHazine());
                    gridArray.SetGrid(l.x, l.y, 0);
                    instance.transform.position += Vector3.left;
                    l.x -= 1;
                    gridArray.SetGrid(l.x, l.y, 1);
                    gridArray.SetGridSqaure(h.baslangic_x-1, h.baslangic_y -1, h.baslangic_x ,h.baslangic_y, 0);
                    break;

                }
            }
        }
        else if(gridArray.getOne(l.x-1,l.y) == bakir_sandik_no)
        {
            Debug.Log("bakir sandik silme" + spawnGenerator.getHazineList().Count);
            foreach(Hazine h in spawnGenerator.getHazineList())
            {
                
                if(h.baslangic_x == l.x-1 && (h.baslangic_y == l.y || h.baslangic_y-1 == l.y))
                {
                    h.setSilindi_mi(true);
                    Destroy(h.getHazine());
                    gridArray.SetGrid(l.x, l.y, 0);
                    instance.transform.position += Vector3.left;
                    l.x -= 1;
                    gridArray.SetGrid(l.x, l.y, 1);
                    gridArray.SetGridSqaure(h.baslangic_x-1, h.baslangic_y -1, h.baslangic_x ,h.baslangic_y, 0);
                    break;

                }
            }

        }
        /*
        else if(gridArray.getOne(l.x-1,l.y) == zumrut_sandik_no)
        {
            foreach(Hazine h in spawnGenerator.getHazineList())
            {
                if(h.baslangic_x == l.x-1 && h.baslangic_y == l.y)
                {
                    Debug.Log("zumrut sandik silme");
                    h.setSilindi_mi(true);
                    Destroy(h.getHazine());
                    gridArray.SetGridSqaure(h.baslangic_x-1, h.baslangic_y -1, h.baslangic_x ,h.baslangic_y, 0);
                }
            }

        }
        */

        else if(gridArray.getOne(l.x-1,l.y) == 0)
        {
            instance.transform.position += Vector3.left;
            gridArray.SetGrid(l.x, l.y, 0);
            l.x -= 1;
            gridArray.SetGrid(l.x, l.y, 1);
        }
        

         
    }

    public void SagaGit(int gumus_sandik_no, int altin_sandik_no, int bakir_sandik_no, int zumrut_sandik_no)
    {
        SpawnGenerator spawnGenerator = new SpawnGenerator();

        //kenar kontrolü
        int temp_x = l.x + gridArray.getSize() / 2;
        //Debug.Log(temp_x);
        if(temp_x >= gridArray.getSize()- 1) // -1 en sol 0
        {
            return;
        }

        //hazine

        if(gridArray.getOne(l.x+1,l.y) == gumus_sandik_no)
        {
            Debug.Log("gumus sandik silme");
            foreach(Hazine h in spawnGenerator.getHazineList())
            {
                if(h.baslangic_x == l.x+2 && (h.baslangic_y == l.y || h.baslangic_y-1 == l.y))
                {
                    h.setSilindi_mi(true);
                    Destroy(h.getHazine());
                    gridArray.SetGrid(l.x, l.y, 0);
                    instance.transform.position += Vector3.right;
                    l.x += 1;
                    gridArray.SetGrid(l.x, l.y, 1);
                    gridArray.SetGridSqaure(h.baslangic_x-1, h.baslangic_y -1, h.baslangic_x ,h.baslangic_y, 0);
                    break;
                }
            }
        }
        else if(gridArray.getOne(l.x+1,l.y) == altin_sandik_no)
        {
            Debug.Log("altin sandik silme");
            foreach(Hazine h in spawnGenerator.getHazineList())
            {
                if(h.baslangic_x == l.x+2 && (h.baslangic_y == l.y || h.baslangic_y-1 == l.y))
                {
                    h.setSilindi_mi(true);
                    Destroy(h.getHazine());
                    gridArray.SetGrid(l.x, l.y, 0);
                    instance.transform.position += Vector3.right;
                    l.x += 1;
                    gridArray.SetGridSqaure(h.baslangic_x-1, h.baslangic_y -1, h.baslangic_x ,h.baslangic_y, 0);
                    break;
                }
            }
        }
        else if(gridArray.getOne(l.x+1,l.y) == bakir_sandik_no)
        {
            Debug.Log("bakir sandik silme");
            foreach(Hazine h in spawnGenerator.getHazineList())
            {
                if(h.baslangic_x == l.x+2 && (h.baslangic_y == l.y || h.baslangic_y-1 == l.y))
                {
                    h.setSilindi_mi(true);
                    Destroy(h.getHazine());
                    //listeden elemanı silme
                    spawnGenerator.getHazineList().Remove(h);

                    gridArray.SetGrid(l.x, l.y, 0);
                    instance.transform.position += Vector3.right;
                    l.x += 1;
                    gridArray.SetGrid(l.x, l.y, 1);
                    gridArray.SetGridSqaure(h.baslangic_x-1, h.baslangic_y -1, h.baslangic_x ,h.baslangic_y, 0);
                    break;
                }
            }
        }
        /*
        else if(gridArray.getOne(l.x+1,l.y) == zumrut_sandik_no)
        {
            foreach(Hazine h in spawnGenerator.getHazineList())
            {
                if(h.baslangic_x == l.x+1)
                {
                    h.setSilindi_mi(true);
                    Destroy(h.getHazine());
                    gridArray.SetGridSqaure(h.baslangic_x-1, h.baslangic_y -1, h.baslangic_x ,h.baslangic_y, 0);
                }
            }
        }
        */

        else if(gridArray.getOne(l.x+1,l.y) == 0)
        {
            instance.transform.position += Vector3.right;
            gridArray.SetGrid(l.x, l.y, 0);
            l.x += 1;
            gridArray.SetGrid(l.x, l.y, 1);
        }
        
    }

    public void YukariGit(int gumus_sandik_no, int altin_sandik_no, int bakir_sandik_no, int zumrut_sandik_no)
    {

        SpawnGenerator spawnGenerator = new SpawnGenerator();
        //kenar kontrolü
        int temp_y = l.y + gridArray.getSize() / 2;
        if(temp_y >= gridArray.getSize()- 1) // -1 en alt 0
        {
            return;
        }

        if(gridArray.getOne(l.x,l.y+1) == gumus_sandik_no)
        {
            Debug.Log("gumus sandik silme");
            foreach(Hazine h in spawnGenerator.getHazineList())
            {
                if(h.baslangic_y == l.y+2 && (h.baslangic_x == l.x || h.baslangic_x-1 == l.x))
                {
                    h.setSilindi_mi(true);
                    Destroy(h.getHazine());
                    gridArray.SetGrid(l.x, l.y, 0);
                    instance.transform.position += Vector3.up;
                    l.y += 1;
                    gridArray.SetGrid(l.x, l.y, 1);
                    gridArray.SetGridSqaure(h.baslangic_x-1, h.baslangic_y -1, h.baslangic_x ,h.baslangic_y, 0);
                    break;
                }
            }
        }
        else if(gridArray.getOne(l.x,l.y+1) == altin_sandik_no)
        {
            Debug.Log("altin sandik silme");
            foreach(Hazine h in spawnGenerator.getHazineList())
            {
                if(h.baslangic_y == l.y+2 && (h.baslangic_x == l.x || h.baslangic_x-1 == l.x))
                {
                    h.setSilindi_mi(true);
                    Destroy(h.getHazine());
                    gridArray.SetGrid(l.x, l.y, 0);
                    instance.transform.position += Vector3.up;
                    l.y += 1;
                    gridArray.SetGrid(l.x, l.y, 1);
                    gridArray.SetGridSqaure(h.baslangic_x-1, h.baslangic_y -1, h.baslangic_x ,h.baslangic_y, 0);
                    break;
                }
            }
        }
        else if(gridArray.getOne(l.x,l.y+1) == bakir_sandik_no)
        {
            Debug.Log("bakir sandik silme");
            foreach(Hazine h in spawnGenerator.getHazineList())
            {
                if(h.baslangic_y == l.y+2 && (h.baslangic_x == l.x || h.baslangic_x-1 == l.x))
                {
                    h.setSilindi_mi(true);
                    Destroy(h.getHazine());
                    gridArray.SetGrid(l.x, l.y, 0);
                    instance.transform.position += Vector3.up;
                    l.y += 1;
                    gridArray.SetGrid(l.x, l.y, 1);
                    gridArray.SetGridSqaure(h.baslangic_x-1, h.baslangic_y -1, h.baslangic_x ,h.baslangic_y, 0);
                    break;
                }
            }
        }
        /*
        else if(gridArray.getOne(l.x,l.y+1) == zumrut_sandik_no)
        {
            foreach(Hazine h in spawnGenerator.getHazineList())
            {
                if(h.baslangic_y == l.y+1)
                {
                    h.setSilindi_mi(true);
                    Destroy(h.getHazine());
                    gridArray.SetGridSqaure(h.baslangic_x-1, h.baslangic_y -1, h.baslangic_x ,h.baslangic_y, 0);
                }
            }
        }

        */
        else if(gridArray.getOne(l.x,l.y+1) == 0)
        {
            instance.transform.position += Vector3.up;
            gridArray.SetGrid(l.x, l.y, 0);
            l.y += 1;
            gridArray.SetGrid(l.x, l.y, 1);
        }
        
        
    }

    public void AsagiGit(int gumus_sandik_no, int altin_sandik_no, int bakir_sandik_no, int zumrut_sandik_no)
    {

        SpawnGenerator spawnGenerator = new SpawnGenerator();
        //kenar kontrolü
        int temp_y = l.y + gridArray.getSize() / 2;
        if(temp_y <= 0)
        {
            return;
        }

        //hazine

        if(gridArray.getOne(l.x,l.y-1) == gumus_sandik_no)
        {
            foreach(Hazine h in spawnGenerator.getHazineList())
            {
                if(h.baslangic_y == l.y-1 && (h.baslangic_x == l.x || h.baslangic_x-1 == l.x))
                {
                    h.setSilindi_mi(true);
                    Destroy(h.getHazine());
                    gridArray.SetGrid(l.x, l.y, 0);
                    instance.transform.position += Vector3.down;
                    l.y -= 1;
                    gridArray.SetGrid(l.x, l.y, 1);
                    gridArray.SetGridSqaure(h.baslangic_x-1, h.baslangic_y -1, h.baslangic_x ,h.baslangic_y, 0);
                    break;
                }
            }
        }
        else if(gridArray.getOne(l.x,l.y-1) == altin_sandik_no)
        {
            foreach(Hazine h in spawnGenerator.getHazineList())
            {
                if(h.baslangic_y == l.y-1  && (h.baslangic_x == l.x || h.baslangic_x-1 == l.x))
                {
                    h.setSilindi_mi(true);
                    Destroy(h.getHazine());
                    gridArray.SetGrid(l.x, l.y, 0);
                    instance.transform.position += Vector3.down;
                    l.y -= 1;
                    gridArray.SetGrid(l.x, l.y, 1);
                    gridArray.SetGridSqaure(h.baslangic_x-1, h.baslangic_y -1, h.baslangic_x ,h.baslangic_y, 0);
                    break;
                }
            }
        }
        else if(gridArray.getOne(l.x,l.y-1) == bakir_sandik_no)
        {
            foreach(Hazine h in spawnGenerator.getHazineList())
            {
                if(h.baslangic_y == l.y-1  && (h.baslangic_x == l.x || h.baslangic_x-1 == l.x))
                {
                    h.setSilindi_mi(true);
                    Destroy(h.getHazine());
                    gridArray.SetGrid(l.x, l.y, 0);
                    instance.transform.position += Vector3.down;
                    l.y -= 1;
                    gridArray.SetGrid(l.x, l.y, 1);
                    gridArray.SetGridSqaure(h.baslangic_x-1, h.baslangic_y -1, h.baslangic_x ,h.baslangic_y, 0);
                    break;
                }
            }
        }
        /*
        else if(gridArray.getOne(l.x,l.y-1) == zumrut_sandik_no)
        {
            foreach(Hazine h in spawnGenerator.getHazineList())
            {
                if(h.baslangic_y == l.y-1)
                {
                    h.setSilindi_mi(true);
                    Destroy(h.getHazine());
                    gridArray.SetGridSqaure(h.baslangic_x-1, h.baslangic_y -1, h.baslangic_x ,h.baslangic_y, 0);
                }
            }
        }
        */

        else if(gridArray.getOne(l.x,l.y-1) == 0)
        {
            instance.transform.position += Vector3.down;
            gridArray.SetGrid(l.x, l.y, 0);
            l.y -= 1;
            gridArray.SetGrid(l.x, l.y, 1);
        }
        
    }
    // EN KISA YOL

    
}