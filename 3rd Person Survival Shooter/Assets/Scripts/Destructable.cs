using System;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startsHitsPoints = 100f;
    [SerializeField] private float hitPoints;
    [SerializeField] private GameObject destroyEffect;
    [SerializeField] private float destractioinEfectTime = 1f;

    void Start()
    {
        hitPoints = startsHitsPoints;
    }

    public void Hit(float damage)
    {
        hitPoints -= damage;

        if (hitPoints <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        destructionEffect();
    }

    private void destructionEffect()
    {
        GameObject effect = Instantiate(destroyEffect, GetComponent<Transform>().position, GetComponent<Transform>().rotation * Quaternion.AngleAxis(90f, Vector3.up)) as GameObject;
        Destroy(effect, destractioinEfectTime);
    }

    // Update is called once per frame
    void Update()
    {

    }
}