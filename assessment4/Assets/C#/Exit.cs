﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    //exit button
    public void exitGame() {
        Debug.Log("The game has quit");
        Application.Quit();
      }
}
