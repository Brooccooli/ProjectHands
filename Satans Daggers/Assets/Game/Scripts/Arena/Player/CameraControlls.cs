using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlls : MonoBehaviour
{
    [SerializeField] private GameObject _shardPrefab;
    [SerializeField] private GameObject _spawnPosition;
    [SerializeField] private GameObject _shootEffect;

    private Vector3 _originPos;
    private Vector3 _spawnPos;

    private float _shootingDelay = 0.1f;
    private float _shootTimer;
    
    private Camera _camera;

    public float mouseSensitivity = 100;

    private float _xRotation = 0;
    
    private void Start()
    {
        _camera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        _originPos = _spawnPosition.transform.localPosition;
    }

    private void _shooting()
    {
        _spawnPos = _spawnPosition.transform.position;
        
        if (_shootTimer > 0) _shootTimer -= Time.deltaTime;
        else if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Move hand forward
            _spawnPosition.transform.localPosition = _originPos * 1.1f;
            for (int i = 0; i < 5; i++)
            {
                Quaternion rotation = _spawnPosition.transform.rotation;
                GameObject shard = Instantiate(_shardPrefab, _spawnPos + _camera.transform.forward, _spawnPosition.transform.rotation);
                shard.transform.localScale = shard.transform.localScale * 0.5f;

                int range = 5;
                float randX = Random.Range(-range, range);
                float randY = Random.Range(-range, range);
                float randZ = Random.Range(-range, range);
                shard.transform.Rotate(new Vector3(randX, randY, randZ));
            }
            Instantiate(_shootEffect, _spawnPos + _camera.transform.forward, _camera.transform.rotation);
            _shootTimer = _shootingDelay;
        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            // Move hand forward
            _spawnPosition.transform.localPosition = _originPos * 1.1f;
            
            Instantiate(_shardPrefab, _spawnPos + _camera.transform.forward, _spawnPosition.transform.rotation);
            Instantiate(_shootEffect, _spawnPos + _camera.transform.forward, _camera.transform.rotation);
            _shootTimer = _shootingDelay;
        }
        else
        {
            _spawnPosition.transform.localPosition = _originPos;
        }
    }

    private void Update()
    {
        _shooting();
        
        Vector3 mouse;
        mouse.x =Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouse.y =Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        _xRotation -= mouse.y;
        _xRotation = Mathf.Clamp(_xRotation, -90, 90);
        
        transform.Rotate(Vector3.up * mouse.x);
        _camera.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
    }
}
