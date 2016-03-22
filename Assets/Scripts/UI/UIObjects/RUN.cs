using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RUN : BaseUIObject
{
    public float _destroyAfter = 3f;
    public Vector3 _targetScale;
    public Transform _runText;
    private Coroutine coroutine;

    public AnimationCurve curve;
    private float lerpCont = 0f;
    public float _lerpSpeed = .5f;
    void Start()
    {
        Destroy(gameObject, _destroyAfter);
        //coroutine = StartCoroutine("RunScaling");
    }

    void Update()
    {
        _runText.localScale = Vector3.Lerp(_runText.localScale, _targetScale, curve.Evaluate(lerpCont));
        lerpCont += Time.deltaTime * _lerpSpeed;
    }

    //IEnumerator RunScaling()
    //{
    //    while (_destroyAfter > 0)
    //    {
    //        _runText.fontSize += 2;
    //        yield return new WaitForSeconds(.5f);
    //    }
    //}



    void OnDestroy()
    {
        _onComplete.Invoke();
    }




}
