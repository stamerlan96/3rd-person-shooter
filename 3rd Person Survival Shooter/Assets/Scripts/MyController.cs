using JetBrains.Annotations;
using UnityEditor.PackageManager;
using UnityEngine;

public class MyController : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float movingForce = 20.0f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] public float jumpForce = 10f;
    [SerializeField] private float jumpDelay = 0.4f;
    [SerializeField, Range(0, 90)] public float maxSlope = 30f;
    [SerializeField, Range(0, 1)] public float speedDamping = 0.3f;
    [SerializeField] public GameObject cam;
    private float nextTimeToJump = 0f;

    [Header("Shooting Parameters")]
    [SerializeField] public float shootPower = 10f;
    [SerializeField] public float fireRate = 15f;
    [SerializeField] public float destroyOffset = 1f;
    [SerializeField] public float shootingTime = 0.1f;
    [SerializeField] public float bulletDamage = 1f;
    [SerializeField] public GameObject shootParticles;
    private float nextTimeToFire = 0f;

    public GameObject bulletPrefab;
    public Transform gun;
    private bool onGround;
    private Rigidbody _rigidbody;
    


    // Methods
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }


    private void Start()
    {

    }

    // Update is called once per frame
    private void FixedUpdate()
    {

        if (onGround)
        {
            ApplyMovementForce();
            if (Input.GetButton("Jump") && Time.time >= nextTimeToJump)
            {
                nextTimeToJump = Time.time + jumpDelay;
                _rigidbody.AddForce(Vector3.up * jumpForce);
            }
            else
            {
                _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, maxSpeed);
            }
        }

        cam.GetComponent<Transform>().position = transform.position;
    }

    private void Update()
    {
        LookAtTarget();

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    private void ApplyMovementForce()
    {
        Vector3 xAxisForce = transform.right * Input.GetAxis("Horizontal"); //определяем силу по оси Х
        Vector3 zAxisForce = transform.forward * Input.GetAxis("Vertical"); //определяем силу по оси Z

        Vector3 resultXZForce = xAxisForce + zAxisForce;
        resultXZForce.Normalize();

        resultXZForce = resultXZForce * movingForce; //умножаем результирующий вектор на силу движения персонажа (задаем скорость)

        if (resultXZForce.magnitude > 0)
        {
            _rigidbody.AddForce(resultXZForce);
        }
        else
        {
            Vector3 dampedVelocity = _rigidbody.velocity * speedDamping;
            dampedVelocity.y = _rigidbody.velocity.y;
            _rigidbody.velocity = dampedVelocity;

        }

    }

    private void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.layer == 3 || collisionInfo.gameObject.layer == 6 )
        {
            onGround = true;
        }
    }

    private bool ChechIsOnTheGround(Collision collisionInfo, float slope)
    {
        for (int i = 0; i < collisionInfo.contacts.Length; i++)
        {
            if (collisionInfo.contacts[i].point.y < transform.position.y)
            {
                if (Vector3.Angle(collisionInfo.contacts[i].normal, Vector3.up) < slope)
                {
                    return true;
                }

            }

        }
        return false;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 3 || collision.gameObject.layer == 6)
        {
            onGround = false;
        }
    }
    private void LookAtTarget()
    {
        Plane plane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance;

        if (plane.Raycast(ray, out distance))
        {
            Vector3 position = ray.GetPoint(distance);;
            position.y = transform.position.y;
            transform.LookAt(position);
        }
        /*
        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitInfo))
        {
            Vector3 position = ray.GetPoint(hitInfo.distance);
            position.y = transform.position.y;
            transform.LookAt(position);
        }*/
    }

    private void Shoot()
    {
        GameObject newBullet = Instantiate(bulletPrefab, gun.position, gun.rotation) as GameObject;
        newBullet.GetComponent<Rigidbody>().AddForce(gun.forward * shootPower);
        GameObject shellsGO = Instantiate(shootParticles, gun.position, gun.rotation * Quaternion.AngleAxis(90f, Vector3.up)) as GameObject;
        Destroy(shellsGO, shootingTime);
        Destroy(newBullet, 2);
    }
}
