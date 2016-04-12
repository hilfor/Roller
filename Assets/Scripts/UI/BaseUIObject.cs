using UnityEngine;
using System;
using System.Collections;

public abstract class BaseUIObject : MonoBehaviour {

    public Action OnCompleteCallback;


    public abstract void StartAnim();
}
