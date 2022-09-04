using System;
using UnityEngine;

public class OnCollisionDestoroy : MonoBehaviour
{
    [Header("Die On Collision Parameters")]
    [SerializeField] public float bulletDestroyOffset = 1f;
    [SerializeField] public float impactTime = 0.1f;
    [SerializeField] public GameObject flare;
    [SerializeField] public float bulletPower = 10f;
    [Header("Damage Parameters")]
    [SerializeField] private float damage;
    [SerializeField] public GameObject hitEffect;
<<<<<<< Updated upstream
=======
    [SerializeField] public float destroyTime = 1f;


>>>>>>> Stashed changes
    public float Damage
    {
        get { return damage; }
        set
        {
            if (value >= damage)
            {
                damage = value;
            }
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (Time.time >= bulletDestroyOffset)
        {
            if (collision.gameObject.layer == 7)
            {
<<<<<<< Updated upstream
                EnemyHit(collision); 
=======
                EnemyHit(collision);
                CollisionWithEnemyEffect(collision);
>>>>>>> Stashed changes
            }
            else
            {
                CollisionWithWallEffect(collision);
            }

        }

    }

    private void EnemyHit(Collision collisionInfo)
    {
        Destructable target = collisionInfo.gameObject.GetComponent<Destructable>();

        if (target != null)
        {
            target.Hit(Damage);

        }
        if (target != null)
        {
            //Vector3 direction = collisionInfo.gameObject.GetComponent<Rigidbody>().velocity.normalized;
            //collisionInfo.articulationBody.GetComponent<Rigidbody>().AddForce(direction * bulletPower);
        }
        Destroy(gameObject);


    }
    private void CollisionWithEnemyEffect(Collision collision)
    {

        Destroy(gameObject);

        GameObject effect = Instantiate(hitEffect, collision.contacts[1].point, Quaternion.LookRotation(collision.contacts[1].normal)) as GameObject;
        Destroy(effect, destroyTime);
    }

    private void CollisionWithWallEffect(Collision collision)
    {
        Destroy(gameObject);
        GameObject flareGO = Instantiate(flare, collision.contacts[1].point, Quaternion.LookRotation(collision.contacts[1].normal)) as GameObject;
        Destroy(flareGO, impactTime);
    }
}
