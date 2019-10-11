using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] blocks;
    // Start is called before the first frame update
    void Start()
    {
        NewSpawn();
    }

    public void NewSpawn() 
    {
        Instantiate(blocks[Random.Range(0, blocks.Length)], transform.position, Quaternion.identity);
     }
}
