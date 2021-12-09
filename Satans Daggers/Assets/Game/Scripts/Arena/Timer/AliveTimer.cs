using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AliveTimer : MonoBehaviour
{
    private TextMeshPro _text;
    private float _timer;
    
    void Start()
    {
        _text = GetComponent<TextMeshPro>();
    }

    void Update()
    {
        _timer += Time.deltaTime;
        _text.text = "Time: " + _timer;
    }
}
