using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Unit : MonoBehaviour
{
    private Vector3 targetPosition;
    [SerializeField] private Animator _unitAnimator;
    [SerializeField] private float _movespeed = 4f;
    [SerializeField] private float _stoppingDistance = .1f;
    [SerializeField] private float _rotateSpeed = 10f;
    private const string _iswalking = "IsWalking";

    private void Awake()
    {
        targetPosition = transform.position;
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, targetPosition) > _stoppingDistance)
        {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
        
            transform.position += moveDirection * _movespeed * Time.deltaTime;
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * _rotateSpeed);
            _unitAnimator.SetBool(_iswalking, true);

        }
        else
        {
            _unitAnimator.SetBool(_iswalking, false);
        }
    } 

    public void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }
}
