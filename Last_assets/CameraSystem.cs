using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSystem : MonoBehaviour
{
    public float moveSpeed;
    public Vector3 offset;
    public GameObject player;

    GridArray grid;

    public void Start()
    {
        grid = new GridArray();
    }
    
    private void Update()
    {
       if(grid.getHazir())
       {
            transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, moveSpeed * Time.deltaTime);
       }
    }
}
