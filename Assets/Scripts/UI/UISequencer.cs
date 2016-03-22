using UnityEngine;
using System.Collections;

public class UISequencer : MonoBehaviour
{

    public BaseUIObject[] uiObjects;
    private int displayIndex = 0;


    void Start()
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
