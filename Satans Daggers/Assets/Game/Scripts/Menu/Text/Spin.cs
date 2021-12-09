using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float spinSpeed = 2;
    
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, spinSpeed));
    }
}
