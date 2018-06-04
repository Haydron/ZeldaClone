using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGamepadControl : CharacterBaseControl
{
    void Start()
    {

    }
    void Update ()
    {
        UpdateDirection();
        UpdateAction();
        UpdateAttack();
    }
    
    void UpdateAction()
    {
        if (Input.GetButtonDown("Interact"))
        {
            OnActionPressed();
        }
    }

    void UpdateAttack()
    {
        if (Input.GetButtonDown("Attack"))
        {
            OnAttackPressed();
        }
    }

    void UpdateDirection()
    {
        Vector2 newDirection = new Vector2(Input.GetAxisRaw("Horizontal") , Input.GetAxisRaw("Vertical"));
        float deadZone = 0.20f;

        if (Mathf.Abs(newDirection.x) < deadZone)
        {
            newDirection.x = 0;
        }
        else
        {
            newDirection.x = Mathf.Sign(newDirection.x);
        }

        if (Mathf.Abs(newDirection.y) < deadZone)
        {
            newDirection.y = 0;
        }
        else
        {
            newDirection.y = Mathf.Sign(newDirection.y);
        }

        SetDirection(newDirection);
    }
}
