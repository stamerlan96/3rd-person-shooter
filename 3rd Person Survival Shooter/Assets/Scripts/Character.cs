using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    [Header("Shooting Parameters")]
    [SerializeField] public float shootPower = 10f;
    [SerializeField] public float fireRate = 15f;
    [SerializeField] public float destroyOffset = 1f;
    [SerializeField] public float shootingTime = 0.1f;
    [SerializeField] public GameObject shootParticles;


    public GameObject bulletPrefab;
    public Transform gun;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShootBullet()
    {
        GameObject newBullet = Instantiate(bulletPrefab, gun.position, gun.rotation) as GameObject;
        newBullet.GetComponent<Rigidbody>().AddForce(gun.forward * shootPower);
        GameObject shellsGO = Instantiate(shootParticles, gun.position, gun.rotation * Quaternion.AngleAxis(90f, Vector3.up)) as GameObject;
        Destroy(shellsGO, shootingTime);
        Destroy(newBullet, 2);
    }
}
