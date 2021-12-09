using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CornText : MonoBehaviour
{
    private TextMeshPro _text;
    void Start()
    {
        _text = GetComponent<TextMeshPro>();
    }

    void Update()
    {
        _text.text = "Corn: " + PlayerPrefs.GetInt("Corn");
    }
}
