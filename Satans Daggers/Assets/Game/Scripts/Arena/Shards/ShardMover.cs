using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardMover : MonoBehaviour
{
    [SerializeField] private List<Mesh> _meshes;
    [SerializeField] private List<Material> _materials;
    [SerializeField] private float speed = 2;
    [SerializeField] private int _dmg = 1;
    private float _aliveTime = 3;

    private MeshFilter _filter;
    private MeshRenderer _renderer;

    private Vector3 _startSize;

    private float _boostRange = 3;
    
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.up * speed, ForceMode.Impulse);
        if (!PlayerPrefs.HasKey("Damage"))
        {
            PlayerPrefs.SetInt("Damage", 1);
        }

        _filter = GetComponent<MeshFilter>();
        _renderer = GetComponent<MeshRenderer>();
        
        _dmg = PlayerPrefs.GetInt("Damage");
        _startSize = transform.localScale;
    }

    private void OnCollisionEnter(Collision other)
    {
        GameObject player = GameObject.Find("Player");
        
        Vector3 _distance = player.transform.position - transform.position;
        if (Mathf.Abs(_distance.magnitude) <= _boostRange)
        {
            player.GetComponent<PlayerMovement>()._knockBack(_distance * (_boostRange - _distance.magnitude));
        }
        
        if (other.gameObject.tag == "enemy")
        {
            other.gameObject.GetComponent<Skull>()?.hit(_dmg);
            other.gameObject.GetComponent<SkullSpawner>()?.hit(_dmg);
        }
        Destroy(gameObject);
    }

    private void Update()
    {
        _aliveTime -= Time.deltaTime;
        if(_aliveTime <= 0) Destroy(gameObject);

        if (PlayerPrefs.GetInt("Hand") == 3)
        {
            _filter.mesh = _meshes[1];
            transform.localScale = _startSize * 0.2f;
            _renderer.material = _materials[1];
        }
        else
        {
            _filter.mesh = _meshes[0];
            transform.localScale = _startSize;
            _renderer.material = _materials[0];
        }
    }
}
