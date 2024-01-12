using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    [Header("Dectection Parameters")]
    public bool manual;
    public float checkRadius;
    public Vector2 bottomOffset;
    public Vector2 leftOffset;
    public Vector2 rightOffset;
    public LayerMask groundLayer;

    [Header("Dectection Results")]
    public bool isGround;
    public bool touchLeftWall;
    public bool touchRightWall;

    private CapsuleCollider2D coll;

    public void TurnAround()
    {
        leftOffset.x *= -1;
        rightOffset.x *= -1;
        bottomOffset.x *= -1;
    }


    private void Awake()
    {
        isGround = true;
        coll = GetComponent<CapsuleCollider2D>();

        if (manual) { return; }

        leftOffset = transform.localScale * new Vector2(
            coll.offset.x - coll.bounds.size.x / 2,
            coll.offset.y
            );

        rightOffset = transform.localScale * new Vector2(
            coll.offset.x + coll.bounds.size.x / 2,
            coll.offset.y
            );
    }

    // Update is called once per frame
    void Update()
    {
        Check();
    }

    private void Check()
    {
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkRadius, groundLayer);
        touchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, checkRadius, groundLayer);
        touchRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, checkRadius, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkRadius);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, checkRadius);
    }
}
