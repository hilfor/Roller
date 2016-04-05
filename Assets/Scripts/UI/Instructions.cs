using UnityEngine;
using System.Collections;
using System;

public class Instructions : BaseUIObject
{
    [SerializeField]
    private float m_HideAfter = 4f;

    private float m_CompleteAfter = 2f;

    private Animator m_Animator;

    private float m_DisplayTimeCounter = 0f;
    private float m_CompleteTimeCounter = 0f;
    private bool m_Disabled = true;

    private bool m_HideAnimStarted = false;

    void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    public override void StartAnim()
    {
        m_Animator.SetTrigger("Show");
        m_Disabled = false;
    }

    void Update()
    {
        if (m_Disabled) return;

        if (m_DisplayTimeCounter < m_HideAfter)
        {
            m_DisplayTimeCounter += Time.deltaTime;
        }
        else
        {
            StartHideAnim();
            if (m_CompleteTimeCounter < m_CompleteAfter)
            {
                m_CompleteTimeCounter += Time.deltaTime;
            }
            else
            {
                _onComplete.Invoke();
                m_Disabled = true;
            }
        }
    }

    void StartHideAnim()
    {
        if (m_HideAnimStarted)
            return;
        m_Animator.SetTrigger("Hide");
        m_HideAnimStarted = true;
    }
}
