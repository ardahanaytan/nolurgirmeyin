using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lokasyon
{
    public int x;
    public int y;
    public Lokasyon(int x_, int y_)
    {
        this.x = x_;
        this.y = y_;
    }

    public int getX() { return x; }
    public int getY() { return y; }

    public void setX(int x)
    {
        this.x = x;
    }

    public void setY(int y)
    {
        this.y = y;
    }
}