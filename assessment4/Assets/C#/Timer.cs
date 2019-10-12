using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{
    public Text counterText;
    public float seconds, minutes;
   void Start()
    {
        counterText = GetComponent<Text>() as Text;
    }
    void Update() 
    {
        //minutes = (int)(Time.time / 60f);
        seconds = (int)(Time.time % 60f);
        counterText.text = "Scores:" + seconds.ToString("0000000");
      }

    //minutes.ToString("00") + ":" + 
    }

