using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int pixel;

    bool[] grid;

    public void Init(int pixel)
    {
        grid = new bool[pixel];
        this.pixel = pixel;
    }
}
