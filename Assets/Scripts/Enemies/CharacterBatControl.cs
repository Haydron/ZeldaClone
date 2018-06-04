using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBatControl : CharacterBaseControl
{
    public float pushStrength;
    public float pushTime;
    GameObject m_CharacterInRange;
    public AttackableEnemy AttackableEnemy;
    void Update()
    {
            UpdateDirection();
    }

    public void SetCharacterInRange(GameObject characterInRange)
    {
        m_CharacterInRange = characterInRange;
    }

    public void OnHitCharacter(GameObject character)
    {
        Vector2 direction = character.transform.position - transform.position;
        direction.Normalize();



        m_CharacterInRange = null;
        character.GetComponent<CharacterMovementModel>().PushCharacter(direction * pushStrength,pushTime);
    }

    void UpdateDirection()
    {
        Vector2 direction = Vector2.zero;
        if (m_CharacterInRange != null)
        {
            direction =  m_CharacterInRange.transform.position - transform.position;
            direction.Normalize();
        }
        if (AttackableEnemy != null && AttackableEnemy.GetHealth() <= 0)
        {
            direction = Vector3.zero;
        }
            SetDirection(direction);
    }

}
