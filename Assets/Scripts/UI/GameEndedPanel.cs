using UnityEngine;
using System.Collections;
using System;

public class GameEndedPanel : BaseUIObject
{
    //[SerializeField]
    //private GameObject[] m_LevelLostDisplayGroup;
    //[SerializeField]
    //private GameObject[] m_LevelWonDisplayGroup;

    //private bool m_Disabled = true;
    private Animator m_Animator;
    //private GameState m_LevelState;

    void Awake()
    {
        m_Animator = GetComponent<Animator>();
        //RegisterEvents();
    }

    public void OnDisplayAnimationEnded()
    {
        OnCompleteCallback.Invoke();
    }

    public override void StartAnim()
    {
        m_Animator.SetTrigger("Show");
    }


    //void RegisterEvents()
    //{
    //    EventBus.LevelEnded.AddListener(LevelStateChanged);
    //}

    //private void LevelStateChanged(GameState state)
    //{
    //    m_LevelState = state;
    //}

    //private void LevelEnded()
    //{
    //    if (m_LevelState == GameState.GameLost)
    //    {
    //        DisplayLostGroup();
    //    }
    //    else
    //    {
    //        DisplayWonGroup();
    //    }
    //}

    //private void DisplayLostGroup()
    //{
    //    for (int i = 0; i < m_LevelLostDisplayGroup.Length; i++)
    //    {
    //        m_LevelLostDisplayGroup[i].SetActive(true);
    //    }
    //}

    //private void DisplayWonGroup()
    //{
    //    for (int i = 0; i < m_LevelWonDisplayGroup.Length; i++)
    //    {
    //        m_LevelWonDisplayGroup[i].SetActive(true);
    //    }
    //}
}
