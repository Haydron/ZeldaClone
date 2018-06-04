using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationListener : MonoBehaviour {

    public CharacterMovementModel MovementModel;
    public CharacterMovementView MovementView;

	
	public void OnAttackStarted (AnimationEvent animationEvent)
    {
	    if(MovementModel != null)
        {
            MovementModel.OnAttackStarted();
        }

        if (MovementView != null)
        {
            MovementView.OnAttackStarted();
        }

        ShowWeapon();
        SetSortingOrderOfWeapon(animationEvent.intParameter);
        SetShieldDirection(animationEvent.stringParameter);
    }

    public void OnAttackFinished()
    {
        if (MovementModel != null)
        {
            MovementModel.OnAttackFinished();
        }

        if (MovementView != null)
        {
            MovementView.OnAttackFinished();
            MovementView.ReleaseShieldDirection();
        }

        HideWeapon();
    }

    public void HideWeapon()
    {
        if (MovementModel != null)
        {
            MovementView.HideWeapon();
        }

        if (MovementView != null)
        {
            MovementView.HideWeapon();
        }
      

    }
    public void ShowWeapon()
    {
        if (MovementModel != null)
        {
            MovementView.ShowWeapon();
        }

        if (MovementView != null)
        {
            MovementView.ShowWeapon();
        }
    }
    public void SetSortingOrderOfWeapon(int sortingOrder)
    {
        MovementView.SetSortingOrderOfWeapon(sortingOrder);
    }
    public void SetSortingOrderOfShield(int sortingOrder)
    {
        MovementView.SetSortingOrderOfShield(sortingOrder);
    }

    public void SetShieldDirection(string direction)
    {
        switch (direction)
        {
            case "FrontHalf":
                MovementView.ForceShieldDirection(CharacterMovementView.ShieldDirection.FrontHalf);
                break;
            case "BackHalf":
                MovementView.ForceShieldDirection(CharacterMovementView.ShieldDirection.BackHalf);
                break;
            case "Back":
                MovementView.ForceShieldDirection(CharacterMovementView.ShieldDirection.Back);
                break;
            case "Front":
                MovementView.ForceShieldDirection(CharacterMovementView.ShieldDirection.Front);
                break;
            case "Right":
                MovementView.ForceShieldDirection(CharacterMovementView.ShieldDirection.Right);
                break;
            case "Left":
                MovementView.ForceShieldDirection(CharacterMovementView.ShieldDirection.Left);
                break;
        }
    }
}
