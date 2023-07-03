using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    public float checkRadius;
    public LayerMask groundLayer;
    public bool isGround;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Check();
    }

    private void Check()
    {
        isGround = Physics2D.OverlapCircle(transform.position, checkRadius, groundLayer);
    }
}
