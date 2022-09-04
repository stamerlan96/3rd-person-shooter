using System;
using UnityEngine;

public class OnCollisionDestoroy : MonoBehaviour
{
    [Header("Die On Collision Parameters")]
    [SerializeField] public float bulletDestroyOffset = 1f;
    [SerializeField] public float impactTime = 0.1f;
    [SerializeField] public GameObject flare;

    [Header("Damage Parameters")]
    [SerializeField] private float damage;
    [SerializeField] public GameObject hitEffect;

    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Time.time >= bulletDestroyOffset)
        {
            if (collision.gameObject.layer == 7)
            {
                EnemyHit(collision);
                //анимация столкновения с enemy
            }
            else
            {
                // анимация столконвения со стеной
            }

            CollisionWithWallEffect(collision);
        }

    }

    private void EnemyHit(Collision collisionInfo)
    {
        Destructable target = collisionInfo.gameObject.GetComponent<Destructable>();

        if (target != null)
        {
            target.Hit(Damage);
        }
        Destroy(gameObject);

        EnemyHitEffect(collisionInfo);
    }
    private void EnemyHitEffect(Collision collisionInfo)
    {
        GameObject hitEffect = Instantiate(flare, collisionInfo.contacts[1].point, Quaternion.LookRotation(collisionInfo.contacts[1].normal)) as GameObject;
        Destroy(hitEffect, impactTime);
    }
    

    private void CollisionWithWallEffect(Collision collision)
    {
        Destroy(gameObject);
        GameObject flareGO = Instantiate(flare, collision.contacts[1].point, Quaternion.LookRotation(collision.contacts[1].normal)) as GameObject;
        Destroy(flareGO, impactTime);
    }
}
