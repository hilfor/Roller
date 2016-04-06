using UnityEngine;
using System.Collections;
using System;

public class ScaryBlockController : MonoBehaviour
{

    public Vector3 _movementDirection;
    public float _movementSpeed = 2f;
    public Vector3 _explosionVectorDirection;

    public float _explosionForce = 2f;

    private bool m_MovementEnabled = true;

    private Transform m_LocalTransform;
    private Vector3 _initialPosition;

    void Awake()
    {
        RegisterEvents();
    }

    private void RegisterEvents()
    {
        EventBus.GameLost.AddListener(LevelEnded);
        EventBus.GameWon.AddListener(LevelEnded);
    }

    void OnDestroy()
    {
        ClearEvents();
    }

    private void ClearEvents()
    {
        EventBus.GameLost.RemoveListener(LevelEnded);
        EventBus.GameWon.RemoveListener(LevelEnded);
    }

    private void LevelEnded()
    {
        m_MovementEnabled = false;
    }

    void Start()
    {
        m_LocalTransform = transform;
        _initialPosition = m_LocalTransform.position;
    }

    void Update()
    {
        if (m_MovementEnabled)
            m_LocalTransform.position = m_LocalTransform.position + _movementDirection * _movementSpeed * Time.deltaTime;
    }

    //public void EnableMovement()
    //{
    //    m_MovementEnabled = true;
    //}
    //public void DisableMovement()
    //{
    //    m_MovementEnabled = false;
    //    m_LocalTransform.position = _initialPosition;
    //}

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("On Collision " + collision.rigidbody.name);

        if (collision.rigidbody.tag == "Player")
        {
            Debug.Log("Creating Explosion");
            collision.rigidbody.GetComponent<BallController>().ScaryBlockHit(collision.contacts[0].point);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("On trigger " + other.name);
        if (other.tag == "Player")
        {
            Debug.Log("Creating Explosion");
            other.GetComponent<BallController>().ScaryBlockHit(m_LocalTransform.forward);
            //other.GetComponent<Rigidbody>().AddForce(_localTransform.forward + _explosionVectorDirection * _explosionForce);
        }
    }
}
