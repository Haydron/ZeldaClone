using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementView : MonoBehaviour
{
    public Animator Animator;
    public Transform WeaponParent;
    public Transform ShieldParent;
    public Transform PickUpParent;
    private CharacterMovementModel m_MovementModel;

    // Use this for initialization
    void Awake()
    {
        m_MovementModel = GetComponent<CharacterMovementModel>();

        if (Animator == null)
        {
            Debug.LogError("Character Animator Not Setup!");
            enabled = false;
        }
        if (WeaponParent == null)
        {
            Debug.LogWarning("WeaponParent Not Setup!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDirection();
        UpdatePickingUpAnimation();
        UpdateHit();
        UpdateShield();

    }

    void UpdatePickingUpAnimation()
    {
        bool isPickingUpOneHanded = false;
        bool isPickingUpTwoHanded = false;

        if (m_MovementModel.IsFrozen())
        {
            ItemType pickupItem = m_MovementModel.GetItemThatIsBeingPickedUp();

            if(m_MovementModel.GetItemThatIsBeingPickedUp() != ItemType.None)
            {
                ItemData itemData = DataBase.Item.FindItem(pickupItem);
                switch (itemData.pickUpAnimation)
                {
                    case ItemData.PickUpAnimation.OneHanded:
                        isPickingUpOneHanded = true;
                        break;
                    case ItemData.PickUpAnimation.TwoHanded:
                        isPickingUpTwoHanded = true;
                        break;
                }
            }
            else
            {
                isPickingUpOneHanded = false;
                isPickingUpTwoHanded = false;
            }
        }

        Animator.SetBool("IsPickingUpOneHanded", isPickingUpOneHanded);
        Animator.SetBool("IsPickingUpTwoHanded", isPickingUpTwoHanded);
    }

    public void SetSortingOrderOfWeapon(int sortingOrder)
    {
        if (WeaponParent == null)
        {
            return;
        }

        SpriteRenderer[] sr = WeaponParent.GetComponentsInChildren<SpriteRenderer>();

        for (int i = 0; sr.Length > i; i++)
        {
            sr[i].sortingOrder = sortingOrder;
        }
    }

    public void SetSortingOrderOfPickupItem(int sortingOrder)
    {
        if (PickUpParent == null)
        {
            return;
        }

        SpriteRenderer[] sr = PickUpParent.GetComponentsInChildren<SpriteRenderer>();

        for (int i = 0; sr.Length > i; i++)
        {
            sr[i].sortingOrder = sortingOrder;
        }
    }

    public void SetSortingOrderOfShield(int sortingOrder){
            if (ShieldParent == null)
            {
                return;
            }

            SpriteRenderer[] sr = ShieldParent.GetComponentsInChildren<SpriteRenderer>();

            for (int i = 0; sr.Length > i; i++)
            {
                sr[i].sortingOrder = sortingOrder;
            }

    }

    void UpdateDirection()
    {
        Vector3 direction = m_MovementModel.GetDirection();

        if (direction != Vector3.zero)
        {
            Animator.SetFloat("DirectionX", direction.x);
            Animator.SetFloat("DirectionY", direction.y);

        }

        Animator.SetBool("IsMoving", m_MovementModel.IsMoving());
    }

    void UpdateHit()
    {
        Animator.SetBool("IsHit",m_MovementModel.IsBeingPushed());
    }

    public void DoAttack()
    {
        Animator.SetTrigger("DoAttack");
    }

    void SetActivateWeapon(bool doActivate)      
    {
        if(WeaponParent == null)
        {
            return;
        }
        WeaponParent.gameObject.SetActive(doActivate);        
    }

    void SetActiveShield(bool doActivate)
    {
        if (ShieldParent == null)
        {
            return;
        }
        ShieldParent.gameObject.SetActive(doActivate);
    }

    public void OnAttackStarted()
    {
        //SetActivateWeapon(true);
    }

    public void OnAttackFinished()
    {
        //SetActivateWeapon(false);
    }

    public void ShowWeapon()
    {
        SetActivateWeapon(true);
    }

    public void HideWeapon()
    {
        SetActivateWeapon(false);
    }

    public enum ShieldDirection
    {
        Left,Right,Front,Back,FrontHalf,BackHalf,
    }

    public void ForceShieldDirection(ShieldDirection direction)
    {
        ArmorShieldView shield = m_MovementModel.ShieldParent.GetComponentInChildren<ArmorShieldView>();

        if (shield == null)
        {
            return;
        }

        shield.ForceShieldDirection(direction);
    }

    public void ReleaseShieldDirection()
    {
        ArmorShieldView shield = m_MovementModel.ShieldParent.GetComponentInChildren<ArmorShieldView>();

        if (shield == null)
        {
            return;
        }

        shield.ReleaseShieldDirection();
    }

    void UpdateShield()
    {
        Animator.SetBool("HasShield", m_MovementModel.GetEquippedShield() != ItemType.None);
    }

}
