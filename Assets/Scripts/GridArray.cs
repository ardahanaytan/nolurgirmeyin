using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridArray
{
    public static int[,] grid;
    public static int size;


    public GridArray()
    {

    }
    public GridArray(int x, int y)
    {
        grid = new int[x, y];
        //0'lar ile doldur
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                grid[i, j] = 0;
            }
        }
        size = x;
    }

    public  int[,] GetGrid()
    {
        return grid;
    }

    public void SetGrid(int x, int y, int value)
    {
        Debug.Log("önceki x: " + x + " önceki y: " + y + " önceki value: " + value);
        Debug.Log(size);
        x += size / 2;
        y += size / 2;
        Debug.Log(x + " " + y + " " + value);
        grid[y, x] = value;
    }

    public void SetGridSqaure(int x1, int y1, int x2, int y2, int value)
    {
        x1 += size / 2;
        y1 += size / 2;
        x2 += size / 2;
        y2 += size / 2;
        for (int i = x1; i < x2; i++)
        {
            for (int j = y1; j < y2; j++)
            {
                grid[j, i] = value;
            }
        }
    }


    public void printGrid()
    {
        for (int i = size-1; 0 <= i; i--)
        {

            string str = "";
            for (int j = 0; j < size; j++)
            {
                str += grid[i, j].ToString() + " ";
            }
            Debug.Log(str+"\n");
        }
    }
}