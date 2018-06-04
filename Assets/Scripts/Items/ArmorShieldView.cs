using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorShieldView : MonoBehaviour {
    public Sprite VisualsFront;
    public Sprite VisualsSide;
    public Sprite VisualsBack;
    public Sprite VisualsFrontHalf;
    public Sprite VisualsBackHalf;

    SpriteRenderer m_Renderer;
    CharacterMovementView m_MovementView;

    bool m_IsDirectionForced = false;
    CharacterMovementView.ShieldDirection m_ForcedDirection;

    void Start () {
        m_Renderer = GetComponentInChildren<SpriteRenderer>();
        m_MovementView = GetComponentInParent<CharacterMovementView>();
	}
	

	void Update () {
		if(m_IsDirectionForced == true)
        {
            SetShieldDirection(m_ForcedDirection);
            return;
        }

        float facingDirectionX = m_MovementView.Animator.GetFloat("DirectionX");
        float facingDirectionY = m_MovementView.Animator.GetFloat("DirectionY");

        if(facingDirectionX == -1)
        {
            SetShieldDirection(CharacterMovementView.ShieldDirection.Left);
        }
        else if(facingDirectionX == 1)
        {
            SetShieldDirection(CharacterMovementView.ShieldDirection.Right);
        }
        else if(facingDirectionY == -1)
        {
            SetShieldDirection(CharacterMovementView.ShieldDirection.Front);
        }
        else if(facingDirectionY == 1)
        {
            SetShieldDirection(CharacterMovementView.ShieldDirection.Back);
        }
    }

    public void ReleaseShieldDirection() {
        m_IsDirectionForced = false;
    }

    public void ForceShieldDirection(CharacterMovementView.ShieldDirection direction)
    {
        m_IsDirectionForced = true;
        m_ForcedDirection = direction;
    }

    void SetShieldDirection(CharacterMovementView.ShieldDirection direction) {
        transform.localScale = Vector3.one;

        switch (direction)
        {
            case CharacterMovementView.ShieldDirection.Front:
                m_Renderer.sprite = VisualsFront;
                m_MovementView.SetSortingOrderOfShield(150);
                break;
            case CharacterMovementView.ShieldDirection.Back:
                m_Renderer.sprite = VisualsBack;
                m_MovementView.SetSortingOrderOfShield(50);
                break;
            case CharacterMovementView.ShieldDirection.Left:
                transform.localScale = new Vector3(-1, 1, 1);
                m_Renderer.sprite = VisualsSide;
                m_MovementView.SetSortingOrderOfShield(50);
                break;
            case CharacterMovementView.ShieldDirection.Right:
                m_Renderer.sprite = VisualsSide;
                m_MovementView.SetSortingOrderOfShield(150);
                break;
            case CharacterMovementView.ShieldDirection.FrontHalf:
                m_Renderer.sprite = VisualsFrontHalf;
                m_MovementView.SetSortingOrderOfShield(150);
                break;
            case CharacterMovementView.ShieldDirection.BackHalf:
                m_Renderer.sprite = VisualsBackHalf;
                break;
        }
    }
}
