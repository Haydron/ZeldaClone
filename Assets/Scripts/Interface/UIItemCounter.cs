using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItemCounter : MonoBehaviour {
    public CharacterInventoryModel Inventory;
    public ItemType ItemType;
    public string NumberFormat;
    Text m_Text;
	// Use this for initialization
	void Awake () {
        m_Text = GetComponentInChildren<Text>();
	}
	
	void Update () {
	    if(Inventory == null)
        {
            return;
        }

        m_Text.text = Inventory.GetItemCount(ItemType).ToString(NumberFormat);
	}
}
