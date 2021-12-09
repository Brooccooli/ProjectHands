using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowStocks : MonoBehaviour
{
    private int _skipFrames = 60;
    private const int _numOfCorps = 9;
    
    private Text _text;
    private string[] _company = new string[_numOfCorps];
    private int[] _price = new int[_numOfCorps];
    
    private string[] _names = new string[10]
    {
        "Zombie",
        "Skull",
        "Death",
        "Tech",
        "Coffee",
        "Death",
        "Devils",
        "Bread",
        "Apples",
        "Ghost"
    };
    private string[] _end = new string[3]
    {
        "inc.",
        "ltd.",
        "corp."
    };
    
    void Start()
    {
        _text = GetComponent<Text>();
        // names
        for (int i = 0; i < _company.Length; i++)
        {
            _company[i] += _names[i] + " ";
            if (Random.Range(0, 2) > 0)
            {
                _company[i] += _names[Random.Range(0, _names.Length)] + " ";
            }

            _company[i] += _end[Random.Range(0, _end.Length)];
        }
        
        // start price
        for (int i = 0; i < _price.Length; i++)
        {
            _price[i] = Random.Range(-10, 10);
        }
    }

    private void _priceChange()
    {
        for (int i = 0; i < _price.Length; i++)
        {
            _price[i] += Random.Range(-3, 4);
        }
    }

    private void _printScreen()
    {
        string screen = "";
        for (int i = 0; i < _price.Length; i++)
        {
            string addColor = "<color=red>";
            if (_price[i] >= 0)
            {
                addColor = "<color=green>";
            }
            screen += "<b>" + _company[i] + "</b> " + addColor + _price[i] + "</color> \n";
        }

        _text.text = screen;
    }

    private void _bubbleSort()
    {
        for (int i = 1; i < _numOfCorps; i++)
        {
            for (int j = 0; j < _numOfCorps; j++)
            {
                if (_price[i] > _price[i - 1])
                {
                    int tempI = _price[i - 1];
                    string tempN = _company[i - 1];
                    
                    _price[i - 1] = _price[i];
                    _company[i - 1] = _company[i];

                    _price[i] = tempI;
                    _company[i] = tempN;
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (_skipFrames <= 0)
        {
            _skipFrames = 60;
            _priceChange();
        }

        _skipFrames--;
        
        _bubbleSort();
        _printScreen();
    }
}
