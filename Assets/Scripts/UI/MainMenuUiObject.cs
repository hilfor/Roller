using UnityEngine;
using System.Collections;
using System;

public class MainMenuUiObject : BaseUIObject
{

    private Animator m_Animator;

    void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    public void OnDisplayAnimationEnded()
    {
        OnCompleteCallback.Invoke();
    }

    public override void StartAnim()
    {
        m_Animator.SetTrigger("Show");
    }
}
