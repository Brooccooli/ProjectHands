using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int _max_air_speed = 5;
    [SerializeField] private int _max_speed = 10;
    [SerializeField] private int _max_accel = 2;
    
    [SerializeField] private int _jump_speed = 10;

    private Vector3 _velocity;
    public float friction = 2;
    public float gravity = 10;
    
    private CharacterController _controller;

    private Vector3 _goalGizmo;

    private void Start()
    {
        _max_accel = _max_accel * _max_speed;
        _controller = GetComponent<CharacterController>();
    }
    
    public void _knockBack(Vector3 push)
    {
        _velocity += push;
    }

    private void _update_velocity_ground(Vector3 goalDirection)
    {
        _friction();

        float current_speed = Vector3.Dot(_velocity, goalDirection);
        float add_speed = Mathf.Clamp(_max_speed - current_speed, 0, _max_accel * Time.deltaTime);

        _velocity += goalDirection * add_speed;
    }
    
    private void _update_velocity_air(Vector3 goalDirection)
    {
        float current_speed = Vector3.Dot(_velocity, goalDirection);
        float add_speed = Mathf.Clamp(_max_air_speed - current_speed, 0, _max_accel * Time.deltaTime);

        _velocity += goalDirection * add_speed;
    }

    private void _friction()
    {
        _velocity -= _velocity * friction * Time.deltaTime;
    }

    private void _controls()
    {
        Vector3 goalDirection = Vector3.zero;
        float xAxis = Input.GetAxisRaw("Horizontal");
        float yAxis = Input.GetAxisRaw("Vertical");

        goalDirection = transform.right * xAxis + transform.forward * yAxis;
        goalDirection = Vector3.Normalize(goalDirection);
        
        if (Input.GetKey(KeyCode.Space))
        {
            if (_controller.isGrounded)
            {
                _velocity.y = _jump_speed;
            }
        }
        
        _gravityAndSlide(goalDirection);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));
        Gizmos.DrawLine(transform.position, transform.position + _velocity);
    }

    private void _gravityAndSlide(Vector3 goalDirection)
    {
        // Check angle of floor
        Camera camera = GetComponentInChildren<Camera>();
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity);
        float angle = Vector3.Dot(Vector3.down, hit.normal);
        
        if (_controller.isGrounded)
        {
            if (angle > -0.5f)
            {
                // goalDirection += -camera.transform.up + (hit.normal * 0.5f) * 10;
                goalDirection += camera.transform.forward + (-camera.transform.up + (hit.normal * 0.5f) * 10);
                _update_velocity_air(goalDirection);
            }
            else
            {
                _update_velocity_ground(goalDirection);
            }
        }
        else
        {
            Applygravity();
        }

        void Applygravity()
        {
            _update_velocity_air(goalDirection);
            _velocity.y -= gravity * Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        _controller.Move(_velocity * Time.deltaTime);
    }

    void Update()
    {
        _controls();
    }
}
