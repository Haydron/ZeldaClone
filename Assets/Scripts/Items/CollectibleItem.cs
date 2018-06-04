using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour {

    public ItemType ItemType;
    public int Amount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterInventoryModel inventoryModel = collision.gameObject.GetComponent<CharacterInventoryModel>();
        if (inventoryModel != null)
        {
            inventoryModel.AddItem(ItemType, Amount);
            Debug.Log("Added " + Amount + " " + ItemType + "!");
            Destroy(this.gameObject);
        }
    }
}
