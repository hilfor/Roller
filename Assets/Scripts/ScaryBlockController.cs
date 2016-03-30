using UnityEngine;
using System.Collections;

public class ScaryBlockController : MonoBehaviour
{

    public Vector3 _movementDirection;
    public float _movementSpeed = 2f;
    public Vector3 _explosionVectorDirection;
    
    public float _explosionForce = 2f;

    private bool _movementEnabled = false;

    private Transform _localTransform;
    private Vector3 _initialPosition;
    void Start()
    {
        _localTransform = transform;
        _initialPosition = _localTransform.position;
    }

    void Update()
    {
        if (_movementEnabled)
            _localTransform.position = _localTransform.position + _movementDirection * _movementSpeed * Time.deltaTime;
    }

    public void EnableMovement()
    {
        _movementEnabled = true;
    }
    public void DisableMovement()
    {
        _movementEnabled = false;
        _localTransform.position = _initialPosition;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("On trigger " + other.name);
        if (other.name == "LevelPlayer")
        {
            Debug.Log("Creating Explosion");
            other.GetComponent<Rigidbody>().AddForce(_localTransform.forward + _explosionVectorDirection * _explosionForce);
            other.GetComponent<UnityStandardAssets.Vehicles.Ball.Ball>().ScaryBlockHit(other.transform.position);
        }
    }
}
