using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeText : MonoBehaviour
{
    public string text = "nothing here";
    
    private TextMeshPro _text;
    private const float _lifeSpan = 2;
    private float _existanceTimer = _lifeSpan;
    
    void Start()
    {
        _text = GetComponent<TextMeshPro>();
        _text.text = text;
    }

    void FixedUpdate()
    {
        _existanceTimer -= Time.deltaTime;
        transform.position += new Vector3(0, 0.01f, 0);

        if (_existanceTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
