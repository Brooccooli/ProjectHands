using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class BuyIceHand : MonoBehaviour
{
    private bool _playerInside;
    private TextMeshProUGUI _playerText;

    private bool _won;
    
    private float _viewTime = 5;
    private float _viewTimer = -5;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerText = other.GetComponentInChildren<TextMeshProUGUI>();
            other.GetComponentInChildren<TextMeshProUGUI>().text = "Trade Corn";
            _playerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponentInChildren<TextMeshProUGUI>().text = "";
            _playerInside = false;
        }
    }

    private void Update()
    {
        if (_playerInside)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (PlayerPrefs.GetInt("Corn") >= 3000)
                {
                    _won = true;
                    PlayerPrefs.SetInt("Hand", 1);
                    PlayerPrefs.SetInt("Corn", PlayerPrefs.GetInt("Corn") - 3000);
                    _viewTimer = _viewTime;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (_viewTimer > 0)
        {
            if (_won)
            {
                _playerText.text = "New Hand";
                _playerText.color = Color.green;
            }
            else
            {
                _playerText.text = "Lost 50$";
                _playerText.color = Color.red;
            }
        }

        if (_viewTimer > -2 && _viewTimer < 0)
        {
            _playerText.text = "";
            _playerText.color = Color.black;
        }

        if (_viewTimer > -5)
        {
            _viewTimer -= Time.deltaTime;
        }
    }
}
