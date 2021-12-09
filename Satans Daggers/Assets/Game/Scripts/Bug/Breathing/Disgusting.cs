using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disgusting : MonoBehaviour
{
    private MeshRenderer _mesh;
    private float max = 0.08f;
    void Start()
    {
        _mesh = GetComponent<MeshRenderer>();
    }

    void FixedUpdate()
    {
        Material mat = _mesh.material;

        //print(mat.GetFloat("_PARALLAXMAP"));
    }
}
