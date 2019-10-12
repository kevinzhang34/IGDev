using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMove : MonoBehaviour
{

    public AudioSource cleanSf;
    public AudioSource clickSf;
    public AudioSource rotateSf;
    public static int height = 20;
    public static int width = 10;
    public Vector3 rPoint;
    private float previousTime;
    private static Transform[,] grid = new Transform[width, height];
    public float fallingTime = 0.7f;
    // Start is called before the first frame update
    void Start()
    {
        cleanSf = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            clickSf.Play();
            transform.position += new Vector3(1, 0, 0);
            if (!checkBoundary())
                transform.position -= new Vector3(1, 0, 0);
        }
         else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            clickSf.Play();
            transform.position += new Vector3(-1, 0, 0);
            if (!checkBoundary())
                transform.position -= new Vector3(-1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.Space)) 
        {
            rotateSf.Play();
            transform.RotateAround(transform.TransformPoint(rPoint), new Vector3(0, 0, 1), 90);
            if (!checkBoundary())
                transform.RotateAround(transform.TransformPoint(rPoint), new Vector3(0, 0, 1), -90);
          }

        //where the block floating up to the top 
        if (Time.time - previousTime > (Input.GetKey(KeyCode.UpArrow) ? fallingTime / 10 : fallingTime))
        {
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

    bool HasLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            if (grid[j, i] == null)
                return false;
        }

        return true;
    }

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

    void CleanLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
    }
    void AddToGrid() 
        {
            foreach (Transform children in transform) 
            {
                int roundedX = Mathf.RoundToInt(children.transform.position.x);
                int roundedY = Mathf.RoundToInt(children.transform.position.y);
            grid[roundedX, roundedY] = children;
            }
        }

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
            if (grid[roundedX, roundedY] != null)
                return false;
            }
            return true;
        }

    }
