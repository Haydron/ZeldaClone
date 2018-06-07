using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class CharacterInteractionModel : MonoBehaviour {

    private Character m_Character;
    private CharacterMovementModel m_MovementModel;
    private InteractablePickUp m_PickedUpObject;

    void Awake()
    {
        m_Character = GetComponent<Character>();
        m_MovementModel = GetComponent<CharacterMovementModel>();
    }

    public void OnInteract()
    {
        if (IsCarryingObject())
        {
            ThrowCarryingObject();
        }
        InteractableBase usableInteractable = FindUsableInteractable();
        if (usableInteractable == null)
        {
            return;
        }
        usableInteractable.OnInteract(m_Character);

    }

    public void PickUpObject(InteractablePickUp pickUpObject)
    {
        m_PickedUpObject = pickUpObject;
        m_MovementModel.SetInterating(true);
        if (m_PickedUpObject == null)
        {
            Debug.LogWarning("pickup object not set!");
            return;
        }
        m_PickedUpObject.transform.parent = m_MovementModel.PickUpItemParent;
        m_PickedUpObject.transform.localPosition = Vector3.zero;
        m_MovementModel.SetFrozen(false, false, false);
        Helper.SetSortingLayerForAllRenderers(m_PickedUpObject.transform, "Foreground");
    }

    public bool IsCarryingObject()
    {
        return m_PickedUpObject != null;
    }

    public void ThrowCarryingObject()
    {
        m_PickedUpObject.Throw(m_Character);
        m_MovementModel.SetInterating(false);
        m_PickedUpObject = null;
        m_MovementModel.SetFrozen(false, false, false);
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
