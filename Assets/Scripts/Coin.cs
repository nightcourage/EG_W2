using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int RotationSpeed = 1; 
    private AudioSource _audioSource;
    public GameObject _coinRenderer;

    private void Awake()
    {
        _audioSource = GetComponentInChildren<AudioSource>();
    }

    void Update()
    {
        RotateCoin();
    }

    private void RotateCoin()
    {
        transform.rotation *= Quaternion.Euler(0, 0, RotationSpeed * Time.deltaTime);
    }

    public void PlaySound()
    {
        _audioSource.Play();
    }

    public void DestroyCoin()
    {
        PlaySound();
        _coinRenderer.SetActive(false);
    }
}
