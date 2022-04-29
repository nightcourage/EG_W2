using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _secondatyTarget;
    [SerializeField] private float _heightDamping;
    [SerializeField] private float _rotationDamping;
    [SerializeField] private float _height;
    private bool isAlive;
    private Vector3 position;
    
    private float _distance;

    private void Awake()
    {
        _distance = Vector3.Distance(_target.position, transform.position);
    }

    void Update()
    {
        isAlive = _target.GetComponent<Player>().isAlive;
        
        if (isAlive = true)
        {
            float wantedRotationAngle = _target.eulerAngles.y;
            float wantedHeight = _target.position.y + _height;

            float currentRotationAngle = transform.eulerAngles.y;
            position = transform.position;
            float currentHeight = position.y;

            currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, _rotationDamping);
            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, _heightDamping);

            Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

            position = _target.position;
            position -= currentRotation * Vector3.forward * _distance;
            position.y = currentHeight;
            transform.position = position;
        
            transform.LookAt(_target);
        }
        else
        {
            position = transform.position;
        }
    }
}
