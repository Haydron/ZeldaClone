using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacterCollision : MonoBehaviour {
    CharacterBatControl m_Control;

    private void Awake()
    {
        m_Control = GetComponentInParent<CharacterBatControl>();
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            m_Control.OnHitCharacter(collider.gameObject);
            Debug.Log("Collide attackeds!");
        }
    }
}
