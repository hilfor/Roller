using UnityEngine;
using System.Collections;

public class UISequencer : MonoBehaviour
{

    public BaseUIObject[] uiObjects;
    private int displayIndex = 0;

    void Start()
    {
    }

    public void StartSequence()
    {
        OnAnimComplete();
    }

    void OnAnimComplete()
    {
        if (displayIndex < uiObjects.Length)
        {
            uiObjects[displayIndex]._onComplete += OnAnimComplete;
            uiObjects[displayIndex++].gameObject.SetActive(true);
        }
    }

}
