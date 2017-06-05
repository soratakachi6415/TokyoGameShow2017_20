using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
    
    public float speed;
    public float range;
    
    Vector3 origin;
    void Start()
    {
        origin = transform.position;
    }

    void Update()
    {
        transform.position = origin + Vector3.right * Mathf.Sin(Time.time + speed) * range;
    }
}
