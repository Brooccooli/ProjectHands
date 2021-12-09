using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    private Text _text;
    void Start()
    {
        _text = GetComponent<Text>();
    }

    void FixedUpdate()
    {
        _text.text = PlayerPrefs.GetInt("Money").ToString();
    }
}
