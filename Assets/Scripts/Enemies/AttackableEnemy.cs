using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackableEnemy : AttackableBase {

    public GameObject DestroyObjectOnDeath;
    public int MaxHealth;
    public    CharacterMovementModel MovementModel;

    public GameObject DestroyEffect;
    public Sprite DestroyedSprite;
    public float HitPushStrength;
    public float HitPushDuration;
    public float DestroyDelayOnDeath;

    int m_Health;
    private SpriteRenderer m_Renderer;

    private void Awake()
    {
        m_Health = MaxHealth;
        m_Renderer = GetComponentInChildren<SpriteRenderer>();
    }

    public int GetHealth()
    {
        return m_Health;
    }

    IEnumerator CreateDeathEffect(float delay)
    {
        yield return new WaitForSeconds(delay);
        BroadcastMessage("OnLootDrop", SendMessageOptions.DontRequireReceiver);
        GameObject destroyEffect = (GameObject)Instantiate(DestroyEffect);
        destroyEffect.transform.position = transform.position;
    }


    public override void OnHit(Collider2D hitCollider)
    {
        m_Health--;

        if (MovementModel != null)
        {
            Vector3 pushDirection = transform.position - hitCollider.transform.position;
            pushDirection = pushDirection.normalized * HitPushStrength;
            MovementModel.PushCharacter(pushDirection, HitPushDuration);
        }
        Debug.Log("Bat losthealth and has " + m_Health + " Left");
        if(m_Health <= 0)
        {
        if (DestroyEffect != null)
        {
            StartCoroutine(CreateDeathEffect(HitPushDuration));
        }
            Destroy(DestroyObjectOnDeath, DestroyDelayOnDeath);            
        }
    }
}
