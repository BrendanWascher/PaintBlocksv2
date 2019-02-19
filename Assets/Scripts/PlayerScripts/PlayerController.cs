using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float gridUpperXLimit = 10.5f;

    [SerializeField]
    private float gridUpperZLimit = 8.5f;

    [SerializeField]
    private float gridLowerXLimit = 1.5f;

    [SerializeField]
    private float gridLowerZLimit = -0.5f;

    [SerializeField]
    private GameObject playerGameObject;

    [SerializeField]
    private Animator animator;

    [HideInInspector]
    public Material playerMaterial;

    [HideInInspector]
    public GameObject camera;

    public float curXCoord = 2.5f;
    public float curZCoord = .5f;

    public int playerNumber = 1;

    public Renderer[] renderers;

    public string lastDirectionMoved = null;

    private bool isButtonPressed = false;

	void Start ()
    {
        animator = GetComponent<Animator>();

        SetPlayerColor();

        curXCoord = gameObject.transform.position.x;
        curZCoord = gameObject.transform.position.z;
    }
	
	void Update ()
    {
        GetInput();
	}

    public void SetPlayerColor()
    {
        renderers = playerGameObject.GetComponentsInChildren<Renderer>();
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material = playerMaterial;
        }
    }

    private void GetInput()
    {
        CheckPressed();
        CheckReleased();
        UpdateLocation();
    }

    private void CheckPressed()
    {
        if(!isButtonPressed)
        {
            if (Input.GetAxis("Horizontal" + playerNumber) > 0
                && curZCoord < gridUpperZLimit)
            {
                if (OverlapChecker.CheckOverlap(playerNumber-1, "Z", curZCoord + 1,curXCoord))
                {
                    isButtonPressed = true;
                    MoveRight();
                }
            }
            else if (Input.GetAxis("Horizontal" + playerNumber) < 0
                && curZCoord > gridLowerZLimit)
            {
                if (OverlapChecker.CheckOverlap(playerNumber-1, "Z", curZCoord - 1, curXCoord))
                {
                    isButtonPressed = true;
                    MoveLeft();
                }
            }
            else if(Input.GetAxis("Vertical" + playerNumber) > 0
                && curXCoord > gridLowerXLimit)
            {
                if (OverlapChecker.CheckOverlap(playerNumber-1, "X", curXCoord - 1, curZCoord))
                {
                    isButtonPressed = true;
                    MoveUp();
                }
            }
            else if(Input.GetAxis("Vertical" + playerNumber) < 0
                && curXCoord < gridUpperXLimit)
            {
                if (OverlapChecker.CheckOverlap(playerNumber-1, "X", curXCoord + 1, curZCoord))
                {
                    isButtonPressed = true;
                    MoveDown();
                }
            }
        }
    }

    private void CheckReleased()
    {
        if ((Input.GetAxis("Horizontal" +playerNumber)) == 0 && 
            (Input.GetAxis("Vertical"+playerNumber)) == 0)
        {
            isButtonPressed = false;
            /*
            animator.SetBool("isPlayerMovingRight", false);
            animator.SetBool("isPlayerMovingLeft", false);
            animator.SetBool("isPlayerMovingUp", false);
            animator.SetBool("isPlayerMovingDown", false);
            */
        }
    }

    private void UpdateLocation()
    {
        curXCoord = gameObject.transform.position.x;
        curZCoord = gameObject.transform.position.z;
    }

    private void MoveRight()
    {
        //animator.SetBool("isPlayerMovingRight", true);
        gameObject.transform.Translate(0f, 0f, 1f);
        playerGameObject.transform.Rotate(90f, 0f, 0f, Space.World);
        lastDirectionMoved = "Right";
        //curZCoord++;

        //Debug.Log("Z coord is " + curZCoord);
    }

    private void MoveLeft()
    {
        //animator.SetBool("isPlayerMovingLeft", true);
        gameObject.transform.Translate(0f, 0f, -1f);
        playerGameObject.transform.Rotate(-90f, 0f, 0f, Space.World);
        lastDirectionMoved = "Left";
        //curZCoord--;

        //Debug.Log("Z coord is " + curZCoord);
    }

    private void MoveDown()
    {
        //animator.SetBool("isPlayerMovingDown", true);
        gameObject.transform.Translate(1f, 0f, 0f);
        playerGameObject.transform.Rotate(0f, 0f, -90f, Space.World);
        lastDirectionMoved = "Down";
        //curXCoord++;

        //Debug.Log("X coord is " + curXCoord);
    }

    private void MoveUp()
    {
        //animator.SetBool("isPlayerMovingUp", true);
        gameObject.transform.Translate(-1f, 0f, 0f);
        playerGameObject.transform.Rotate(0f, 0f, 90f, Space.World);
        lastDirectionMoved = "Up";
        //curXCoord--;

        //Debug.Log("X coord is " + curXCoord);
    }
}
