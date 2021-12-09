using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyFollow : MonoBehaviour
{
    public GameObject target;
    
    private Vector3 _direction;
    private Vector3 _goalDirection;
    
    private float _speed = 30;
    
    private void _move()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        
        _direction = Vector3.Lerp(_direction, _goalDirection, 0.5f);
        _direction = Vector3.Normalize(_direction);

        body.AddForce(_direction * _speed, ForceMode.Force);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            target.GetComponent<PlayerManager>().addScore(1);
            Destroy(gameObject);
        }
    }
    
    void FixedUpdate()
    {
        _goalDirection = target.transform.position - transform.position;
        _goalDirection = Vector3.Normalize(_goalDirection);
        _move();
    }
}
