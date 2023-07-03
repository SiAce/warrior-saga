 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        SetAnimation();
    }

    public void GetHit()
    {
        animator.SetTrigger("Hurt");
    }

    public void SetDeath()
    {
        animator.SetBool("dead", true);
    }

    public void PlayerAttack()
    {
        animator.SetTrigger("attack");
    }

    void SetAnimation()
    {
        animator.SetFloat("speedX", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("velocityY", rb.velocity.y);
        animator.SetBool("isGround", physicsCheck.isGround);
    }

}
