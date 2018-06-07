using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementModel : MonoBehaviour {

    public float Speed;
    public Transform WeaponParent;
    public Transform ShieldParent;
    public Transform PickUpItemParent;

    private Vector3 m_FacingDirection;
    private Vector3 m_MovementDirection;
    private Rigidbody2D m_Body;
    bool m_IsFrozen = false;
    bool m_IsDirectionFrozen = false; 
    bool m_IsAttacking = false;
    bool m_IsInteracting = false;
    private ItemType m_PickingUpObject = ItemType.None;
    private ItemType m_EquippedWeapon = ItemType.None;
    private ItemType m_EquippedShield = ItemType.None;

    private GameObject m_PickUpItem;
    private Vector2 m_PushDirection;
    private float m_PushTime;

    int m_LastDirectionFrameCount;

	// Use this for initialization
	void Awake ()
    {
        m_Body = GetComponent<Rigidbody2D>();	
	}

    void Update()
    {
        UpdatePushTime();
    }


    void UpdatePushTime()
    {
        m_PushTime = Mathf.MoveTowards(m_PushTime, 0f, Time.deltaTime);
    }
    // Update is called once per frame
    void FixedUpdate () {
        UpdateMovement();
	}

    void UpdateMovement()
    {
        if (m_IsFrozen)
        {
            m_Body.velocity = Vector2.zero;
            return;
        }
        if(m_MovementDirection != Vector3.zero)
        {
            m_MovementDirection.Normalize();
        }
        if (IsBeingPushed())
        {
            m_Body.velocity = m_PushDirection;
        }
        else
        {
            m_Body.velocity = m_MovementDirection * Speed;
        }

        
    }

    public void PushCharacter(Vector2 pushDirection, float time)
    {
        if (m_IsAttacking)
        {
            GetComponentInChildren<CharacterAnimationListener>().OnAttackFinished();
        }
            
        m_PushDirection = pushDirection;
        m_PushTime = time;
    }


    public bool CanAttack()
    {
        if (!m_IsAttacking && m_EquippedWeapon != ItemType.None && !m_IsFrozen && IsBeingPushed()== false && !m_IsInteracting)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void DoAttack()
    {
    }

    public void EquipWeapon(ItemType itemType)
    {
        EquipItem(itemType,ItemData.EquipPosition.SwordHand,WeaponParent,ref m_EquippedWeapon);
    }

    public void EquipShield(ItemType itemType)
    {
        EquipItem(itemType, ItemData.EquipPosition.ShieldHand, ShieldParent, ref m_EquippedShield);
    }

    void EquipItem(ItemType itemType, ItemData.EquipPosition equipPostion, Transform itemParent, ref ItemType equippedItemSlot)
    {
        if (itemParent == null)
        {
            return;
        }
        ItemData itemData = DataBase.Item.FindItem(itemType);

        if(itemData == null)
        {
            return;
        }
        if(itemData.IsEquipable != equipPostion)
        {
            return;
        }

        equippedItemSlot = itemType;

        GameObject go = (GameObject)Instantiate(itemData.Prefab);

        go.transform.parent = itemParent;
        go.transform.localPosition = Vector2.zero;
        go.transform.localRotation = Quaternion.identity;
    }

    public void ShowItemPickup(ItemType itemType)
    {
        if (PickUpItemParent == null)
        {
            return;
        }
        ItemData itemData = DataBase.Item.FindItem(itemType);

        if(itemData == null)
        {
            return;
        }

        SetFrozen(true,true, true);
        m_PickingUpObject = itemType;

        m_PickUpItem = (GameObject)Instantiate(itemData.Prefab);
        m_PickUpItem.transform.parent = PickUpItemParent;
        m_PickUpItem.transform.localPosition = Vector2.zero;
        m_PickUpItem.transform.localRotation = Quaternion.identity;
    }

    public ItemType GetItemThatIsBeingPickedUp()
    {
        return m_PickingUpObject;
    }

    public bool IsFrozen()
    {
        return m_IsFrozen;
    }

    public bool IsBeingPushed()
    {
        return m_PushTime > 0;
    }
    public void SetInterating(bool isInteracting)
    {
        m_IsInteracting = isInteracting;
    }

    public void SetFrozen(bool isFrozen,bool freezeDirection, bool affectGameTime)
    {
        m_IsDirectionFrozen = freezeDirection;
        m_IsFrozen = isFrozen;
        if (affectGameTime)
        {
            if (isFrozen)
            {
                //Time.timeScale = 0;
            }
            else
            {
                //Time.timeScale = 1;
            }
        }
    }

    public void SetDirection(Vector2 direction)
    {
        if (direction != Vector2.zero && GetItemThatIsBeingPickedUp() != ItemType.None)
        {
            m_PickingUpObject = ItemType.None;
            Destroy(m_PickUpItem);
            SetFrozen(false,false,false);
        }
        if (m_IsDirectionFrozen)
        {
            return;
        }

        if (IsBeingPushed()){
            m_MovementDirection = m_PushDirection;
            return;
        }

        if (Time.frameCount == m_LastDirectionFrameCount)
        {
            return;
        }

        m_MovementDirection = new Vector3(direction.x, direction.y, 0);

        if(direction != Vector2.zero){
            m_FacingDirection = m_MovementDirection;
            m_LastDirectionFrameCount = Time.frameCount;
        }


    }

    public Vector3 GetDirection()
    {
        return m_MovementDirection;
    }

    public Vector3 GetFacingDirection()
    {
        return m_FacingDirection;
    }

    public bool IsMoving()
    {
        if (m_IsFrozen)
        {
            return false;
        }
        return m_MovementDirection != Vector3.zero;
    }

    public void OnAttackStarted()
    {
        m_IsAttacking = true;
        SetFrozen(true,true,false);
    }

    public void OnAttackFinished()
    {
        m_IsAttacking = false;
        SetFrozen(false,false,false);
    }

    public ItemType GetEquippedShield()
    {
        return m_EquippedShield;
    }

    public ItemType GetEquippedWeapon()
    {
        return m_EquippedWeapon;
    }
}
