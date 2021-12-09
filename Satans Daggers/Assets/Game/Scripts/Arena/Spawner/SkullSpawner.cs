using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SkullSpawner : MonoBehaviour
{
    [SerializeField] private int _spawnAmount = 5;
    [SerializeField] private float _speed = 0.1f;
    [SerializeField] private GameObject _skull;
    [SerializeField] private GameObject _spawnArea;
    [SerializeField] private GameObject _dollarSign;
    public GameObject target;

    public float _spawnDelay = 10;
    private float _spawnTimer = 1;

    private Vector3 _goalPosition;
    private int _health = 25;

    private void Start()
    {
        int x = Random.Range(-10, 10);
        int z = Random.Range(-10, 10);
        _goalPosition = new Vector3(x, transform.position.y, z);
    }

    public void hit(int damage)
    {
        _health -= damage;
    }

    private void _move()
    {
        if (transform.position != _goalPosition)
        {
            Vector3 direction = Vector3.Normalize(_goalPosition - transform.position);
            GetComponent<Rigidbody>().AddForce(direction * _speed, ForceMode.Force);
        }
    }
    
    void FixedUpdate()
    {
        _move();
        _spawnTimer -= Time.deltaTime;

        if (_spawnTimer <= 0)
        {
            for (int i = 0; i < _spawnAmount; i++)
            {
                GameObject skull = Instantiate(_skull, _spawnArea.transform.position, Quaternion.identity);
                skull.GetComponent<Skull>().target = target;
            }

            _spawnTimer = _spawnDelay;
        }

        if (_health <= 0)
        {
            for (int i = 0; i < 30; i++)
            {
                GameObject money = Instantiate(_dollarSign, transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)), Quaternion.identity);
                money.GetComponent<MoneyFollow>().target = target;
            }
            Destroy(gameObject);
        }
    }
}
