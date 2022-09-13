using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{

    private NavMeshAgent _navMeshAgent;

    private Transform target;
    private bool seeTarget;
  
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
        InvokeRepeating("Shoot", 2f, fireRate);
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
    private void Shoot()
    {
        if (seeTarget)
        {
            Vector3 targetDirection = target.position - gun.transform.position;

            targetDirection.Normalize();
 
		    ShootBullet();
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
