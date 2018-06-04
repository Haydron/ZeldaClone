using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class CharacterInteractionModel : MonoBehaviour {

    private Character m_Character;
    private CharacterMovementModel m_MovementModel;

    void Awake()
    {
        m_Character = GetComponent<Character>();
        m_MovementModel = GetComponent<CharacterMovementModel>();
    }

    public void OnInteract()
    {
        InteractableBase usableInteractable = FindUsableInteractable();
        if (usableInteractable == null)
        {
            return;
        }
        usableInteractable.OnInteract(m_Character);

    }

    InteractableBase FindUsableInteractable()
    {
        Collider2D[] closeColliders = Physics2D.OverlapCircleAll(transform.position, 0.8f);
        InteractableBase clostestInteractable = null;
        float angleToclosestInteractable = Mathf.Infinity;
        for (int i = 0; i < closeColliders.Length; i++)
        {
            InteractableBase colliderInteractable = closeColliders[i].GetComponent<InteractableBase>();
            if(colliderInteractable == null)
            {
                continue;
            }
            Vector3 directionToInteractable = closeColliders[i].transform.position - transform.position;


            float angleToInteractable = Vector3.Angle(m_MovementModel.GetFacingDirection(), directionToInteractable);
            if(angleToInteractable < 45)
            {
                if(angleToInteractable < angleToclosestInteractable)
                {
                    clostestInteractable = colliderInteractable;
                }
            }
        }
        return clostestInteractable;
    }
}
