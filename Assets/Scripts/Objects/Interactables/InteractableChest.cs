using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableChest : InteractableBase
{
    public Sprite OpenChestSprite;
    public ItemType ItemInChest;
    public int ItemAmount;
    bool isOpened = false;
    bool Interacted = false;
    public string chestText;

    public override void OnInteract(Character character)
    {
        if (!isOpened)
        {
            isOpened = true;
            character.InventoryModel.AddItem(ItemInChest, ItemAmount);
            if (OpenChestSprite != null)
            {
                GetComponentInChildren<SpriteRenderer>().sprite = OpenChestSprite;
            }
            else
            {
                GetComponentInChildren<SpriteRenderer>().sprite = null;
                Debug.LogWarning("No open chest sprite found!");        
            }

            if (!Interacted)
            {
                DialogueBox.Show(chestText);
                character.MovementModel.SetFrozen(true, true);
            }
        }
        if (!Interacted)
        {
                Interacted = true;
        }
        else
        {
            DialogueBox.Hide();
            Interacted = false;
            character.MovementModel.SetFrozen(false, true);
        }
    }
}
