using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CornGrow : MonoBehaviour
{
    private bool _grown;
    private bool _playerInside;

    private float _maxY = 0;
    private float _minY = -3.5f;

    private float _growSpeed = 0.001f;

    private void Start()
    {
        transform.position += new Vector3(0, Random.Range(0, _minY));
        if (!PlayerPrefs.HasKey("Corn"))
        {
            PlayerPrefs.SetInt("Corn", 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (_grown)
            {
                other.GetComponentInChildren<TextMeshProUGUI>().text = "Harvest";
            }

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
        if (_grown && _playerInside)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PlayerPrefs.SetInt("Corn", PlayerPrefs.GetInt("Corn") + 24);
                transform.position += new Vector3(0, _minY);
                _grown = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            PlayerPrefs.SetInt("Corn", PlayerPrefs.GetInt("Corn") + 3000);
        }
    }

    void FixedUpdate()
    {
        if (transform.position.y < 0)
        {
            _grown = false;
            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(transform.position.x, _maxY, transform.position.z), _growSpeed);
        }
        else
        {
            _grown = true;
        }
    }
}