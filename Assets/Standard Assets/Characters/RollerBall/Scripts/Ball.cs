using System;
using UnityEngine.UI;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Ball
{
    public class Ball : MonoBehaviour
    {
        [SerializeField]
        private int m_Life = 1;
        [SerializeField]
        private float m_MovePower = 5; // The force added to the ball to move it.
        [SerializeField]
        private bool m_UseTorque = true; // Whether or not to use torque to move the ball.
        [SerializeField]
        private float m_MaxAngularVelocity = 25; // The maximum velocity the ball can rotate at.
        [SerializeField]
        private float m_JumpPower = 2; // The force added to the ball when it jumps.
        [SerializeField]
        private GameObject m_ExplosionHit;
        [SerializeField]
        private GameObject m_ExplosionDestroy;
        [SerializeField]
        private Text m_ScoreUI;

        private float m_CurrentScore = 0;
        

        private const float k_GroundRayLength = 1f; // The length of the ray to check if the ball is grounded.
        private Rigidbody m_Rigidbody;
        private Transform m_Transform;


        private void Start()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
            m_Transform = transform;
            // Set the maximum angular velocity.
            GetComponent<Rigidbody>().maxAngularVelocity = m_MaxAngularVelocity;
        }

        public void ScaryBlockHit(Vector3 hitPoint)
        {
            if (--m_Life > 0)
            {
                Instantiate(m_ExplosionHit, hitPoint, Quaternion.identity);
            }
            else
            {
                Instantiate(m_ExplosionDestroy, hitPoint, Quaternion.identity);

            }

        }


        public void Move(Vector3 moveDirection, bool jump)
        {
            // If using torque to rotate the ball...
            if (m_UseTorque)
            {
                // ... add torque around the axis defined by the move direction.
                m_Rigidbody.AddTorque(new Vector3(moveDirection.z, 0, -moveDirection.x) * m_MovePower);
            }
            else
            {
                // Otherwise add force in the move direction.
                m_Rigidbody.AddForce(moveDirection * m_MovePower);
            }

            // If on the ground and jump is pressed...
            if (Physics.Raycast(transform.position, -Vector3.up, k_GroundRayLength) && jump)
            {
                // ... add force in upwards.
                m_Rigidbody.AddForce(Vector3.up * m_JumpPower, ForceMode.Impulse);
            }
        }

        private void Update()
        {
            m_CurrentScore += Time.deltaTime;
            m_ScoreUI.text = m_CurrentScore.ToString();
        }
    }
}
