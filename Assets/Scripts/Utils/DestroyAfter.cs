using UnityEngine;
using System.Collections;

public class DestroyAfter : MonoBehaviour {

    public float m_DestroyAfter = 1f;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, m_DestroyAfter);
	}
	

}
