using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePickUp : InteractableBase {

    Vector3 m_CharacterThrowPosition;
    Vector3 m_ThrowDirection;
    public GameObject DestroyEffect;
    public float ThrowDistance;
    public float ThrowSpeed;
    bool isThrown;
    Collider2D m_Collider;
    private void Awake()
    {
        m_Collider = GetComponent<Collider2D>();
    }
    public override void OnInteract( Character character)
    {
        CharacterInteractionModel InteractionModel = character.GetComponent<CharacterInteractionModel>();
        if(InteractionModel == null)
        {
            Debug.LogWarning("No Advanced Movemodel found on Interact!");
            return;
        }

        BroadcastMessage("OnPickupObject", character, SendMessageOptions.DontRequireReceiver);
        character.InteractionModel.PickUpObject(this);
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach(Collider2D col in colliders)
        {
            if (col.isTrigger == false)
            {
                col.enabled = false;
            }
        } 


    }

    public void Throw(Character character)
    {
        transform.parent = null;
        m_CharacterThrowPosition = character.transform.position;
        m_ThrowDirection = character.MovementModel.GetFacingDirection();
        isThrown = true;
        StartCoroutine(ThrowRoutine(m_CharacterThrowPosition, m_ThrowDirection));
    }
    
    IEnumerator ThrowRoutine(Vector3 characterThrowPos, Vector3 throwDir)
    {
        Vector3 targetPos = characterThrowPos + throwDir.normalized * ThrowDistance;
        while(transform.position != targetPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, ThrowSpeed*Time.deltaTime);
            yield return null;
        }
        BroadcastMessage("DestroyObject", SendMessageOptions.DontRequireReceiver);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (isThrown)
        {
            AttackableBase attackable = col.GetComponent<AttackableBase>();
            if(attackable != null)
            {
                attackable.OnHit(m_Collider);
            }
        }
    }

    public void DestroyObject()
    {
        BroadcastMessage("OnLootDrop", SendMessageOptions.DontRequireReceiver);
        if (DestroyEffect != null)
        {
            GameObject destroyEffect = (GameObject)Instantiate(DestroyEffect);
            destroyEffect.transform.position = transform.position;
        }
        Destroy(this.gameObject);
    }
}
