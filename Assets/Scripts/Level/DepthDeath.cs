using UnityEngine;
using System.Collections;
using System;

public class DepthDeath : MonoBehaviour
{
    [SerializeField]
    private float m_DeathDepth;

    private Transform m_PlayerTransform;
    private Transform m_LocalTransform;

    void Awake()
    {
        m_LocalTransform = transform;
        if (!m_PlayerTransform)
        {
            m_PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            m_LocalTransform.position = new Vector3(m_PlayerTransform.position.x, m_DeathDepth, m_PlayerTransform.position.z);
        }
        RegisterEvents();
    }

    private void RegisterEvents()
    {
        EventBus.LevelEnded.AddListener(LevelEnded);
    }

    private void LevelEnded(GameState obj)
    {
        Destroy(gameObject);
    }

    public void OnDestroy()
    {
        RemoveEvents();
    }

    private void RemoveEvents()
    {
        EventBus.LevelEnded.RemoveListener(LevelEnded);
    }

    void Update()
    {
        if (m_PlayerTransform)
            m_LocalTransform.position = new Vector3(m_PlayerTransform.position.x, m_DeathDepth, m_PlayerTransform.position.z);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            EventBus.LevelEnded.Dispatch(GameState.GameLost);
        }
    }
}
