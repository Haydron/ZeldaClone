using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoteTrigger : MonoBehaviour {

    Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            animator.SetBool("IsShowing", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            animator.SetBool("IsShowing", false);
        }
    }
}
