using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] blocks;
    //array contain all the blocks
    // Start is called before the first frame update
    void Start()
    {
        NewSpawn();
    }
    //spawn new block at the position of the spawner.
    public void NewSpawn() 
    {
        Instantiate(blocks[Random.Range(0, blocks.Length)], transform.position, Quaternion.identity);
     }
}
