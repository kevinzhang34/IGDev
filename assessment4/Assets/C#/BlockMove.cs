using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMove : MonoBehaviour
{

    public static int height = 20;
    public static int width = 10;
    public Vector3 rPoint;
    private float previousTime;
    public float fallingTime = 0.7f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!checkBoundary())
                transform.position -= new Vector3(1, 0, 0);
        }
         else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!checkBoundary())
                transform.position -= new Vector3(-1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.Space)) 
        {
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
                this.enabled = false;
                FindObjectOfType<Spawner>().NewSpawn();
            }
            previousTime = Time.time;
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
            }
            return true;
        }

    }
