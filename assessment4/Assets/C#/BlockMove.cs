﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockMove : MonoBehaviour
{
    public AudioSource cleanSf;
    public AudioSource clickSf;
    public AudioSource rotateSf;
    public AudioSource stackSf;
    public static int height = 20;
    public static int width = 10;
    public Vector3 rPoint;
    private float previousTime;
    private static Transform[,] grid = new Transform[width, height];
    public float fallingTime = 0.7f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
         {
           
        }
        //check user input and move block to right
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            clickSf.Play();
            transform.position += new Vector3(1, 0, 0);
            if (!checkBoundary())
                transform.position -= new Vector3(1, 0, 0);
           
        }
        // check user input and move block to left
         else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            clickSf.Play();
            transform.position += new Vector3(-1, 0, 0);
            if (!checkBoundary())
                transform.position -= new Vector3(-1, 0, 0);
        }
        //rotate the block 90 degree
        else if (Input.GetKeyDown(KeyCode.Space)) 
        {
            rotateSf.Play();
            transform.RotateAround(transform.TransformPoint(rPoint), new Vector3(0, 0, 1), 90);
            if (!checkBoundary())
                transform.RotateAround(transform.TransformPoint(rPoint), new Vector3(0, 0, 1), -90);
          }

        //where the block falling down to the buttom 
        //if down key preeed falling time devide by 10 othewise reture full time
        if (Time.time - previousTime > (Input.GetKey(KeyCode.UpArrow) ? fallingTime / 10 : fallingTime))
        {
            clickSf.Play();
            transform.position += new Vector3(0, -1, 0);
             if (!checkBoundary())
            {
                transform.position -= new Vector3(0, -1, 0);
                AddToGrid();
                CheckLines();
                this.enabled = false;
                FindObjectOfType<Spawner>().NewSpawn();
            }
            previousTime = Time.time;
        }
        }
        
    void CheckLines()
    {
        for (int i = height - 1; i >= 0; i--)
        {
            if (HasLine(i))
            {
                CleanLine(i);
                cleanSf.Play();
                RowDown(i);
            }
        }
    }
    // check and loop all the line see if it is null
    bool HasLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            if (grid[j, i] == null)
                return false;
        }

        return true;
    }
    //when line is clean the block will drop down by 1 y.
    void RowDown(int i)
    {
        for (int y = i; y < height; y++)
        {
            for (int j = 0; j < width; j++)
            {
                if (grid[j, y] != null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }
     //delete all the element of the line. 
    void CleanLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
          
        }
    }
    // add existing block the the grid. 
    void AddToGrid() 
        {
            foreach (Transform children in transform) 
            {
                int roundedX = Mathf.RoundToInt(children.transform.position.x);
                int roundedY = Mathf.RoundToInt(children.transform.position.y);
            grid[roundedX, roundedY] = children;
            stackSf.Play();
            }
        }
  // check every block position if inside the grid, if doesn't return false,otherwise ture.
    bool checkBoundary()
        {
            foreach (Transform children in transform)
            {

                int roundedX = Mathf.RoundToInt(children.transform.position.x);
                int roundedY = Mathf.RoundToInt(children.transform.position.y);
                if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height)
                {
                    return false;
                }
                // check if the block is already been taken by other block.
            if (grid[roundedX, roundedY] != null)
                return false;
            }
            return true;
        }
}
