using UnityEngine;
using System;
using System.Collections;

public abstract class BaseUIObject : MonoBehaviour {

    public Action _onComplete;

    public abstract void StartAnim();
}
