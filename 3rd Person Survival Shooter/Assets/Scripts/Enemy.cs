using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    private NavMeshAgent _navMeshAgent;

    private Transform target;

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
        InvokeRepeating("Shoot", 2, 5);
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
    }

    /// <summary> 
    
    ///private void ChackTargetVisibility()
    ///{
    ///    Vector3 targetDirection = target.position - gun.position;

    //    Ray ray = new Ray(gun.position, targetDirection);

      //  RaycastHit hit;

        //if (Physics.Raycast(ray, out hit))
        //{
          //  if (hit.transform == target)
            //{
              //  seeTarget = true;
               // return;
            //}
        //}

        //seeTarget = false;
    //}
    /// </summary>
}
