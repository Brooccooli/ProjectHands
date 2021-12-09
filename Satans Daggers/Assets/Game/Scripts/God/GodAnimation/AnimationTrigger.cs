using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    [SerializeField] private Animator _god;
    [SerializeField] private GameObject _fire;

    [Header("Upgrade Text")]
    [SerializeField] private GameObject _upgradeText;
    [SerializeField] private GameObject _spawnPos;
    
    private bool _playerInside;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (PlayerPrefs.GetInt("Money") >= 2000)
            {
                other.GetComponentInChildren<TextMeshProUGUI>().text = "Press E";
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

    void Update()
    {
        // Debug, Delete later
        if (Input.GetKeyDown(KeyCode.O))
        {
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + 2000);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 2000);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayerPrefs.SetInt("Damage", 1);
        }
        
        if (_playerInside)
        {
            if (PlayerPrefs.GetInt("Money") >= 2000)
            {
                _god.Play("Knod");

                if (Input.GetKeyDown(KeyCode.E))
                {
                    GameObject fire = Instantiate(_fire, transform.position, Quaternion.identity);
                    fire.transform.Rotate(-90, 0, 0);
                    
                    // text
                    GameObject text = Instantiate(_upgradeText, _spawnPos.transform.position, Quaternion.identity);
                    text.GetComponent<UpgradeText>().text = "New Hand";
                    text.transform.Rotate(0, 90, 0);
                    
                    // Upgrade
                    PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - 2000);
                    PlayerPrefs.SetInt("Hand", 2);
                }
            }
            else
            {
                _god.Play("Shake");
            }
        }
        else
        {
            _god.Play("Still");
        }
    }
}
