using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Change : MonoBehaviour
{
    //loading scene from gameover to start menu
public void changetoscene(int changeTheScene) 
{
        SceneManager.LoadScene(changeTheScene);  
  }
}
