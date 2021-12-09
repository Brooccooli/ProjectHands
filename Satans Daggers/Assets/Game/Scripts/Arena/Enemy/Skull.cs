using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Skull : MonoBehaviour
{
    public GameObject target;

    private Vector3 _direction;
    private Vector3 _goalDirection;

    public int health = 10;
    [SerializeField] private float _speed = 3;
    [SerializeField] private GameObject _dollarSign;

    private Vector3 _xOffset;

    private Color _startColor;

    private void Start()
    {
        _xOffset = new Vector3(Random.Range(-2f, 2f), 0);
        _startColor = GetComponent<MeshRenderer>().material.color;
    }

    private void _move()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        
        _direction = Vector3.Lerp(_direction, _goalDirection, 0.5f);
        _direction = Vector3.Normalize(_direction);
        
        transform.rotation = Quaternion.LookRotation(_direction);
        transform.Rotate(-90, 0, 0);

        body.AddForce(-transform.up * _speed, ForceMode.Force);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            target.GetComponent<PlayerManager>().die();
        }
    }

    private void _aliveCheck()
    {
        if (health <= 0)
        {
            for (int i = 0; i < 10; i++)
            {
                GameObject money = Instantiate(_dollarSign, transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f)), Quaternion.identity);
                money.GetComponent<MoneyFollow>().target = target;
            }
            Destroy(gameObject);
        }
    }

    public void hit(int damage)
    {
        health -= damage;
        GetComponent<MeshRenderer>().material.color += Color.red;
    }

    public void _resetColor()
    {
        Material mat = GetComponent<MeshRenderer>().material;
        if (mat.color != _startColor)
        {
            mat.color = Color.Lerp(mat.color, _startColor, 0.01f);
        }
    }
    
    void FixedUpdate()
    {
        _resetColor();
        _goalDirection = (target.transform.position + _xOffset) - transform.position;
        _goalDirection.y += 1;
        _goalDirection = Vector3.Normalize(_goalDirection);
        _move();
        _aliveCheck();
    }
}
