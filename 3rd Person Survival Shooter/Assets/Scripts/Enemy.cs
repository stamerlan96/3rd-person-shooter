using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    private NavMeshAgent _navMeshAgent;

    private Transform target;
    private bool seeTarget;
    [Header("Shooting Parameters")]

    [SerializeField] public float shootPower = 10f;
    [SerializeField] public float fireRate = 15f;
    [SerializeField] public float destroyOffset = 1f;
    [SerializeField] public float shootingTime = 0.1f;

    public GameObject bulletPrefab;
    public Transform gun;

    public Transform Target
    {
        get => target;
        set => target = value;
    }

    // Start is called before the first frame update
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        InvokeRepeating("ShootEnemy", 2f, fireRate);
    }

    void Update()
    {
        if (target != null)
        {
            _navMeshAgent.SetDestination(target.position);
        }
        else
        {
            Debug.LogError("Target not assigned!");
        }
        
        CheckTargetVisibility();
        
        
    }
    private void ShootEnemy()
    {
        if (seeTarget)
        {
            GameObject newBullet = Instantiate(bulletPrefab, gun.position, gun.rotation) as GameObject;
            Vector3 targetDirection = target.position - gun.position;
            targetDirection.Normalize();
            newBullet.GetComponent<Rigidbody>().AddForce(targetDirection * shootPower);
            Destroy(newBullet, 4);
        }
    }

        private void CheckTargetVisibility()
    {
        Vector3 targetDirection = target.position - gun.position;

        Ray ray = new Ray(gun.position, targetDirection);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            {
            if (hit.transform == target)
            {
                seeTarget = true;
                return;
            }
        }

        seeTarget = false;
    }
    
}
