using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private List<Material> _handMaterials;
    [SerializeField] private MeshRenderer _hand;

    private int score;
    private int money;
    
    void Start()
    {
        if (!PlayerPrefs.HasKey("Money"))
        {
            PlayerPrefs.SetInt("Money", money);
        }
        if (!PlayerPrefs.HasKey("Hand"))
        {
            PlayerPrefs.SetInt("Hand", 0);
        }

        money = PlayerPrefs.GetInt("Money");
    }

    public void addScore(int points)
    {
        score += points;
    }

    public void die()
    {
        SceneManager.LoadScene("Village");
        PlayerPrefs.SetInt("Money", money + score);
    }

    private void _changeHand()
    {
        _hand.material = _handMaterials[PlayerPrefs.GetInt("Hand")];
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            PlayerPrefs.SetInt("Hand", 0);
        }
        
        _changeHand();
        if (transform.position.y < -1)
        {
            die();
        }
    }
}
