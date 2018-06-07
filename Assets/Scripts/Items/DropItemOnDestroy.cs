using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemOnDestroy : MonoBehaviour {
    public ItemType DropItemType;
    bool LootDropped = false;
    public int Amount;
    [Range(0f,1f)]
    public float Probability=1;

    void OnLootDrop()
    {
        float randomValue = Random.Range(0f, 1f);

        if (randomValue > Probability)
        {
            return;
        }

        ItemData data = DataBase.Item.FindItem(DropItemType);
        if (data == null || data.Prefab == null)
        {
            Debug.LogWarning("No Item Data Found or The Item Data doesn't have a prefab");
            return;
        }
        if (!LootDropped) {
            GameObject go = (GameObject)Instantiate(data.Prefab, transform.position, Quaternion.identity);
        }
    }
}
