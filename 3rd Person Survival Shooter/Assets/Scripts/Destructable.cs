using System;
using UnityEngine;

public class Destructable : MonoBehaviour
{

    [SerializeField] private float startsHitsPoints = 100f;
    [SerializeField] private float hitPoints;
    // Start is called before the first frame update
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
