using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackableBase : MonoBehaviour
{
    public virtual void OnHit(Collider2D collider)
    {
        Debug.LogWarning("No OnHit event setup for " + gameObject.name, gameObject);
    }
}
