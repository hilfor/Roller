using UnityEngine;
using System.Collections;

public class BouncyBoxBounce : MonoBehaviour
{


    public float m_ForceMultiplyer = 10f;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            Debug.Log("Creating explosion");
            collision.rigidbody.AddExplosionForce(m_ForceMultiplyer, collision.contacts[0].point, 0.5f, 0f, ForceMode.Impulse);
        }
    }

}
