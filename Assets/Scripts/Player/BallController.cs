using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class BallController : MonoBehaviour
{
    [SerializeField]
    private float m_MovemenMultiplier = 3f;
    [SerializeField]
    private GameObject m_Explosion;

    private Vector3 m_MoveDirection;

    private Transform m_MainCam;
    private Transform m_LocalTransform;

    private Vector2 m_SwipeStartPosition;
    private Vector2 m_SwipeEndPosition;

    //private float m_ExplosionForce = 10f;

    private Rigidbody m_RigidBody;

    private GameManager m_Path;
    private bool m_IsMoveable = true;

    void Awake()
    {
        if (Camera.main != null)
        {
            m_MainCam = Camera.main.transform;
        }

        m_RigidBody = gameObject.GetComponent<Rigidbody>();
        m_Path = GameObject.Find("Game Manager").GetComponent<GameManager>();
        m_LocalTransform = transform;
        RegisterEvents();
    }

    void OnDestroy()
    {
        RemoveEvents();
    }

    private void RemoveEvents()
    {

        EventBus.LevelEnded.RemoveListener(LevelEnded);
    }

    private void RegisterEvents()
    {
        EventBus.LevelEnded.AddListener(LevelEnded);
    }

    private void LevelEnded(GameState state)
    {
        if (state == GameState.GameWon)
        {
            m_IsMoveable = false;
        }
        else
        {
            if (m_Explosion)
                Instantiate(m_Explosion, m_LocalTransform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (!m_IsMoveable)
            return;

        Vector2 swipeDirection = GetInputSwipeDirection();

        if (m_MainCam != null)
        {
            Vector3 camForward = Vector3.Scale(m_MainCam.forward, new Vector3(1, 0, 1)).normalized;
            if (swipeDirection != Vector2.zero)
                Debug.Log("Swipe Direction" + swipeDirection);

            m_MoveDirection = (swipeDirection.y * camForward + swipeDirection.x * m_MainCam.right).normalized;
            if (m_MoveDirection != Vector3.zero)
                Debug.Log("Move Direction" + m_MoveDirection);
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
                SetSwipeStartPos(touch.position);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                SetSwipeEndPos(touch.position);
                swipeDirection = GetSwipeDirection();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                SetSwipeStartPos(Input.mousePosition);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                SetSwipeEndPos(Input.mousePosition);
                swipeDirection = GetSwipeDirection();
            }

        }

        return swipeDirection;
    }

    void SetSwipeStartPos(Vector2 startPos)
    {
        m_SwipeStartPosition = startPos;
    }

    void SetSwipeEndPos(Vector2 endPos)
    {
        m_SwipeEndPosition = endPos;
    }

    Vector2 GetSwipeDirection()
    {
        return (m_SwipeEndPosition - m_SwipeStartPosition).normalized;
    }


    void FixedUpdate()
    {
        if (!m_IsMoveable)
            return;
        m_Path.UpdateScore(m_MoveDirection);
        //m_RigidBody.AddTorque(new Vector3(m_MoveDirection.z, 0, -m_MoveDirection.x) * m_MovemenMultiplier);
        //m_RigidBody.AddTorque(m_MoveDirection * m_MovemenMultiplier, ForceMode.VelocityChange);
        m_RigidBody.AddForce(m_MoveDirection * m_MovemenMultiplier, ForceMode.VelocityChange);
    }

    public void ScaryBlockHit(Vector3 explosionPoint)
    {
        //m_RigidBody.AddExplosionForce(m_ExplosionForce, explosionPoint, 3f);
        EventBus.LevelEnded.Dispatch(GameState.GameLost);
    }

}
