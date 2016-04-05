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

    private Rigidbody m_RigidBody;

    private GameManager m_Path;

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
        m_Path.UpdateScore(m_MoveDirection);
        m_RigidBody.AddTorque(new Vector3(m_MoveDirection.z, 0, -m_MoveDirection.x) * m_MovePower);
    }

}
