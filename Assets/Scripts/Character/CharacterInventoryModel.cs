using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventoryModel : MonoBehaviour
{
    CharacterMovementModel m_MovementModel;
    Dictionary<ItemType, int> m_Items = new Dictionary<ItemType, int>();
	// Use this for initialization
	void Awake () {
        m_MovementModel = GetComponent<CharacterMovementModel>();
    }
	
    public void AddItem(ItemType itemType)
    {
        AddItem(itemType, 1);
    }
    public int GetItemCount(ItemType item)
    {
        if(m_Items.ContainsKey(item) == false)
        {
            return 0;
        }
        return m_Items[item];
    }

    public void AddItem(ItemType itemType, int amount)
    {
        if (m_Items.ContainsKey(itemType))
        {
            m_Items[itemType] += amount;
        }
        else
        {
            m_Items.Add(itemType, amount);
        }

        if(amount > 0)
        {
            ItemData itemData = DataBase.Item.FindItem(itemType);
            
            if(itemData != null)
            {
                if(itemData.pickUpAnimation != ItemData.PickUpAnimation.None)
                {
                    m_MovementModel.ShowItemPickup(itemType);
                }
                if (itemData.IsEquipable== ItemData.EquipPosition.SwordHand)
                {
                    m_MovementModel.EquipWeapon(itemType);
                }
                if(itemData.IsEquipable == ItemData.EquipPosition.ShieldHand)
                {
                    m_MovementModel.EquipShield(itemType);
                }
            }
        }

        Debug.Log(amount + " " + itemType + " Added!");
    }
}
