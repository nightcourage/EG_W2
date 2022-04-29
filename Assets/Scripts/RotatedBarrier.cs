using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatedBarrier : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    void Update()
    {
        transform.rotation *= Quaternion.Euler(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
