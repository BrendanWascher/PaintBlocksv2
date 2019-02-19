using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    [SerializeField]
    private Text timeText;

    private int size;

    private void Start()
    {
        size = timeText.fontSize;
    }

    // Update is called once per frame
    void Update ()
    {
        UpddateTimer();
	}

    private void UpddateTimer()
    {
        if (GameTimer.timeRemaining >= 10)
            timeText.text = Math.Round((GameTimer.timeRemaining), 0).ToString();
        else
        {
            //ChangeClockSize();
            timeText.text = Math.Round((GameTimer.timeRemaining), 1).ToString();
        }
    }


    private void ChangeClockSize()
    {
        int cT = Mathf.RoundToInt(GameTimer.timeRemaining);
        if(cT == 10 || cT == 9 || cT == 8 || cT == 7)
        {
            timeText.fontSize = size + 30;
        }
        else
        {
            timeText.fontSize = size;
        }
    }

    private void CheckLastSeconds()
    {
        int check = Mathf.RoundToInt(GameTimer.timeRemaining);
        switch(check)
        {
            case 1:

                break;

            case 2:

                break;

            case 3:

                break;

            case 4:

                break;

            case 5:

                break;

            case 6:

                break;

            case 7:

                break;

            case 8:

                break;

            case 9:

                break;

            case 10:

                break;
        }
       
    }
}
