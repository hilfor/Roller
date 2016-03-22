using UnityEngine;
using System.Collections;

public class ScaryBlockMovement : MonoBehaviour
{

    public Vector3 _movementDirection;
    public float _movementSpeed = 2f;
    public Vector3 _explosionVectorDirection;
    public float _explosionForce = 2f;

    private Transform _localTransform;
    void Start()
    {
        _localTransform = transform;
    }

    void Update()
    {
        _localTransform.position = _localTransform.position + _movementDirection * _movementSpeed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("On trigger " + other.name);
        if (other.tag == "Player")
        {
            Debug.Log("Creating Explosion");
            other.GetComponent<Rigidbody>().AddForce(_localTransform.forward + _explosionVectorDirection * _explosionForce);
        }
    }
}
