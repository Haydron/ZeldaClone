using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteractionView : MonoBehaviour {

    public Animator Animator;
    CharacterInteractionModel m_InteractionModel;
    private void Awake()
    {
        m_InteractionModel = GetComponent<CharacterInteractionModel>();
    }
    void Update()
    {
        UpdateIsCarryingObject();
    }

    void UpdateIsCarryingObject()
    {
        Animator.SetBool("IsCarrying", m_InteractionModel.IsCarryingObject());
    }

}
