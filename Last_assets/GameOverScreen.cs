using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameObject boyutGir;
    public GameObject yeniDunya;
    public GameObject baslat;
    
    public void Setup()
    {
        gameObject.SetActive(true);
        boyutGir.SetActive(false);
        yeniDunya.SetActive(false);
        baslat.SetActive(false);
    }

    public void Restart()
    {
        gameObject.SetActive(false);
        boyutGir.SetActive(true);
        yeniDunya.SetActive(true);

        Karakter k = new Karakter();
        k.getInstance().transform.position = new Vector3(0,0,0);
        
    }
}
