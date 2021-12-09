using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Trader : MonoBehaviour
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
            other.GetComponentInChildren<TextMeshProUGUI>().text = "Press E, 50$";
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
                if (PlayerPrefs.GetInt("Money") >= 50)
                {
                    if (Random.Range(0, 100) > 95)
                    {
                        _won = true;
                        PlayerPrefs.SetInt("Hand", 3);
                    }
                    else
                    {
                        _won = false;
                        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") -50);
                    }
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
