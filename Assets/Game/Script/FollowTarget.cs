using System;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3f;
    public Vector3 offset = Vector3.zero;

    private Vector3 _velocity;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void Start()
    {
        _transform.position = target.position + offset;
    }

    private void FixedUpdate()
    {
        Vector3 targetPosition = target.TransformPoint(offset);
        _transform.position = Vector3.SmoothDamp(_transform.position, targetPosition, ref _velocity, smoothTime);
    }
}
