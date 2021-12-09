using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class SpawnSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private GameObject _spawner;

    [SerializeField] private float _startDelay = 10;
    [SerializeField] private float _spawnDelay = 10;
    private float _spawnTimer;

    private float spawnY = 6.64f;
    
    void Start()
    {
        _spawnTimer = _startDelay;
        transform.position = new Vector3(transform.position.x, spawnY, transform.position.z);
    }

    
    void Update()
    {
        if (_spawnTimer <= 0)
        {
            GameObject temp = Instantiate(_spawner, transform.position, Quaternion.identity);
            temp.transform.Rotate(-90, 0, 0);
            temp.GetComponent<SkullSpawner>().target = _target;
            _spawnTimer = _spawnDelay;
        }

        _spawnTimer -= Time.deltaTime;
    }
}
