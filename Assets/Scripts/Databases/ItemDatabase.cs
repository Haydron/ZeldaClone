using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "ZeldaClone/ItemDatabase", order = 1)]
public class ItemDatabase : ScriptableObject
{
    public List<ItemData> Data;

    public ItemData FindItem(ItemType itemType)
    {
        for(int i = 0; i < Data.Count; i++)
        {
            if (Data[i].Type == itemType)
            {
                return Data[i];
            }
        }

        return null;

    }
}

[System.Serializable]
public class ItemData
{
    public enum PickUpAnimation
    {
        None,
        OneHanded,
        TwoHanded,
    }

    public enum EquipPosition
    {
        NotEquipable,
        SwordHand,
        ShieldHand
    }
    public ItemType Type;
    public GameObject Prefab;
    public EquipPosition IsEquipable;
    public PickUpAnimation pickUpAnimation;
}
