using UnityEngine;
using System.Collections;
using System;

public class ScaryBlockController : MonoBehaviour
{
    [SerializeField]
    private Vector3 m_MovementDirection;
    [SerializeField]
    private float m_CurrentMovementSpeed = 2f;

    public float MovementSpeed
    {
        set
        {
            m_CurrentMovementSpeed = value;
        }
    }

    private bool m_MovementEnabled = true;

    private Transform m_LocalTransform;
    //private Vector3 m_InitialPosition;

    void Awake()
    {
        RegisterEvents();
    }

    private void RegisterEvents()
    {
        EventBus.LevelEnded.AddListener(LevelEnded);
    }

    void OnDestroy()
    {
        ClearEvents();
    }

    private void ClearEvents()
    {
        EventBus.LevelEnded.RemoveListener(LevelEnded);
    }

    private void LevelEnded(GameState state)
    {
        m_MovementEnabled = false;
    }

    void Start()
    {
        m_LocalTransform = transform;
        //m_InitialPosition = m_LocalTransform.position;
    }

    void Update()
    {
        if (m_MovementEnabled)
            m_LocalTransform.position = m_LocalTransform.position + m_MovementDirection * m_CurrentMovementSpeed * Time.deltaTime;
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
