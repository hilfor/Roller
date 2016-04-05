using UnityEngine;
using System.Collections;

public class UISequencer : MonoBehaviour
{

    public BaseUIObject[] m_UiObjects;
    private int displayIndex = 0;


    void Start()
    {
        if (m_UiObjects.Length > 0)
        {
            for (int i = 0; i < m_UiObjects.Length; i++)
            {
                m_UiObjects[i]._onComplete += OnAnimComplete;
            }
            m_UiObjects[displayIndex++].StartAnim();
        }

    }


    //public void StartSequence()
    //{
    //    OnAnimComplete();
    //}

    void OnAnimComplete()
    {
        if (displayIndex < m_UiObjects.Length)
        {
            m_UiObjects[displayIndex++].StartAnim();
        }
    }

}
