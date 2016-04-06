using UnityEngine;
using System;
using System.Collections;

public class UISequencer : MonoBehaviour
{

    public BaseUIObject[] m_StartGameSequence;

    public BaseUIObject[] m_GameOverSequence;
    public BaseUIObject[] m_WinSequence;

    private int m_StartSequenceDisplayIndex = 0;
    private int m_WinSequenceDisplayIndex = 0;
    private int m_GameOverSequenceDisplayIndex = 0;

    void Awake()
    {
        RegisterEvents();
    }

    void RegisterEvents()
    {
        EventBus.GameLost.AddListener(GameOverEvent);
        EventBus.GameWon.AddListener(GameWonEvent);
    }

    public void OnDestroy()
    {
        RemoveEvents();
    }

    private void RemoveEvents()
    {
        EventBus.GameLost.RemoveListener(GameOverEvent);
        EventBus.GameWon.RemoveListener(GameWonEvent);
    }

    void GameOverEvent()
    {
        if (m_GameOverSequence.Length > 0)
        {
            m_GameOverSequence[m_GameOverSequenceDisplayIndex++].StartAnim();
        }
    }

    void GameWonEvent()
    {
        Debug.Log("UISequence displaying win UI");
        if (m_WinSequence.Length > 0)
        {
            m_WinSequence[m_WinSequenceDisplayIndex++].StartAnim();
        }
    }



    void Start()
    {
        if (m_StartGameSequence.Length > 0)
        {
            SetOnCompleteCallback(m_StartGameSequence, OnStartSequenceComplete);
            m_StartGameSequence[m_StartSequenceDisplayIndex++].StartAnim();
        }
        SetOnCompleteCallback(m_GameOverSequence, OnGameOverSequenceComplete);
        SetOnCompleteCallback(m_WinSequence, OnWinSequenceComplete);

    }



    private void SetOnCompleteCallback(BaseUIObject[] uiObjects, Action act)
    {
        for (int i = 0; i < uiObjects.Length; i++)
        {
            uiObjects[i]._onComplete += act;
        }
    }

    void OnGameOverSequenceComplete()
    {
        if (m_GameOverSequenceDisplayIndex < m_GameOverSequence.Length)
        {
            m_GameOverSequence[m_GameOverSequenceDisplayIndex++].StartAnim();
        }
        else
        {
            m_GameOverSequenceDisplayIndex = 0;
        }
    }

    void OnWinSequenceComplete()
    {
        if (m_WinSequenceDisplayIndex < m_WinSequence.Length)
        {
            m_WinSequence[m_WinSequenceDisplayIndex++].StartAnim();
        }
        else
        {
            m_WinSequenceDisplayIndex = 0;
        }
    }

    void OnStartSequenceComplete()
    {
        if (m_StartSequenceDisplayIndex < m_StartGameSequence.Length)
        {
            m_StartGameSequence[m_StartSequenceDisplayIndex++].StartAnim();
        }
        else
        {
            m_StartSequenceDisplayIndex = 0;
        }
    }

}
