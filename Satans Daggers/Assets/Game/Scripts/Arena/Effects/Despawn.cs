using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawn : MonoBehaviour
{
    void Update()
    {
        if(!GetComponent<ParticleSystem>().isPlaying) Destroy(gameObject);  
    }
}
