﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
   public void nextSence(string scenename) 
   {
        Application.LoadLevel(scenename);
     }
}
