using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerSound : MonoBehaviour
{
    [SerializeField]
    private AudioSource timerPlay;

    [SerializeField]
    private AudioClip timerTick;

    [SerializeField]
    private AudioClip timerBuzz;

    private int checkChanged;
	
	// Update is called once per frame
	void Update ()
    {
        CheckLastSeconds();
	}

    private void CheckLastSeconds()
    {
        int check = Mathf.CeilToInt(GameTimer.timeRemaining);
        switch (check)
        {
            case 0:
                if (check != checkChanged)
                {
                    checkChanged = check;
                    timerPlay.clip = timerBuzz;
                    timerPlay.Play();
                }
                break;

            case 1:
                if(check != checkChanged)
                {
                    checkChanged = check;
                    timerPlay.clip = timerTick;
                    timerPlay.Play();
                }
                break;

            case 2:
                if (check != checkChanged)
                {
                    checkChanged = check;
                    timerPlay.clip = timerTick;
                    timerPlay.Play();
                }
                break;

            case 3:
                if (check != checkChanged)
                {
                    checkChanged = check;
                    timerPlay.clip = timerTick;
                    timerPlay.Play();
                }
                break;

            case 4:
                if (check != checkChanged)
                {
                    checkChanged = check;
                    timerPlay.clip = timerTick;
                    timerPlay.Play();
                }
                break;

            case 5:
                if (check != checkChanged)
                {
                    checkChanged = check;
                    timerPlay.clip = timerTick;
                    timerPlay.Play();
                }
                break;

            case 6:
                if (check != checkChanged)
                {
                    checkChanged = check;
                    timerPlay.clip = timerTick;
                    timerPlay.Play();
                }
                break;

            case 7:
                if (check != checkChanged)
                {
                    checkChanged = check;
                    timerPlay.clip = timerTick;
                    timerPlay.Play();
                }
                break;

            case 8:
                if (check != checkChanged)
                {
                    checkChanged = check;
                    timerPlay.clip = timerTick;
                    timerPlay.Play();
                }
                break;

            case 9:
                if (check != checkChanged)
                {
                    checkChanged = check;
                    timerPlay.clip = timerTick;
                    timerPlay.Play();
                }
                break;

            case 10:
                if (check != checkChanged)
                {
                    checkChanged = check;
                    timerPlay.clip = timerTick;
                    timerPlay.Play();
                }
                break;

            default:
                break;
        }

    }
}
