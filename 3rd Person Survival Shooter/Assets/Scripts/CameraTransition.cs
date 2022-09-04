using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    [SerializeField] public GameObject player;

    private Vector3 _position;
    private Quaternion _rotation;
    // Update is called once per frame

    private void Awake()
    {
        _position = player.GetComponent<Transform>().localPosition;
        _rotation = player.GetComponent<Transform>().localRotation;
    }
    void FixedUpdate()
    {
        transform.position = _position;
        transform.rotation = _rotation;
    }
}
