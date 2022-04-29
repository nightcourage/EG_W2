using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Serialization;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Player : MonoBehaviour
{
    public int Speed = 10;
    public int BoostMultiplier = 3;
    public int RotationSpeed = 10;
    public ProgressListener ProgressListener;
    public bool isAlive;
    
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        isAlive = true;
    }

    private void Update()
    {

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
    }

    private void MovePlayer(float translationHorizontal, float translationVertical)
    {
        float finalSpeed = Speed * Time.deltaTime;
        _rigidbody.velocity = transform.TransformDirection(new Vector3(translationHorizontal * finalSpeed, 0, translationVertical * finalSpeed));
    }

    private void ChargePlayer(float translationHorizontal, float translationVertical)
    {
        
        float boostSpeed = Speed * BoostMultiplier * Time.deltaTime;
        _rigidbody.AddRelativeForce(translationHorizontal * boostSpeed,0, translationVertical * boostSpeed, ForceMode.VelocityChange);
    }

    private void RotatePlayer(int direction)
    {
        transform.rotation *= Quaternion.Euler(0, RotationSpeed * direction * Time.deltaTime, 0);
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
