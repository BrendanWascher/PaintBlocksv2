using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCube : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }


    public void RotateToAudioPanel(bool isTo)
    {
        if (isTo)
        {
            animator.SetBool("isGoingToAudio", true);
            //this.gameObject.transform.Rotate(0f, 90f, 0f);
        }
        else
        {
            animator.SetBool("isGoingToAudio", false);
            //this.gameObject.transform.Rotate(0f, -90f, 0f);
        }
    }

    public void RotateToCreditsPanel(bool isTo)
    {
        if (isTo)
        {
            animator.SetBool("isGoingToCredits", true);
            //this.gameObject.transform.Rotate(0f, -90f, 0f);
        }
        else
        {
            animator.SetBool("isGoingToCredits", false);
            //this.gameObject.transform.Rotate(0f, 90f, 0f);
        }
    }

    public void RotateToStartPanel(bool isTo)
    {
        if (isTo)
        {
            animator.SetBool("isGoingToStart", true);
            //this.gameObject.transform.Rotate(-90f, 0f, 0f);
        }
        else
        {
            animator.SetBool("isGoingToStart", false);
            //this.gameObject.transform.Rotate(90f, 0f, 0f);
        }
    }

    public void RotateToQuitPanel(bool isTo)
    {
        if (isTo)
        {
            animator.SetBool("isGoingToQuit", true);
            //this.gameObject.transform.Rotate(90f, 0f, 0f);
        }
        else
        {
            animator.SetBool("isGoingToQuit", false);
            //this.gameObject.transform.Rotate(-90f, 0f, 0f);
        }
    }

    public void RotateToReadyUpPanel(bool isTo)
    {
        if(isTo)
        {
            animator.SetBool("isGoingToReady", true);
            //this.gameObject.transform.Rotate(-90f, 0f, 0f);
        }
        else
        {
            animator.SetBool("isGoingToReady", false);
            //this.gameObject.transform.Rotate(90f, 0f, 0f);
        }
    }
}
