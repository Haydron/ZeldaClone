using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackableBush : AttackableBase
{
    public Sprite DestroyedSprite;
    private SpriteRenderer m_Renderer;
    public GameObject DestroyEffect;

    private void Awake()
    {
        m_Renderer = GetComponentInChildren<SpriteRenderer>();
    }

    public override void OnHit(Collider2D collider, ItemType item)
    {
        m_Renderer.sprite = DestroyedSprite;
        if(GetComponent<Collider2D>() != null)
        {
            GetComponent<Collider2D>().enabled = false;
        }

        if(DestroyEffect != null)
        {
            GameObject destroyEffect = (GameObject)Instantiate(DestroyEffect);
            destroyEffect.transform.position = transform.position;
        }
        BroadcastMessage("OnLootDrop",SendMessageOptions.DontRequireReceiver);

    }
}
