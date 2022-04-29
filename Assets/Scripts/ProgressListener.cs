using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressListener : MonoBehaviour
{
    public float minDistance = 0.1f;
    public TextMeshProUGUI TextTotal;
    public TextMeshProUGUI TextCollected;
    public GameObject FinalPanel;
    public GameObject GameOverPanel;
    
    private int _coinCounter;
    private int _coinsCollected;
    private Player _player;
    private List<Coin> _coins = new List<Coin>();

    void Start()
    {
        _player = FindObjectOfType<Player>();
        _coinCounter = CountCoinsAtStart();
        TextTotal.text = "/" + _coinCounter.ToString();
        TextCollected.text = _coinsCollected.ToString();
        FinalPanel.SetActive(false);
        GameOverPanel.SetActive(false);
    }
    
    void Update()
    {
        CollectCoins();
        TextCollected.text = _coinsCollected.ToString();
        ShowFinalScreen();
    }

    private int CountCoinsAtStart()
    {
        foreach (var coin in FindObjectsOfType<Coin>())
        {
            _coins.Add(coin);
        }

        return _coins.Count;
    }
    
    private void CollectCoins()
    {
        foreach (var coin in _coins)
        {
            if (Vector3.Distance(coin.transform.position, _player.transform.position) <= minDistance)
            {
                _coinsCollected += 1;
                _coins.Remove(coin);
                coin.DestroyCoin();
                break;
            }
        }
    }

    private void ShowFinalScreen()
    {
        if (_coinCounter == _coinsCollected)
        {
            FinalPanel.SetActive(true);
            _player.GetComponent<Player>().enabled = false;
            _player.GetComponent<Collider>().enabled = false;
        }
    }

    public void ShowLoseScreen()
    {
        GameOverPanel.SetActive(true);
    }
}
