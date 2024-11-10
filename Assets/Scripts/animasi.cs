using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animasi : MonoBehaviour
{
    public Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.S)){
            animator.SetBool("down", true);
        }
        else{
            animator.SetBool("down", false);
        }

        if(Input.GetKey(KeyCode.W)){
            animator.SetBool("up", true);
        }
        else{
            animator.SetBool("up", false);
        }

        if(Input.GetKey(KeyCode.D)){
            animator.SetBool("right", true);
        }
        else{
            animator.SetBool("right", false);
        }

        if(Input.GetKey(KeyCode.A)){
            animator.SetBool("left", true);
        }
        else{
            animator.SetBool("left", false);
        }
    }
}
