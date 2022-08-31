using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    [SerializeField] public GameObject player;

    private Vector3 _position;
    // Update is called once per frame

    private void Awake()
    {
        _position = player.GetComponent<Transform>().position;
    }
    void FixedUpdate()
    {
        transform.position = _position;  
    }
}
