using UnityEngine;
using System.Collections;

public class BouncyBoxBounce : MonoBehaviour
{


    public float _forceMultiplyer = 30f;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.rigidbody.AddForce(collision.impulse * _forceMultiplyer);
        }
    }

}
