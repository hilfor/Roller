using UnityEngine;
using System.Collections;

public class FollowObject : MonoBehaviour
{

    public Transform _TargetObject;
    public Vector3 _Offset;
    public float _minDistanceToFollow;
    
    private Transform _localTransform;
    private bool _catchingUp = false;
    void Start()
    {
        _localTransform = transform;
    }
    void Update()
    {
        
        if (Vector3.Distance(_localTransform.position, _TargetObject.position) > _minDistanceToFollow)
        {
            
            _localTransform.position = _TargetObject.position + _Offset;
        }
        else
        {
            _localTransform.LookAt(_TargetObject.position);
        }

    }


}
