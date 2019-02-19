using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerReadyUp : MonoBehaviour
{
    [SerializeField]
    private int playerNumber = 1;

    [SerializeField]
    private Image fillImage;

    [SerializeField]
    private Text readyUpText;

    [SerializeField]
    private float readyUpTimer = 3f;

    [SerializeField]
    private AudioSource readySound;

    private float timer;
    private bool hasPlayed = false;

    public bool isReady = false;
	
	// Update is called once per frame
	void Update ()
    {
        CheckInput();
	}

    void CheckInput()
    {
        if (!isReady)
        {
            if (Input.GetButton("Fire" + playerNumber))
            {
                if (timer < readyUpTimer)
                {
                    timer += Time.deltaTime;
                    fillImage.fillAmount = (timer / readyUpTimer);
                    readyUpText.text = "Hold...";
                    if(!hasPlayed)
                    {
                        readySound.Play();
                        hasPlayed = true;
                    }
                }
                else
                {
                    fillImage.fillAmount = 1;
                    readyUpText.text = "Ready!";
                    isReady = true;
                }
            }
            else
            {
                ResetThis();
            }
        }
    }

    public void ResetThis()
    {
        if(readySound.isPlaying)
        {
            readySound.Stop();
        }
        timer = 0;
        isReady = false;
        hasPlayed = false;
        fillImage.fillAmount = 0f;
        readyUpText.text = "Hold 'A' to Ready Up!";
    }
}
