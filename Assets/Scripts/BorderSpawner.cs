using UnityEngine;
using System;
using System.Collections;

public class BorderSpawner : MonoBehaviour
{

    private Action<Vector3> m_OnTriggerClearEvent;
    private Transform m_Transform;

    void Awake()
    {
        m_Transform = transform;
    }

    public Action<Vector3> OnTriggetClearedEvent
    {
        set
        {
            m_OnTriggerClearEvent += value;
        }
    }


    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Floor")
        {
            m_OnTriggerClearEvent.Invoke(other.transform.position);
        }
    }
}
