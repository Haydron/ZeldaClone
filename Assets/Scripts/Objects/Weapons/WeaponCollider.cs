using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class WeaponCollider : MonoBehaviour
{
    public ItemType Type;
    Collider2D m_Collider;

    private void Awake()
    {
        m_Collider = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AttackableBase attackable = collision.gameObject.GetComponent<AttackableBase>();
        if(attackable != null)
        {
            attackable.OnHit(m_Collider);
        }
    }

}
