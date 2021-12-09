using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExitGame : MonoBehaviour
{
    private bool _playerInside;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponentInChildren<TextMeshProUGUI>().text = "Press E";
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

    void Update()
    {
        if (_playerInside)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Application.Quit();
            }
        }
    }
}