using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatProximityTrigger : MonoBehaviour {

    CharacterBatControl m_Control;
    CircleCollider2D m_MyCollider;

    public float DetectedSize;
    private float NotDetectedSize;

    private void Awake()
    {
        m_Control = GetComponentInParent<CharacterBatControl>();
        m_MyCollider = GetComponent<CircleCollider2D>();
        NotDetectedSize = m_MyCollider.radius;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            m_Control.SetCharacterInRange(collision.gameObject);
            m_MyCollider.radius = DetectedSize;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            m_Control.SetCharacterInRange(null);
            m_MyCollider.radius = NotDetectedSize;
        }
    }
}
