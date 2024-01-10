using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Unit : MonoBehaviour
{
    private Vector3 targetPosition;
    [SerializeField] private Animator unitAnimator;
    [SerializeField] private float movespeed = 4f;
    [SerializeField] private float stoppingDistance = .1f;
    [SerializeField] private float rotateSpeed = 10f;
    private const string iswalking = "IsWalking";

    private void Awake()
    {
        targetPosition = transform.position;
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
        {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
        
            transform.position += moveDirection * movespeed * Time.deltaTime;
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
            unitAnimator.SetBool(iswalking, true);

        }
        else
        {
            unitAnimator.SetBool(iswalking, false);
        }
    } 

    public void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }
}
