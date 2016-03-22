using UnityEngine;
using System.Collections;

public class Instructions : BaseUIObject{
    public float _destroyAfter = 3f;
    public Vector3 _targetScale;
    public Transform _introductionText;
    private Coroutine coroutine;

    public AnimationCurve curve;
    private float lerpCont = 0f;
    public float _lerpSpeed = .5f;
    void Start()
    {
        Destroy(gameObject, _destroyAfter);
    }

    void Update()
    {
        _introductionText.localScale = Vector3.Lerp(_introductionText.localScale, _targetScale, curve.Evaluate(lerpCont));
        lerpCont += Time.deltaTime * _lerpSpeed;
    }

    void OnDestroy()
    {
        _onComplete.Invoke();
    }

}
