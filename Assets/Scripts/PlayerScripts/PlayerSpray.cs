using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpray : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;

    [SerializeField]
    private GameObject paintSpray;

    [SerializeField]
    private Renderer paintRenderer;

    [SerializeField]
    private Rigidbody paintBlock;

    [SerializeField]
    private float coolDown = 5f;

    [SerializeField]
    private float movementSpeedTimer = 1f;

    [SerializeField]
    private ParticleSystem spray;

    private ParticleSystemRenderer sprayColor;

    public static bool isPaused = false;

    private string lastDirectionMoved;
    private bool isOnCoolDown = false;
    private int playerNumber;
    private float timer;
    private float movementTimer;
    private bool isFirstMove, isSecondMove, isThirdmove, isFourthMove = true;

	void Start ()
    {
        playerNumber = player.playerNumber;
        sprayColor = GetComponent<ParticleSystemRenderer>();
        sprayColor.material = player.playerMaterial;
	}
	
	void Update ()
    {
        lastDirectionMoved = player.lastDirectionMoved;
        CheckInput();
	}

    private void CheckInput()
    {
        //Debug.Log(isOnCoolDown);

        if(Input.GetButtonDown("Fire"+playerNumber) && !isPaused)
        {
            if(!isOnCoolDown)
            {
                if (lastDirectionMoved == "Right")
                    SprayRight();
                else if (lastDirectionMoved == "Left")
                    SprayLeft();
                else if (lastDirectionMoved == "Up")
                    SprayUp();
                else if (lastDirectionMoved == "Down")
                    SprayDown();
                else
                    SprayDown();

                isOnCoolDown = true;
                StartCoroutine(CoolDown());
            }
        }
    }

    private IEnumerator CoolDown()
    {
        timer = 0f;

        while(!CoolDownTimer())
        {
            yield return null;
        }

        //Debug.Log("No longer on cooldown");
        isOnCoolDown = false;
    }

    private IEnumerator MoveSpray(float d1, float d2, float d3, 
        float r1, float r2, float r3)
    {
        movementTimer = 0f;

        while(!MovementTimer(d1/4, d2/4, d3/4))
        {
            paintRenderer.material = player.playerMaterial;
            yield return null;
        }

        paintRenderer.transform.Translate(-d1, -d2, -d3);
        paintRenderer.transform.Rotate(-r1, -r2, -r3);
        isFirstMove = true;
        isSecondMove = true;
        isThirdmove = true;
        isFourthMove = true;
    }

    private bool CoolDownTimer()
    {
        timer += Time.deltaTime;
        if(timer<coolDown)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private bool MovementTimer(float d1, float d2, float d3)
    {
        movementTimer += Time.deltaTime;
        if (movementTimer < movementSpeedTimer)
        {
            if (movementTimer > ((3 / 4) * movementSpeedTimer) && isFirstMove)
            {
                paintRenderer.transform.Translate(d1, d2, d3);
                isFirstMove = false;
                return false;
            }
            else if (movementTimer > ((1 / 2) * movementTimer) && isSecondMove)
            {
                paintRenderer.transform.Translate(d1, d2, d3);
                isSecondMove = false;
                return false;
            }
            else if(movementTimer > ((1/4) * movementTimer) && isThirdmove)
            {
                paintRenderer.transform.Translate(d1, d2, d3);
                isThirdmove = false;
                return false;
            }
            else if(movementTimer < ((1/4)*movementTimer) && isFourthMove)
            {
                paintRenderer.transform.Translate(d1, d2, d3);
                isFourthMove = false;
                return false;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return true;
        }
    }

    private void SprayRight()
    {
        // don't rotate object
        // move object forward
        Debug.Log("Sprayed towards the " + lastDirectionMoved);
        spray.Play();

        StartCoroutine(MoveSpray(0f, 0f, 4f, 0f, 0f, 0f));

        /*
        paintRenderer.transform.Translate(0f, -.1f, 0f);
        for (int i = 0; i < 4; i++)
        {
            paintRenderer.transform.Translate(0f, 0f, 1f);
        }
        paintRenderer.transform.Translate(0f, .33f, 0f);
        paintRenderer.transform.Translate(0f, 0f, -4f);
        */

        // don't rotate object back
    }

    private void SprayLeft()
    {
        // rotate object 180 in y
        paintSpray.transform.Rotate(0, 180, 0);
        // move object forward
        Debug.Log("Sprayed towards the " + lastDirectionMoved);
        spray.Play();

        StartCoroutine(MoveSpray(0f,0f,4f,0f,180f,0f));

        /*
        paintRenderer.transform.Translate(0f, -.33f, 0f);
        for (int i = 0; i < 4; i++)
        {
            paintRenderer.transform.Translate(0f, 0f, 1f);
        }
        paintRenderer.transform.Translate(0f, .33f, 0f);
        paintRenderer.transform.Translate(0f, 0f, -4f);
        */

        // rotate object -180 in y
        //paintSpray.transform.Rotate(0, -180, 0);
    }

    private void SprayUp()
    {
        // rotate object -90 in y
        paintSpray.transform.Rotate(0, -90, 0);
        // move object forward
        Debug.Log("Sprayed towards the " + lastDirectionMoved);
        spray.Play();

        StartCoroutine(MoveSpray(0f,0f,4f,0f,-90f,0f));

        /*
        paintRenderer.transform.Translate(0f, -.33f, 0f);
        for (int i = 0; i < 4; i++)
        {
            paintRenderer.transform.Translate(0f, 0f, 1f);
        }
        paintRenderer.transform.Translate(0f, .33f, 0f);
        paintRenderer.transform.Translate(0f, 0f, -4f);
        */

        // rotate object 90 in y
        //paintSpray.transform.Rotate(0, 90, 0, Space.World);
    }

    private void SprayDown()
    {
        // rotate object 90 in y
        paintSpray.transform.Rotate(0, 90, 0);
        // move object forward
        Debug.Log("Sprayed towards the " + lastDirectionMoved);
        spray.Play();

        StartCoroutine(MoveSpray(0f,0f,4f,0f,90f,0f));

        /*
        paintRenderer.transform.Translate(0f, -.33f, 0f);
        for (int i = 0; i < 4; i++)
        {
            paintRenderer.transform.Translate(0f, 0f, 1f);
        }
        paintRenderer.transform.Translate(0f, .33f, 0f);
        paintRenderer.transform.Translate(0f, 0f, -4f);
        */

        // rotate object -90 in y
        //paintSpray.transform.Rotate(0, -90, 0);
    }

    // make spray IEnumerator
    // moves too fast 
}
