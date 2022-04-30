using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Serialization;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Player : MonoBehaviour
{
    public int Speed = 10;
    public int BoostMultiplier = 3;
    public int RotationSpeed = 10;
    private Vector3 _eulerAngeleVelocity;
    public ProgressListener ProgressListener;
    public bool isAlive;
    
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _eulerAngeleVelocity = new Vector3(0, RotationSpeed, 0);
        isAlive = true;
    }

    private void Update()
    {
        float translationHorizontal = Input.GetAxis("Horizontal");
        float translationVertical = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            ChargePlayer(translationHorizontal, translationVertical);
        }
        else
        {
            MovePlayer(translationHorizontal, translationVertical);
        }
        
        if (Input.GetKey(KeyCode.Q))
        {
            int direction = -1;
            RotatePlayer(direction);
        }

        if (Input.GetKey(KeyCode.E))
        {
            int direction = 1;
            RotatePlayer(direction);
        }
    }

    void FixedUpdate()
    {
    }

    private void MovePlayer(float translationHorizontal, float translationVertical)
    {
        float finalSpeed = Speed * Time.deltaTime;
        _rigidbody.velocity = transform.TransformDirection(new Vector3(translationHorizontal, 0, translationVertical).normalized * finalSpeed);
    }

    private void ChargePlayer(float translationHorizontal, float translationVertical)
    {
        
        float boostSpeed = Speed * BoostMultiplier * Time.deltaTime;
        _rigidbody.AddRelativeForce(translationHorizontal * boostSpeed,0, translationVertical * boostSpeed, ForceMode.VelocityChange);
    }

    private void RotatePlayer(int direction)
    {
        Quaternion deltaRotation = Quaternion.Euler(_eulerAngeleVelocity * (direction * Time.fixedDeltaTime));
        _rigidbody.MoveRotation(_rigidbody.rotation * deltaRotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<RotatedBarrier>())
        {
            isAlive = false;
            gameObject.SetActive(false);
            ProgressListener.ShowLoseScreen();
        }
    }
}
