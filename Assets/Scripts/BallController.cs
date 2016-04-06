using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class BallController : MonoBehaviour
{
    [SerializeField]
    private float m_MovePower = 3f;

    private Vector3 m_MoveDirection;

    private Transform m_MainCam;

    private Vector2 m_SwipeStartPosition;
    private Vector2 m_SwipeEndPosition;

    private float m_ExplosionForce = 10f;

    private Rigidbody m_RigidBody;

    private GameManager m_Path;
    private bool m_IsMoveable = true;

    void Awake()
    {
        RegisterEvents();
    }

    void OnDestroy()
    {
        RemoveEvents();
    }

    private void RemoveEvents()
    {
        EventBus.GameWon.RemoveListener(LevelEnded);
        EventBus.GameLost.RemoveListener(LevelEnded);
    }

    private void RegisterEvents()
    {
        EventBus.GameWon.AddListener(LevelEnded);
        EventBus.GameLost.AddListener(LevelEnded);
    }

    private void LevelEnded()
    {
        m_IsMoveable = false;
    }

    void Start()
    {
        if (Camera.main != null)
        {
            m_MainCam = Camera.main.transform;
        }
        m_RigidBody = gameObject.GetComponent<Rigidbody>();
        m_Path = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (!m_IsMoveable)
            return;

        Vector2 swipeDirection = GetInputSwipeDirection();

        if (m_MainCam != null)
        {
            Vector3 camForward = Vector3.Scale(m_MainCam.forward, new Vector3(1, 0, 1)).normalized;
            m_MoveDirection = (swipeDirection.y * camForward + swipeDirection.x * m_MainCam.right).normalized;
        }
        else
        {
            // we use world-relative directions in the case of no main camera
            m_MoveDirection = (swipeDirection.y * Vector3.forward + swipeDirection.x * Vector3.right).normalized;
        }
    }


    Vector2 GetInputSwipeDirection()
    {
        Vector2 swipeDirection = Vector2.zero;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                m_SwipeStartPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                m_SwipeEndPosition = touch.position;
                swipeDirection = m_SwipeEndPosition - m_SwipeStartPosition;
                swipeDirection.Normalize();
            }
        }

        return swipeDirection;

    }
    void FixedUpdate()
    {
        if (!m_IsMoveable)
            return;
        m_Path.UpdateScore(m_MoveDirection);
        m_RigidBody.AddTorque(new Vector3(m_MoveDirection.z, 0, -m_MoveDirection.x) * m_MovePower);
    }

    public void ScaryBlockHit(Vector3 explosionPoint)
    {
        m_RigidBody.AddExplosionForce(m_ExplosionForce, explosionPoint, 3f);
    }

}
